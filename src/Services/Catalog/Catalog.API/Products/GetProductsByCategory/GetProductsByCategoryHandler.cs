
namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(
    IDocumentSession session,
    ILogger<GetProductsByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting products for category: {Category}", request.Category);

        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(request.Category))
            .ToListAsync(cancellationToken);

        logger.LogInformation("Found {Count} products for category: {Category}", products.Count, request.Category);

        return new(products);
    }
}
