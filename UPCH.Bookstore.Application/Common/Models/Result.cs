using System.Collections.Generic;

namespace UPCH.Bookstore.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public IEnumerable<string>? Errors { get; init; }

        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(IEnumerable<string> errors) => new Result { IsSuccess = false, Errors = errors };
        public static Result Failure(string error) => Failure(new[] { error });
    }

    public class Result<T> : Result
    {
        public T? Data { get; init; }

        public static new Result<T> Success() => new Result<T> { IsSuccess = true };
        public static Result<T> Success(T data) => new Result<T> { IsSuccess = true, Data = data };
        public static new Result<T> Failure(IEnumerable<string> errors) => new Result<T> { IsSuccess = false, Errors = errors };
        public static new Result<T> Failure(string error) => Failure(new[] { error });
    }
}
