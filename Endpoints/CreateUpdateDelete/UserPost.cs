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
                consumeData = new ConsumeData();
                int verificationName = consumeData.ConsumeInfoInteger("SELECT UserId FROM dbo.UserData WHERE Name = @Name ",
                new{ Name = user.Name });
                int verificationEmail = consumeData.ConsumeInfoInteger("SELECT UserId FROM dbo.UserData WHERE Email = @Email ",
                new{ Email = user.Email });
                if(verificationName > 0){
                    return Results.Conflict("User creation failed, the username provided already exists.");
                }
                if(verificationEmail > 0){
                    return Results.Conflict("User creation failed, the email provided already exists.");
                }
                cud = new CudUSer();
                // Execução da interação com o banco de dados (recebe True se ao menos uma linha do banco foi afetada).
                bool persistenceConference = cud.CudObject("INSERT INTO dbo.UserData (Name, Email, Password) VALUES (@Name, @Email, @Password)",
                new {Name = user.Name, Email = user.Email, Password = user.Password});
                if(persistenceConference){
                    // Recupera o Id do usuário recém-criado no banco.
                    int Id = consumeData.ConsumeInfoInteger("SELECT MAX(UserId) FROM dbo.UserData");
                    consumeObject = new ConsumeUser();
                    // Recupera o usuário recém-criado no banco.
                    var userFound = consumeObject.ConsumeUniqueObject("SELECT * FROM dbo.UserData WHERE UserId = @UserId",
                    new{ UserId = Id });
                    // Retorna um resultado de criação com o URI do novo usuário e os detalhes do usuário.
                    return Results.Created($"https://localhost:7120/advice/{Id}", userFound);
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            return Results.BadRequest();
        }
    }
}