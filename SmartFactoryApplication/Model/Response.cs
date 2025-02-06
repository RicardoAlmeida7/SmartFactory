namespace SmartFactoryApplication.Model
{
    public class Response<T>(bool isValid, T? data, Dictionary<string, string>? errors)
    {
        public bool IsValid { get; } = isValid;
        public T? Data { get; } = data;
        public Dictionary<string, string>? Errors { get; } = errors;

        public static Response<T> Success(T data) => new(true, data, null);
        public static Response<T> Success(Dictionary<string, string> messages) => new(true, default, messages);
        public static Response<T> Fail(Dictionary<string, string> errors) => new(false, default, errors);
    }
}
