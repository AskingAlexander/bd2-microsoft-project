using bd2_msft_project.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace bd2_msft_project.Utils.DB
{
    public class DataGetter
    {
        public UserData GetUser(string userName, string password)
        {
            using SqlCommand getUser = new SqlCommand
            {
                CommandText = Settings.LoginOrRegisterProcedure,
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(Settings.DBConnectionString)
            };
            if (getUser.Connection.State != ConnectionState.Open)
            {
                getUser.Connection.Open();
            }

            SqlParameter statusCodeParameter = new SqlParameter("@StatusCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter userIDParameter = new SqlParameter("@UserID", SqlDbType.Int) { Direction = ParameterDirection.Output };

            getUser.Parameters.Add(new SqlParameter("@UserName", userName));
            getUser.Parameters.Add(new SqlParameter("@PassWord", password));
            getUser.Parameters.Add(statusCodeParameter);
            getUser.Parameters.Add(userIDParameter);

            getUser.ExecuteNonQuery();
            getUser.Connection.Close();

            LoginOrRegisterStatusCode dbResult = (LoginOrRegisterStatusCode)int.Parse(statusCodeParameter.Value.ToString());

            if (dbResult == LoginOrRegisterStatusCode.InvalidPassword) return null;

            int userID = int.Parse(userIDParameter.Value.ToString());

            using DataTable userTable = new DataTable();
            using SqlCommand getUserData = new SqlCommand
            {
                CommandText = Settings.GetUserByIDProcedure,
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(Settings.DBConnectionString)
            };
            if (getUserData.Connection.State != ConnectionState.Open)
            {
                getUserData.Connection.Open();
            }

            getUserData.Parameters.Add(new SqlParameter("@UserID", userID));

            using SqlDataAdapter userGrabber = new SqlDataAdapter(getUserData);
            DataRow userData = null;
            try
            {
                userGrabber.Fill(userTable);
                userData = userTable.Rows[0];

            }
            catch (Exception e)
            {
                return null;
            }

            return new UserData
            {
                ID = userID,
                FirstName = userData["FirstName"] != DBNull.Value ? (string)userData["FirstName"] : string.Empty,
                LastName = userData["LastName"] != DBNull.Value ? (string)userData["LastName"] : string.Empty,
                Email = userData["Email"] != DBNull.Value ? (string)userData["Email"] : string.Empty,
                BirthDay = userData["BirthDay"] != DBNull.Value ? (DateTime)userData["BirthDay"] : DateTime.UtcNow
            };
        }

        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            using DataTable productTable = new DataTable();
            using SqlCommand getAllProducts = new SqlCommand
            {
                CommandText = Settings.GetUserByIDProcedure,
                CommandType = CommandType.StoredProcedure,
                Connection = new SqlConnection(Settings.DBConnectionString)
            };
            if (getAllProducts.Connection.State != ConnectionState.Open)
            {
                getAllProducts.Connection.Open();
            }

            using SqlDataAdapter productGrabber = new SqlDataAdapter(getAllProducts);
            try
            {
                productGrabber.Fill(productTable);

                foreach (DataRow currentProduct in productTable.Rows)
                {
                    allProducts.Add(new Product
                    {
                        ID = currentProduct["ID"] != DBNull.Value ? (int)currentProduct["ID"] : 0,
                        DateAdded = currentProduct["DateAdded"] != DBNull.Value ? (DateTime)currentProduct["DateAdded"] : DateTime.UtcNow,
                        ProductDescription = currentProduct["ProductDescription"] != DBNull.Value ? (string)currentProduct["ProductDescription"] : string.Empty,
                        ProductName = currentProduct["ProductName"] != DBNull.Value ? (string)currentProduct["FirstName"] : string.Empty,
                        RemainingStock = currentProduct["RemainingStock"] != DBNull.Value ? (int)currentProduct["RemainingStock"] : 0,
                        UnitPrice = currentProduct["UnitPrice"] != DBNull.Value ? (int)currentProduct["UnitPrice"] : 0
                    });
                }
            }
            catch (Exception e)
            {
                return allProducts;
            }

            return allProducts;
        }
    }
}
