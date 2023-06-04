namespace AppNoOne.TagHelper
{
    public class ResponseData
    {
        public long Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int ErrorCode { get; set; }
        public string Token { get; set; }

        public ResponseData()
        {
            this.Id = 0;
            this.Success = false;
            this.Message = string.Empty;
            this.Data = null;
            this.ErrorCode = 0;
            this.Token = string.Empty;
        }
    }
}
