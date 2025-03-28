using System.Net;

namespace RMDBs_API.Model
{
    public class APIResponse
    {
        public APIResponse() 
        {
            ErrorMessages = new List<string>(); 
        }
        public HttpStatusCode statusCode {  get; set; } 
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result {  get; set; }
    }
}
