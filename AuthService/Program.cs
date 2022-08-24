using AuthService;
using AuthService.Interfaces;
using AuthService.Services;

using Contracts;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig.SectionName));
builder.Services.AddSingleton<IAuthService, MyAuthenticationService>();
//builder.Services.AddAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();

app.MapPost(
    "/api/user/{username}/authenticate",
    ([FromServices] IAuthService service,
    [FromRoute] string username,
    [FromBody] LoginRequest request
    ) => {
        var result = service.AuthenticateUser(username, request.Password);

        return (result.IsError) ? Results.BadRequest() : Results.Ok(result.Value);
    }
 );


app.Run();