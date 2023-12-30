using MeloSolution.authenticationAPI.Entities;
using MeloSolution.authenticationAPI.Repository;
using Microsoft.AspNetCore.Mvc;

/*
Este arquivo contém a lógica para lidar com a criação de usuários por meio de uma solicitação POST.
O endpoint UserPost interage com o banco de dados para persistir informações do usuário.
*/


namespace MeloSolution.authenticationAPI.Endpoints{
    public class UserPost{
        private static ICud cud;
        private static IConsumeObject consumeObject;
        private static IConsumeData consumeData;
        public static string TemplateUserPost => "/User/";
        public static string[] MetodoUserPost => new string[] {HttpMethods.Post.ToString()};
        public static Delegate FuncUserPost => ActionUserPost;

/*
    Realiza um POST no banco de um usuário passado no corpo da requisição.
    - Instancia o objeto para interação com o banco de dados.
    - Executa a interação com o banco de dados.
    - Verifica a persistência no banco e recupera o usuário criado se bem-sucedido.
    - Retorna um resultado apropriado com base na persistência bem-sucedida ou não.
*/
        public static IResult ActionUserPost([FromBody] User user){
            try{
                cud = new CudUSer();
                // Execução da interação com o banco de dados (recebe True se ao menos uma linha do banco foi afetada).
                bool persistenceConference = cud.CudObject("INSERT INTO dbo.UserData (Name, Email, Password) VALUES (@Name, @Email, @Password)",
                new {Name = user.Name, Email = user.Email, Password = user.Password});
                if(persistenceConference){
                    consumeData = new ConsumeData();
                    // Recupera o ID do usuário recém-criado no banco.
                    int ID = consumeData.ConsumeInfoInteger("SELECT MAX(UserId) FROM dbo.UserData");
                    consumeObject = new ConsumeUser();
                    // Recupera o usuário recém-criado no banco.
                    var userFound = consumeObject.ConsumeUniqueObject($"SELECT * FROM dbo.UserData WHERE UserId = {ID}");
                    // Retorna um resultado de criação com o URI do novo usuário e os detalhes do usuário.
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