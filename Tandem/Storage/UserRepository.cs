using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tandem.Model;
using Tandem.Requests;
using Tandem.Responses;

namespace Tandem.Storage
{
    public interface IUserRepository
    {
        public User GetByEmail(string emailAddress);
        public string CreateUser(User user);
    }

    public class UserRepository: IUserRepository
    {
        private string sqlConnectionString;

        private string SqlConnectionString
        {
            get
            {
                if (sqlConnectionString == null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "tandem-madigan.database.windows.net";
                    builder.UserID = "smadigan";
                    builder.Password = "3Sqj3UEegcTJqN";
                    builder.InitialCatalog = "Tandem";
                    return sqlConnectionString = builder.ConnectionString;
                }

                return sqlConnectionString;
            }
        }

        // should be User return type, map to GetUserResponse
        public User GetByEmail(string emailAddress)
        {
            var result = new User();

            using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {

                var sb = new StringBuilder();
                sb.Append("Select TOP 1 FirstName, MiddleName, LastName, EmailAddress, PhoneNumber, Id ");
                sb.Append("From Tandem ");
                sb.Append("Where EmailAddress = '");
                sb.Append(emailAddress);
                sb.Append("';");

                var sql = sb.ToString();

                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.FirstName = reader.GetString(0);
                            result.MiddleName = reader.GetString(1);
                            result.LastName = reader.GetString(2);
                            result.EmailAddress = reader.GetString(3);
                            result.PhoneNumber = reader.GetString(4);
                            result.UserId = reader.GetString(5);
                        }
                        // else return/throw not found
                    }
                }
            }

            return result;
        }

        public string CreateUser(User user)
        {
            var id = Guid.NewGuid().ToString();

            using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {

                var sb = new StringBuilder();
                sb.Append("Insert into Users (FirstName, MiddleName, LastName, EmailAddress, PhoneNumber, Id) ");
                sb.AppendFormat("Values ({0}, {1}, {2}, {3}, {4}, {5});", user.FirstName, user.MiddleName, user.LastName, user.EmailAddress, user.PhoneNumber, id);
                var sql = sb.ToString();

                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }

            return id;
        }
    }
}
