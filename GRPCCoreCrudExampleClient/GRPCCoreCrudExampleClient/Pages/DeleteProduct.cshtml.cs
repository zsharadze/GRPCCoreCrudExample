using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GRPCCoreCrudExampleClient.Pages
{
    public class DeleteProductModel : PageModel
    {
        public class BindModel
        {
            public int Id { get; set; }
        }

        public void OnGet()
        {
        }

        public JsonResult OnPostDeleteProduct([FromBody] BindModel model)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GRPCCoreCrudExampleServer.Product.ProductClient(channel);
            client.DeleteProduct(new GRPCCoreCrudExampleServer.ProductMessage() { Id = model.Id });

            return new JsonResult("Success");
        }
    }
}
