using MeloSolution.authenticationAPI.Repository;
using MeloSolution.authenticationAPI.Entities;
using System.Data.SqlClient;
using MeloSolution.Repository.CreateObject;
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class ConsumeUser:IConsumeObject{
    private readonly IConfiguration configuration;
    private readonly CreateUser createUser;
    public ConsumeUser(){
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        createUser = new CreateUser();
    }
    public object ConsumeUniqueObject(string sqlQuery, object parameters = null){
        string connectionString = configuration.GetConnectionString("ProverbsDataBankConnection");
        try{
            using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                sqlConnection.Open();
                using(SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection)){
                    if(parameters != null){
                            foreach(var prop in parameters.GetType().GetProperties()){
                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters));
                            }
                        }
                    using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()){
                        while(sqlDataReader.Read()){
                            int Id = sqlDataReader.GetInt32(0);
                            string Name = sqlDataReader.GetString(1);
                            string Email = sqlDataReader.GetString(2);
                            string Password = sqlDataReader.GetString(3);
                            User user = createUser.Create(Id, Name, Email, Password);
                            return user;
                        }
                    }
                }
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return null;
    }
    public List<Object> ConsumeMultipleObject(string sqlQuery, object parameters = null){
        string connectionString = configuration.GetConnectionString("ProverbsDataBankConnection");
        List<object> users = new List<object>();
        try{
            using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                sqlConnection.Open();
                using(SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection)){
                    if(parameters != null){
                            foreach(var prop in parameters.GetType().GetProperties()){
                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters));
                            }
                        }
                    using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()){
                        while(sqlDataReader.Read()){
                            int Id = sqlDataReader.GetInt32(0);
                            string Name = sqlDataReader.GetString(1);
                            string Email = sqlDataReader.GetString(2);
                            string Password = sqlDataReader.GetString(3);
                            User user = createUser.Create(Id, Name, Email, Password);
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return null;
    }
}