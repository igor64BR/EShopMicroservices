namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(Guid? Id = null) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsHandler(
    IDocumentSession session,
    ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProducts Query for Id: {Id}", request.Id);

        var query = session.Query<Product>();

        var products = request.Id.HasValue
            ? await query.Where(x => x.Id == request.Id)
                .ToListAsync(cancellationToken)
            : await session.Query<Product>().ToListAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} products", products.Count);

        return new GetProductsResult(products);
    }
}
