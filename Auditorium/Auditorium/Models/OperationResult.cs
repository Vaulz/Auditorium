namespace Auditorium.Models
{
    public class OperationResult
    {
        public OperationResult()
        {
        }

        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        
        public string Message { get; set; }
        
        public object Data { get; set; }
    }
}