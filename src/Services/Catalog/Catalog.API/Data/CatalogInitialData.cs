using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        var hasData = await session.Query<Product>().AnyAsync(cancellation);

        if (hasData) return;

        session.Store(GetInitialData());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetInitialData()
    {
        return [
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000001"),
                Name = "Smartphone X1",
                Category = ["Electronics", "Mobile"],
                Description = "Compact smartphone with powerful camera and long battery life.",
                ImageFile = "smartphone-x1.png",
                Price = 699.99m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000002"),
                Name = "Laptop Pro 14",
                Category = ["Electronics", "Computers"],
                Description = "14-inch professional laptop with high-performance CPU and SSD.",
                ImageFile = "laptop-pro-14.png",
                Price = 1299.00m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000003"),
                Name = "Wireless Headphones",
                Category = ["Electronics", "Audio"],
                Description = "Noise-cancelling over-ear wireless headphones with 30h battery.",
                ImageFile = "wireless-headphones.png",
                Price = 199.50m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000004"),
                Name = "4K Monitor 27\"",
                Category = ["Electronics", "Displays"],
                Description = "27-inch 4K IPS monitor with HDR support and USB-C.",
                ImageFile = "4k-monitor-27.png",
                Price = 449.99m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000005"),
                Name = "Mechanical Keyboard",
                Category = ["Accessories", "Computers"],
                Description = "Tactile mechanical keyboard with customizable RGB lighting.",
                ImageFile = "mechanical-keyboard.png",
                Price = 119.00m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000006"),
                Name = "Smartwatch S3",
                Category = ["Wearables", "Electronics"],
                Description = "Health-tracking smartwatch with GPS and water resistance.",
                ImageFile = "smartwatch-s3.png",
                Price = 249.99m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000007"),
                Name = "Bluetooth Speaker",
                Category = ["Audio", "Electronics"],
                Description = "Portable Bluetooth speaker with deep bass and 12h playtime.",
                ImageFile = "bluetooth-speaker.png",
                Price = 89.95m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000008"),
                Name = "Gaming Mouse",
                Category = ["Accessories", "Gaming"],
                Description = "Ergonomic gaming mouse with adjustable DPI and programmable buttons.",
                ImageFile = "gaming-mouse.png",
                Price = 59.99m
            },
            new Product
            {
                Id = Guid.Parse("b1a1f8a0-0c1d-4f6a-9b2a-000000000009"),
                Name = "External SSD 1TB",
                Category = ["Storage", "Computers"],
                Description = "USB-C external SSD with 1TB capacity and fast transfer speeds.",
                ImageFile = "external-ssd-1tb.png",
                Price = 139.99m
            },
        ];
    }
}
