using MeloSolution.authenticationAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMethods(UserGetByEmail.TemplateUserByEmail, UserGetByEmail.MetodoUserByEmail, UserGetByEmail.FuncUserByEmail);
app.MapMethods(UserGetByName.TemplateUserByName, UserGetByName.MetodoUserByName, UserGetByName.FuncUserByName);
app.MapMethods(UserGetById.TemplateUserById, UserGetById.MetodoUserById, UserGetById.FuncUserById);
app.MapMethods(UserDelete.TemplateUserDelete, UserDelete.MetodoUserDelete, UserDelete.FuncUserDelelte);
app.MapMethods(UserPost.TemplateUserPost, UserPost.MetodoUserPost, UserPost.FuncUserPost);
app.MapMethods(UserPut.TemplateUserPut, UserPut.MetodoUserPut, UserPut.FuncUserPut);

app.Run();
