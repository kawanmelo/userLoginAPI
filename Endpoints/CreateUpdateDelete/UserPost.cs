using MeloSolution.authenticationAPI.Entities;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserPost{
        private static ICud cud;
        private static IConsumeObject consumeObject;
        private static IConsumeData consumeData;
        public static string TemplateUserPost => "/User/";
        public static string[] MetodoUserPost => new string[] {HttpMethods.Post.ToString()};
        public static Delegate FuncUserPost => ActionUserPost;

        public static IResult ActionUserPost([FromBody] User user){
            try{
                cud = new CudUSer();
                bool persistenceConference = cud.CudObject("INSERT INTO dbo.UserData (Name, Email, Password) VALUES (@Name, @Email, @Password)",
                new {Name = user.Name, Email = user.Email, Password = user.Password});
                if(persistenceConference){
                    consumeData = new ConsumeData();
                    int ID = consumeData.ConsumeInfoInteger("SELECT MAX(UserId) FROM dbo.UserData");
                    consumeObject = new ConsumeUser();
                    var userFound = consumeObject.ConsumeUniqueObject($"SELECT * FROM dbo.UserData WHERE UserId = {ID}");
                    return Results.Created($"https://localhost:7120/advice/{ID}", userFound);
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.authenticationAPI.Endpoints.UserPost");
                Console.WriteLine(e.Message);
            }
            return Results.BadRequest();
        }
    }
}