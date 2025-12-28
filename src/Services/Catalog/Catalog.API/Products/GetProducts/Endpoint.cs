namespace Catalog.API.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            Guid? id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new Query(id);
            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(result);
        }).WithName("GetProducts").Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products from db")
        .WithDescription("Get all products from database based on the query parameters");
    }
}
