namespace Catalog.API.Products.GetProducts;

public record Query(Guid? Id = null) : IQuery<Result>;

public record Result(IEnumerable<Product> Products);

public class Handler(
    IDocumentSession session,
    ILogger<Handler> logger) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProducts Query for Id: {Id}", request.Id);

        var query = session.Query<Product>();

        var products = request.Id.HasValue
            ? await query.Where(x => x.Id == request.Id)
                .ToListAsync(cancellationToken)
            : await session.Query<Product>().ToListAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} products", products.Count);

        return new Result(products);
    }
}
