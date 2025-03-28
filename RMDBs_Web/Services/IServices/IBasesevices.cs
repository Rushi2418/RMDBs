using RMDBs_Web.Models;

namespace RMDBs_Web.Services.IServices
{
    public interface IBasesevices
    {
        APIResponse<object> Response { get; set; }  // Here, 'object' is used as a placeholder; you can replace it with a more specific type if needed.
        Task<APIResponse<T>> SendAsync<T>(APIRequest apiRequest);

    }
}
