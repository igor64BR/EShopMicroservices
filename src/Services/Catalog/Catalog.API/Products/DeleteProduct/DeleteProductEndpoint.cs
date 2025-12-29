
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products", async (
            [FromBody] DeleteProductRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<DeleteProductCommand>();

            await sender.Send(command, cancellationToken);

            return Results.NoContent();
        }).WithName("DeleteProduct")
        .WithSummary("Delete a Product")
        .WithDescription("Delete a Product based on the specified id")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
