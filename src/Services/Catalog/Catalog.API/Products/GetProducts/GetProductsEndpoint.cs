namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int PageNumber = 1, int PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products, int PageCount, int PageNumber, int TotalItemCount);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            [AsParameters] GetProductsRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        }).WithName("GetProducts").Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products from db")
        .WithDescription("Get all products from database based on the query parameters");

        TypeAdapterConfig<GetProductsResult, GetProductsResponse>.NewConfig()
            .Map(dest => dest.PageCount, src => src.Products.PageCount)
            .Map(dest => dest.PageNumber, src => src.Products.PageNumber)
            .Map(dest => dest.TotalItemCount, src => src.Products.TotalItemCount);

    }
}
