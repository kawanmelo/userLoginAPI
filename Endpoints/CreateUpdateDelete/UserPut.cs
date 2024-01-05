using MeloSolution.authenticationAPI.Entities;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

/*
Este arquivo contém a lógica para lidar com a atualização de usuários por meio de uma solicitação PUT.
O endpoint UserPut interage com o banco de dados para atualizar informações do usuário.
*/
namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserPut{
        private static ICud cud;
        public static string TemplateUserPut => "/User/";
        public static string[] MetodoUserPut => new string[] {HttpMethod.Put.ToString()};
        public static Delegate FuncUserPut => ActionUserPut;

        /*
        Atualiza um usuário no banco de dados por meio de uma solicitação PUT.
        - Instancia o objeto para interação com o banco de dados.
        - Executa a interação com o banco de dados para atualizar as informações do usuário.
        - Verifica a persistência no banco e retorna um resultado apropriado com base na persistência bem-sucedida ou não.
        */
        public static IResult ActionUserPut([FromBody] User user){
            try{
                cud = new CudUSer();
                // Executa a interação com o banco de dados para atualizar as informações do usuário.
                bool persistenceConference = cud.CudObject("UPDATE dbo.UserData SET Name = @Name, Email = @Email, Password = @Password WHERE UserId = @UserId",
                new {UserId = user.Id, Name = user.Name, Email = user.Email, Password = user.Password});
                if(persistenceConference){
                    // Retorna um resultado sem conteúdo (204 No Content) indicando sucesso na atualização.
                    return Results.NoContent();
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            // Retorna um resultado de solicitação inválida (400 Bad Request) em caso de falha na atualização.
            return Results.BadRequest();
        }
    }
}