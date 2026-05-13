namespace ClinicConsultation.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, Guid id)
            : base($"{entityName} with id '{id}' was not found.") { }

        public NotFoundException(string message) : base(message) { }
    }
}
