using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Helpers
{
    
        public class Result
        {
            public bool IsSuccess { get;  set;  }

            public string? Error { get;   set; }

            public static Result Success()
                => new() { IsSuccess = true };

            public static Result Failure(string error)
                => new()
                {
                    IsSuccess = false,
                    Error = error
                };
        }
    public class Result<T> : Result
    {
        public T? Data { get; set; }
        public static Result<T> Success(T data)
            => new() { IsSuccess = true, Data = data };
        public new static Result<T> Failure(string error)
            => new()
            {
                IsSuccess = false,
                Error = error
            };
    }
}

