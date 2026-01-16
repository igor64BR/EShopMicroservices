

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 256).WithMessage("Name must have between 2 and 256 characters");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductHandler(
    IDocumentSession session) : ICommandHandler<UpdateProductCommand>
{
    async Task<Unit> IRequestHandler<UpdateProductCommand, Unit>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        request.Adapt(product);

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new();
    }
}
