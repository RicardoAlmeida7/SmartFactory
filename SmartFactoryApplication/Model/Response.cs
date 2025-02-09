using SmartFactoryApplication.Utils;
using System.Net;

namespace SmartFactoryApplication.Model
{
    public class Response<T>(bool isValid, T? data, Dictionary<string, string>? errors, HttpStatusCodes statusCode)
    {
        public bool IsValid { get; } = isValid;
        public T? Data { get; } = data;
        public Dictionary<string, string>? Errors { get; } = errors;
        public HttpStatusCodes StatusCode { get; } = statusCode;

        public static Response<T> Success(T data) => new(true, data, null, HttpStatusCodes.OK);
        public static Response<T> Created(T data) => new(true, data, null, HttpStatusCodes.CREATED);
        public static Response<T> NoContent() => new(true, default, null, HttpStatusCodes.NO_CONTENT);
        public static Response<T> NotFound(Dictionary<string, string> errors) => new(false, default, errors, HttpStatusCodes.NOT_FOUND);
        public static Response<T> Fail(Dictionary<string, string> errors) => new(false, default, errors, HttpStatusCodes.BAD_REQUEST);
    }
}
