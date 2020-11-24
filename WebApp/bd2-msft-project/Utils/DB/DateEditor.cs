using bd2_msft_project.Models.DBModels;
using System.Data;
using System.Data.SqlClient;

namespace bd2_msft_project.Utils.DB
{
    public class DateEditor
    {
        public void AddOrEditProduct(Product givenProduct)
        {
            using SqlCommand addOrEditProduct = new SqlCommand
            {
                CommandText = Settings.AddOrEditProductProcedure,
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(Settings.DBConnectionString)
            };
            if (addOrEditProduct.Connection.State != ConnectionState.Open)
            {
                addOrEditProduct.Connection.Open();
            }

            addOrEditProduct.Parameters.Add(new SqlParameter("@ID", givenProduct.ID));
            addOrEditProduct.Parameters.Add(new SqlParameter("@ProductName", givenProduct.ProductName));
            addOrEditProduct.Parameters.Add(new SqlParameter("@ProductDescription", givenProduct.ProductDescription));
            addOrEditProduct.Parameters.Add(new SqlParameter("@DateAdded", givenProduct.DateAdded));
            addOrEditProduct.Parameters.Add(new SqlParameter("@RemainingStock", givenProduct.RemainingStock));
            addOrEditProduct.Parameters.Add(new SqlParameter("@UnitPrice", givenProduct.UnitPrice));

            addOrEditProduct.ExecuteNonQuery();
            addOrEditProduct.Connection.Close();
        }

        public void DeleteProductByID(int intProductID)
        {
            using SqlCommand addOrEditProduct = new SqlCommand
            {
                CommandText = Settings.DeleteProductProcedure,
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(Settings.DBConnectionString)
            };
            if (addOrEditProduct.Connection.State != ConnectionState.Open)
            {
                addOrEditProduct.Connection.Open();
            }

            addOrEditProduct.Parameters.Add(new SqlParameter("@ID", intProductID));

            addOrEditProduct.ExecuteNonQuery();
            addOrEditProduct.Connection.Close();
        }
    }
}
