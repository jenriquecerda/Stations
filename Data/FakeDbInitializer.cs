using System;
using Acuity.Models;
using System.Linq;
using Bogus;

namespace Acuity.Data {
    public static class FakeDbInitializer {
        public static void Initialize(AcuityContext context) {
            context.Database.EnsureCreated();

            if (context.Part.Any()) {
                return;
            }

            var rawMaterials = new[] { "capacitor", "resistor", "PLC", "contactor", "switch" };

            for (int i = 0; i < rawMaterials.Length; i++) {
                context.Part.Add(
                    new Faker<Part>()
                    .RuleFor(p => p.Name, rawMaterials[i])
                    .RuleFor(p => p.Supplier, f => f.Company.CompanyName())
                );
            }

            var fakeProducts = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription()).Generate(10);

            foreach (var product in fakeProducts) {
                context.Product.Add(product);
            }

            context.SaveChanges();

            var products = context.Product.ToList();
            var parts = context.Part.ToList();
            foreach (Product product in products) {
                for (int i = 0; i < 3; i++) {
                    Bom bom = new Bom {
                        Quantity = new Random().Next(1, 11),
                        Part = parts[i],
                        ProductId = product.ProductId,
                    };
                    context.Bom.Add(bom);
                }
            }
            context.SaveChanges();

        }
    }
}
