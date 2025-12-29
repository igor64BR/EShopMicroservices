
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductEndpointRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (
            UpdateProductEndpointRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateProductCommand>();

            await sender.Send(command, cancellationToken);

            return Results.NoContent();
        }).WithName("UpdateProduct")
        .WithSummary("Update a Product")
        .WithDescription("Updates a product given the specific id in request body")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
