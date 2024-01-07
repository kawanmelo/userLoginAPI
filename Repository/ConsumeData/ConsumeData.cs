using MeloSolution.authenticationAPI.Repository;
using System.Data.SqlClient;

namespace MeloSolution.authenticationAPI.Repository
{
    public class ConsumeData:IConsumeData{
        private readonly IConfiguration configuration;
        public ConsumeData(){
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }
        public int ConsumeInfoInteger(string sqlQuery, object parameters = null){
            int value =0;
            try{
                string connectionString = configuration.GetConnectionString("ProverbsDataBankConnection");
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
                               value = sqlDataReader.GetInt32(0);
                               return value;
                            }
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            } 
            return value;
        }
        public string ConsumeInfo(string sqlQuery, object parameters = null){
            string value="";
            try{
                string connectionString = configuration.GetConnectionString("ProverbsDataBankConnection");
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
                               value = sqlDataReader.GetString(0);
                               return value;
                            }
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            } 
            return value;
        }
    }
}