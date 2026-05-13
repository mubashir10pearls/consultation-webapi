using ClinicConsultation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicConsultation.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Consultation> Consultations => Set<Consultation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ── Consultation ──────────────────────────────────────────────────────
        builder.Entity<Consultation>(entity =>
        {
            entity.Property(x => x.PatientName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.PrimaryConcern)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            // 1-to-many: Consultation → Messages (defined once, duplicate removed)
            entity.HasMany(x => x.Messages)
                .WithOne(x => x.Consultation)
                .HasForeignKey(x => x.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-1: Consultation → Recommendation
            entity.HasOne(x => x.Recommendation)
                .WithOne(x => x.Consultation)
                .HasForeignKey<Recommendation>(x => x.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-1: Consultation → Appointment
            entity.HasOne(x => x.Appointment)
                .WithOne(x => x.Consultation)
                .HasForeignKey<Appointment>(x => x.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── Message ───────────────────────────────────────────────────────────
        builder.Entity<Message>(entity =>
        {
            entity.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(4000);
        });

        // ── Recommendation ────────────────────────────────────────────────────
        builder.Entity<Recommendation>(entity =>
        {
            entity.Property(x => x.Summary)
                .HasMaxLength(1000);

            entity.Property(x => x.RecommendedTreatment)
                .HasMaxLength(200);

            entity.Property(x => x.Reasoning)
                .HasMaxLength(1000);

            entity.Property(x => x.AiInsight)
                .HasMaxLength(500);

            // 1-to-many: Recommendation → TreatmentOptions
            entity.HasMany(x => x.Treatments)
                .WithOne(x => x.Recommendation)
                .HasForeignKey(x => x.RecommendationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── TreatmentOption ───────────────────────────────────────────────────
        builder.Entity<TreatmentOption>(entity =>
        {
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasMaxLength(1000);

            entity.Property(x => x.Unit)
                .HasMaxLength(50);

            entity.Property(x => x.EstimatedCostMin)
                .HasPrecision(18, 2);

            entity.Property(x => x.EstimatedCostMax)
                .HasPrecision(18, 2);
        });

        // ── Appointment ───────────────────────────────────────────────────────
        builder.Entity<Appointment>(entity =>
        {
            entity.Property(x => x.Treatment)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.TargetArea)
                .HasMaxLength(200);

            entity.Property(x => x.EstimateDuration)
                .HasMaxLength(50);

            entity.Property(x => x.Provider)
                .HasMaxLength(200);
        });
    }
}
