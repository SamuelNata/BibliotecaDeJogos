namespace GameLib.Model.DTOs
{
    public class MessageApiResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}