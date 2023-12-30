using System;

namespace MeloSolution.authenticationAPI.Repository{
    public interface ICud{
        public bool CudObject(string sqlQuery, object parameters = null);
    }
}