namespace RMDB_Utility
{
    public static  class Class1
    {
        public enum ProductionStatus
        {
            InDevelopment = 1,
            PreProduction = 2,
            InProduction = 3,
            PostProduction = 4,
            Released = 5,
            Cancelled = 6
        }



        public enum MediaType
        {
            Image =1,
            Video=2
        }


        public enum ApiType
        {
            GET,
            POST, PUT, DELETE,
        }
        public static string SessionToken = "JWTToken";

    }
}
