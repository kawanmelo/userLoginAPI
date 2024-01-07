using MeloSolution.authenticationAPI.Repository;
using System.Data.SqlClient;
using System.Reflection;
namespace MeloSolution.authenticationAPI.Repository{
    public class CudUSer:ICud{
        private readonly IConfiguration configuration;
        public CudUSer(){
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }
        // Executa uma operação no banco de dados e retorna FALSE se NENHUMA coluna for afetada, senão retorna TRUE.
        public bool CudObject(string sqlQuery, object parameters = null){
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
                        if(sqlCommand.ExecuteNonQuery() < 1){
                            return false;
                        }else{
                            return true;
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}