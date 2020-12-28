using Acuity.Models;
using System.Linq;
using Bogus;

namespace Acuity.Data {
    public static class FakeDbInitializer {
        public static void Initialize(AcuityContext context) {
            context.Database.EnsureCreated();

            if (context.Parts.Any()) {
                return;
            }

            var rawMaterials = new[] { "capacitor", "resistor", "PLC", "contactor", "switch" };

            for (int i = 0; i < rawMaterials.Length; i++) {
                context.Parts.Add(
                    new Faker<Part>()
                    .RuleFor(p => p.Name, rawMaterials[i])
                    .RuleFor(p => p.Supplier, f => f.Company.CompanyName())
                );
            }

            var fakeProducts = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription()).Generate(10);

            foreach(var product in fakeProducts){
                context.Product.Add(product);
            }

            context.SaveChanges();
        }
    }
}
