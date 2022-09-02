namespace WebShopAPI.Models
{
    public class Response
    {
        public Response()
        {

        }
        public bool Succeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
    public class Response<T>:Response
    {
        public Response(T value)
        {
            Succeded = true;
            Errors = null;
            Message = string.Empty;
            Tag = value;
        }
        public T Tag { get; set; }
    }
}
