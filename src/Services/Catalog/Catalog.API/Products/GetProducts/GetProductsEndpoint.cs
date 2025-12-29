namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            Guid? id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductsQuery(id);
            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(result);
        }).WithName("GetProducts").Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products from db")
        .WithDescription("Get all products from database based on the query parameters");
    }
}
