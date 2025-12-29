

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand;

public class UpdateProductHandler(
    IDocumentSession session,
    ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand>
{
    async Task<Unit> IRequestHandler<UpdateProductCommand, Unit>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with ID {ProductId}", request.Id);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            logger.LogError("Product with ID {ProductId} not found", request.Id);
            throw new ProductNotFoundException();
        }

        request.Adapt(product);

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new();
    }
}
