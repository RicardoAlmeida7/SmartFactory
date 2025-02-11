namespace SmartFactoryApplication.Validation
{
    public interface IValidationError
    {
        bool HasValidationErrors();
        Dictionary<string, string> GetValidationErrors();
        void AddError(string errorProperty, string errorMessage);
        void AddErrors(Dictionary<string, string> errors);
        void AddValidationError(bool condition, string field, string message);
    }
}
