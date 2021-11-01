namespace AuditoriumWebHosting.Models
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
        
        public OperationResult(bool isSuccess, object data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        
        public string Message { get; set; }
        
        public object Data { get; set; }
    }
}