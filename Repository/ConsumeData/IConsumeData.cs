using System;

namespace MeloSolution.authenticationAPI.Repository{
    public interface IConsumeData{
        public string ConsumeInfo(string sqlQuery, object parameters = null);
        public int ConsumeInfoInteger(string sqlQuery, object parameters = null);
    }
}