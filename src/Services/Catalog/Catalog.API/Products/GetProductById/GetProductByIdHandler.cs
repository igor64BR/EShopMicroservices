namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(
    IDocumentSession session,
    ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductById Query for Id: {Id}", request.Id);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            logger.LogWarning("Product with Id: {Id} not found", request.Id);
            throw new ProductNotFoundException();
        }

        return new(product);
    }
}
