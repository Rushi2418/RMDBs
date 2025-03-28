using RMDBs_Web.Models.ViewModel;
using System.Net;

namespace RMDBs_Web.Models
{
    public class APIResponse<T>
    {
        public APIResponse() 
        {
            ErrorMessages = new List<string>(); 
        }
        public HttpStatusCode statusCode {  get; set; } 
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; } // This will handle the collection of MovieDetailsWithRatings
    }
}
