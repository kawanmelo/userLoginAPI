using MeloSolution.authenticationAPI.Entities;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserPut{
        private static ICud cud;
        public static string TemplateUserPut => "/User/";
        public static string[] MetodoUserPut => new string[] {HttpMethod.Put.ToString()};
        public static Delegate FuncUserPut => ActionUserPut;

        public static IResult ActionUserPut([FromBody] User user){
            try{
                cud = new CudUSer();
                bool persistenceConference = cud.CudObject("UPDATE dbo.UserData SET Name = @Name, Email = @Email, Password = @Password WHERE UserId = @UserId",
                new {UserId = user.Id, Name = user.Name, Email = user.Email, Password = user.Password});
                if(persistenceConference){
                    return Results.NoContent();
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.authenticationAPI.Endpoints.UserPost");
                Console.WriteLine(e.Message);
            }
            return Results.BadRequest();
        }
    }
}