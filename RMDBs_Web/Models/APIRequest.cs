using Microsoft.AspNetCore.Mvc;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Models
{
    public class APIRequest
    {
        public ApiType apiType {  get; set; }= ApiType.GET;

        public string Url {  get; set; }
        public object Data { get; set; }

        public string Token { get; set; }


    }
}
