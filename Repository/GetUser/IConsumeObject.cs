using System.Data.SqlClient;

namespace MeloSolution.authenticationAPI.Repository{
    public interface IConsumeObject{
        public object ConsumeUniqueObject(string sqlQuery, object parameters = null);
        public List<object> ConsumeMultipleObject(string sqlQuery, object parameters = null);
    }
}