using AutoMapper;
using Grpc.Net.Client;
using GRPCCoreCrudExampleServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCCoreCrudExampleClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(ILogger<IndexModel> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public List<ProductModel> ProductsList { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new GRPCCoreCrudExampleServer.Product.ProductClient(channel);
                var productsRepsonse = client.GetProducts(new ProductMessage());
                ProductsList = _mapper.Map<List<ProductModel>>(productsRepsonse.Items);

                return Page();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message.ToString().Split(',').SingleOrDefault(a => a.Trim().StartsWith("Detail="));
                return Page();
            }
        }
    }
}
