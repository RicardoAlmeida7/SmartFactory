
namespace SmartFactoryApplication.Validation
{
    public class ValidationError : IValidationError
    {
        private readonly Dictionary<string, string> _errors = [];

        public void AddError(string errorProperty, string errorMessage) =>
            _errors.TryAdd(errorProperty, errorMessage);

        public void AddErrors(Dictionary<string, string> errors)
        {
            foreach (var error in errors) _errors.TryAdd(error.Key, error.Value);
        }

        public Dictionary<string, string> GetValidationErrors() => _errors;

        public bool HasValidationErrors() => _errors.Count > 0;
    }
}
