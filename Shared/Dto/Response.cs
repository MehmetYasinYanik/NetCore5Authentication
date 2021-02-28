using System.Text.Json.Serialization;

namespace Shared.Dto
{
    public class Response<T> where T : class
    {
        public T Data { get; private set; }

        public int StatusCode { get; private set; }

        public ErrorDto Error { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public static Response<T> Success(T data, int statusCode) => new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };

        public static Response<T> Success(int statusCode) => new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };

        public static Response<T> Fail(ErrorDto errorDto, int statusCode) => new Response<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow) => new Response<T> { Error = new ErrorDto(errorMessage, isShow), StatusCode = statusCode, IsSuccessful = false };
    }
}
