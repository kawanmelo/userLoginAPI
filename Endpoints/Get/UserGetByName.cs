using System.Data.SqlClient;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserGetByName{
        private static  IConsumeObject consumeObject;
        public static string TemplateUserByName => "/User/{name}";
        public static string[] MetodoUserByName => new string[] {HttpMethod.Get.ToString()};
        public static Delegate FuncUserByName => ActionUserByName;
    

        public static IResult ActionUserByName(string name){
            try{
                consumeObject = new ConsumeUser();
                var userFound = consumeObject.ConsumeUniqueObject($"SELECT * FROM dbo.UserData WHERE Name LIKE '{name}'");
                if(userFound != null){
                    return Results.Ok(userFound);
                }else{
                    return Results.NotFound();
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.autenticationAPI.Endpoints");
                Console.WriteLine(e.Message);
            }
            return Results.NotFound();
        }
    }
}