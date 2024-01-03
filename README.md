Projeto de API CRUD em C#
Descrição
Este é um projeto pessoal de uma API em C# que realiza operações CRUD (Create, Read, Update, Delete) em um banco de dados. A API gerencia informações de usuários, permitindo a criação, recuperação, atualização e exclusão de registros.

Estrutura do Projeto
Endpoints
UserGetByEmail
Endpoint: /User/GetEmail/{email}
Método HTTP: GET
Função: Recupera um usuário por meio do email.
UserGetByName
Endpoint: /User/GetName/{name}
Método HTTP: GET
Função: Recupera um usuário por meio do nome.
UserGetById
Endpoint: /User/GetId/{Id:int}
Método HTTP: GET
Função: Recupera um usuário por meio do ID.
UserDelete
Endpoint: /User/{Id:int}
Método HTTP: DELETE
Função: Exclui um usuário por meio do ID.
UserPost
Endpoint: /User/
Método HTTP: POST
Função: Cria um novo usuário.
UserPut
Endpoint: /User/
Método HTTP: PUT
Função: Atualiza as informações de um usuário.
Classes
User
Propriedades:
Id
Name
Email
Password
UserDelete
Descrição: Lógica para exclusão de usuários.
UserPost
Descrição: Lógica para criação de usuários.
UserPut
Descrição: Lógica para atualização de usuários.
UserGetByEmail
Descrição: Lógica para recuperação de usuários por email.
UserGetById
Descrição: Lógica para recuperação de usuários por ID.
UserGetByName
Descrição: Lógica para recuperação de usuários por nome.
Repository
ConsumeData
Descrição: Lógica para consumir dados do banco.
CudUser
Descrição: Lógica para operações CUD (Create, Update, Delete) no banco.
Repository/CreateObject
CreateUser
Descrição: Lógica para criação de instância de usuário.
Autor
Kawan Melo

