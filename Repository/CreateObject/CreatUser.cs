using MeloSolution.authenticationAPI.Entities;

namespace MeloSolution.Repository.CreateObject{
    public class CreateUser{
        public User Create(int Id, string Name, string Email, string Password){
            User user = new User(Id, Name, Email, Password);
            return user;
        }
    }
}