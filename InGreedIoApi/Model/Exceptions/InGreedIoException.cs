using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Model.Exceptions
{
    public class InGreedIoException : Exception
    {
        private readonly int _responseStatusCode;

        public InGreedIoException(string message) : base(message)
        {
            _responseStatusCode = StatusCodes.Status400BadRequest;
        }

        public InGreedIoException(string message, int responseStatusCode) : base(message)
        {
            _responseStatusCode = responseStatusCode;
        }

        public IActionResult Result => new ObjectResult(new { Message })
        {
            StatusCode = _responseStatusCode
        };
    }
}
