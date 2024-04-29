using Core.Exceptions;
using Core.Users.Requests;
using MediatR;

namespace AuthenticationEP.Endpoints
{
    internal static class User
    {
        internal static void MapUserEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/user")
                           .WithTags("User");


            group.MapPost("/", async (CreateUser command, ISender sender) =>
            {
                try
                {
                    await sender.Send(command);
                    return Results.Created();
                }
                catch (ExistingRecordException e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Create a new user" });
        }
    }
}