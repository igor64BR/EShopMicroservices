
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;

public class DeleteProductHandler(
    IDocumentSession session,
    ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with ID {ProductId}", request.Id);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            logger.LogError("Product with ID {ProductId} not found", request.Id);
            throw new ProductNotFoundException();
        }

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new();
    }
}
