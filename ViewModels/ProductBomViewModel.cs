using Acuity.Models;
using System.Collections.Generic;

namespace Acuity.ViewModels {
    public class ProductBomViewModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public IDictionary<string, int> Parts { get; set; }

        public ProductBomViewModel(Product product, List<Bom> boms) {
            Name = product.Name;
            Description = product.Description;
            ProductId = product.ProductId;

            IDictionary<string, int> parts = new Dictionary<string, int>();
            foreach (Bom bom in boms) {
                parts.Add(bom.Part.Name, bom.Quantity);
            }
            Parts = parts;

        }
    }
}
