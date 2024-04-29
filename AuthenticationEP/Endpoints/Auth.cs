using AuthenticationEP.Models;
using Core.Auth.Requests;
using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationEP.Endpoints
{
    internal static class Auth
    {
        internal static void MapAuthEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/auth")
                           .WithTags("Authentication");


            group.MapPost("/login", async (Login command, ISender sender) =>
            {
                try
                {
                    var result = await sender.Send(command);

                    if(!result.Item1){ return Results.BadRequest("Incorrect username or password"); }

                    var token = await sender.Send(new Token(command.Username, result.Item2, app.Configuration["Jwt:Key"]));

                    return Results.Ok(token);
                }
                catch (NotFoundException e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Authenticate user and generate JWT" });


            group.MapPost("/refresh", async (RefreshTokenRequest request, ISender sender) =>
            {
                try
                {
                    var result = await sender.Send(new Refresh(request.RefreshToken, request.Username, app.Configuration["Jwt:Key"]));
                    
                    return Results.Ok(result);
                }
                catch (NotFoundException)
                {
                    return Results.BadRequest("Invalid refresh token");
                }
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Generate new access and refresh tokens" });


            group.MapPost("/logout", async (Logout command, ISender sender) =>
            {
                try
                {
                    await sender.Send(command);
                    
                    return Results.Ok();
                }
                catch (NotFoundException)
                {
                    return Results.BadRequest("Invalid credentials");
                }
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Logout user" });
        }
    }
}