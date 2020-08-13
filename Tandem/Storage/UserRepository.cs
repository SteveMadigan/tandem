using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tandem.Model;

namespace Tandem.Storage
{
    public interface IUserRepository
    {
        public Task<User> GetByEmail(string emailAddress);
        public Task<string> CreateUser(User user);
    }

    public class UserRepository: IUserRepository
    {
        private string _sqlConnectionString;

        private string SqlConnectionString
        {
            get
            {
                if (_sqlConnectionString == null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "tandem-madigan.database.windows.net";
                    builder.UserID = "smadigan";
                    builder.Password = "3Sqj3UEegcTJqN";
                    builder.InitialCatalog = "Tandem";
                    return _sqlConnectionString = builder.ConnectionString;
                }

                return _sqlConnectionString;
            }
        }

        public async Task<User> GetByEmail(string emailAddress)
        {
            var result = new User();

            await using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {

                var sb = new StringBuilder();
                sb.Append("Select TOP 1 FirstName, MiddleName, LastName, EmailAddress, Phone, Id ");
                sb.Append("From Users ");
                sb.Append("Where EmailAddress = '");
                sb.Append(emailAddress);
                sb.Append("';");

                var sql = sb.ToString();

                await using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    await using (var reader = await sqlCommand.ExecuteReaderAsync())
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
                        else
                        {
                            result = null;
                        }
                    }
                }
            }

            return result;
        }

        public async Task<string> CreateUser(User user)
        {
            var id = Guid.NewGuid().ToString();

            await using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {

                var sb = new StringBuilder();
                sb.Append("Insert into Users (FirstName, MiddleName, LastName, EmailAddress, Phone, Id) ");
                sb.AppendFormat("Values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", user.FirstName, user.MiddleName, user.LastName, user.EmailAddress, user.PhoneNumber, id);
                var sql = sb.ToString();

                try
                {
                    await using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        sqlConnection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                catch
                {
                    return null;
                }
            }

            return id;
        }
    }
}
