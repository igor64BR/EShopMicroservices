
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (
            Guid id,
            ISender sender,
            ILogger<GetProductByIdEndpoint> logger) =>
        {
            logger.LogInformation("Received request to get product by Id: {Id}", id);

            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        }).WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product with specified id")
        .WithDescription("Retrieve the specified product from the database based on the provided id");
    }
}
