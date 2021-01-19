using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GRPCCoreCrudExampleClient.Pages
{
    public class EditProductModel : PageModel
    {

        public JsonResult OnGetEditProduct(int Id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GRPCCoreCrudExampleServer.Product.ProductClient(channel);
            var product = client.GetProduct(new GRPCCoreCrudExampleServer.ProductMessage() { Id = Id });

            return new JsonResult(product, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}
