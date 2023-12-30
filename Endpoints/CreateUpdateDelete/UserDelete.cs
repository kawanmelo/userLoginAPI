using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserDelete{
        private static ICud cud;
        public static string TemplateUserDelete => "/User/{Id:int}";
        public static string[] MetodoUserDelete => new string[] {HttpMethod.Delete.ToString()};
        public static Delegate FuncUserDelelte => ActionDeleteUser;

        public static IResult ActionDeleteUser([FromRoute] int id){
            try{
                cud = new CudUSer();
                bool persistenceConference = cud.CudObject("DELETE FROM dbo.UserData WHERE UserId = @UserId",
                 new {UserId = id});
                if(persistenceConference){
                    return Results.NoContent();
                }else{
                    return Results.BadRequest();
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.authenticationAPI.Endpoints.UserDelete");
                Console.WriteLine(e.Message);
            }
            return Results.BadRequest();
        }
    }
}