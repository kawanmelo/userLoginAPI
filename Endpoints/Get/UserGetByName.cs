using System.Data.SqlClient;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

/*
Este arquivo contém a lógica para lidar com a recuperação de usuários por meio de uma solicitação Get.
O endpoint UserGetByName interage com o banco de dados para recuperação do usuário.
*/
namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserGetByName{
        private static  IConsumeObject consumeObject;
        public static string TemplateUserByName => "/User/GetName/{name}";
        public static string[] MetodoUserByName => new string[] {HttpMethod.Get.ToString()};
        public static Delegate FuncUserByName => ActionUserByName;
        /*
        Busca um usuário no banco de dados por meio de uma solicitação GET usando o nome como critério de pesquisa.
        - Instancia o objeto responsável por consumir informações do banco de dados.
        - Executa a consulta no banco de dados para recuperar um usuário com o nome fornecido.
        - Retorna um resultado Ok com os detalhes do usuário se encontrado, ou NotFound se não encontrado.
        */
        public static IResult ActionUserByName([FromRoute] string name){
            try{
                consumeObject = new ConsumeUser();
                // Executa a consulta no banco de dados para recuperar um usuário com o nome fornecido.
                var userFound = consumeObject.ConsumeUniqueObject($"SELECT * FROM dbo.UserData WHERE Name = @Name",
                new{ Name = name });
                if(userFound != null){
                    // Retorna um resultado Ok (200 OK) com os detalhes do usuário se encontrado.
                    return Results.Ok(userFound);
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.autenticationAPI.Endpoints");
                Console.WriteLine(e.Message);
            }
            // Retorna um resultado NotFound (404 Not Found) se o usuário não for encontrado.
            return Results.NotFound();
        }
    }
}