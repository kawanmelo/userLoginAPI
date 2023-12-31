using System.Data.SqlClient;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

/*
Este arquivo contém a lógica para lidar com a recuperação de usuários por meio de uma solicitação Get.
O endpoint UserGetByEmail interage com o banco de dados para recuperação do usuário.
*/
namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserGetByEmail{
        private static  IConsumeObject consumeObject;
        public static string TemplateUserByEmail => "/User/GetEmail/{email}";
        public static string[] MetodoUserByEmail => new string[] {HttpMethod.Get.ToString()};
        public static Delegate FuncUserByEmail => ActionUserByEmail;
        /*
        Busca um usuário no banco de dados por meio de uma solicitação GET usando o email como critério de pesquisa.
        - Instancia o objeto responsável por consumir informações do banco de dados.
        - Executa a consulta no banco de dados para recuperar um usuário com o email fornecido.
        - Retorna um resultado Ok com os detalhes do usuário se encontrado, ou NotFound se não encontrado.
        */
        public static IResult ActionUserByEmail([FromRoute] string email){
            try{
                consumeObject = new ConsumeUser();
                // Executa a consulta no banco de dados para recuperar um usuário com o email fornecido.
                var userFound = consumeObject.ConsumeUniqueObject($"SELECT * FROM dbo.UserData WHERE Email LIKE '{email}'");
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