namespace bd2_msft_project.Utils
{
    public static class Settings
    {
        /*
         * In production we do
         * public static string DBConnectionString => Environment.GetEnvironmentVariable("BD2ProjectDBCS");
        */
        // But Locally
        public static string DBConnectionString => "";

        public const string LoginOrRegisterProcedure = "usp_LoginOrRegister";
        public const string GetUserByIDProcedure = "usp_GetUserByID";
        public const string GetAllProducts = "usp_GetAllProducts";
        public const string AddOrEditProductProcedure = "usp_AddOrEditProduct";
        public const string DeleteProductProcedure = "usp_DeleteProduct";
    }
}
