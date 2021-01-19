using System;

namespace GRPCCoreCrudExampleClient
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
