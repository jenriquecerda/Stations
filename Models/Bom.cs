
namespace Acuity.Models {
    public class Bom {
        public int BomId { get; set; }
        public int Quantity { get; set; }

        public int PartId { get; set; }
        public Part Part { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
