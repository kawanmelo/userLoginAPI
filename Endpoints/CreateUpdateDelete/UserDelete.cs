using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

/*
Este arquivo contém a lógica para lidar com a exclusão de usuários por meio de uma solicitação Delete.
O endpoint UserDelete interage com o banco de dados para deletar informações do usuário.
*/
namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserDelete{
        private static ICud cud;
        public static string TemplateUserDelete => "/User/{Id:int}";
        public static string[] MetodoUserDelete => new string[] {HttpMethod.Delete.ToString()};
        public static Delegate FuncUserDelelte => ActionDeleteUser;

        /*
        Exclui um usuário do banco de dados por meio de uma solicitação DELETE.
        - Instancia o objeto para interação com o banco de dados.
        - Executa a interação com o banco de dados para excluir o usuário com o ID fornecido.
        - Verifica a persistência no banco e retorna um resultado apropriado com base na persistência bem-sucedida ou não.
        */
        public static IResult ActionDeleteUser([FromRoute] int id){
            try{
                cud = new CudUSer();
                // Executa a interação com o banco de dados para excluir o usuário com o ID fornecido.
                bool persistenceConference = cud.CudObject("DELETE FROM dbo.UserData WHERE UserId = @UserId",
                 new {UserId = id});
                if(persistenceConference){
                    // Retorna um resultado sem conteúdo (204 No Content) indicando sucesso na exclusão.
                    return Results.NoContent();
                }
            }catch(Exception e){
                Console.WriteLine("MeloSolution.authenticationAPI.Endpoints.UserDelete");
                Console.WriteLine(e.Message);
            }
            // Retorna um resultado de solicitação inválida (400 Bad Request) em caso de falha na exclusão.
            return Results.BadRequest();
        }
    }
}