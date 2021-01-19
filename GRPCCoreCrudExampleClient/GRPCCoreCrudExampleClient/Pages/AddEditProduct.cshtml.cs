using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Net.Client;
using GRPCCoreCrudExampleServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GRPCCoreCrudExampleClient.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly IMapper _mapper;

        public AddProductModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        [BindProperty]
        public ProductModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Input.Name))
                return RedirectToPage("Index");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GRPCCoreCrudExampleServer.Product.ProductClient(channel);
            var product = _mapper.Map<ProductMessage>(Input);

            if (Input.Id == 0)
                await client.AddProductAsync(product);
            else
                await client.UpdateProductAsync(product);

            return RedirectToPage("Index");
        }
    }
}
