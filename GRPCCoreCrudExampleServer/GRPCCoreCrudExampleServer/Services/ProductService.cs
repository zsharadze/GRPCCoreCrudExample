using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCCoreCrudExampleServer
{
    public class ProductService : Product.ProductBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ILogger<ProductService> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async override Task<ProductsResponce> GetProducts(ProductMessage request, ServerCallContext context)
        {
            var products = _mapper.Map<ProductsResponce>(await _context.Products.ToListAsync());

            return products;
        }

        public async override Task<ProductMessage> GetProduct(ProductMessage request, ServerCallContext context)
        {
            var product = _mapper.Map<ProductMessage>(await _context.Products.FindAsync(request.Id));

            return product;
        }

        public async override Task<ProductMessage> AddProduct(ProductMessage request, ServerCallContext context)
        {
            var product = _mapper.Map<ProductModel>(request);
            product.CreatedDate = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productMessage = _mapper.Map<ProductMessage>(product);
            return productMessage;
        }

        public async override Task<ProductMessage> UpdateProduct(ProductMessage request, ServerCallContext context)
        {
            var product = _mapper.Map<ProductModel>(request);
            _context.Attach(product);
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            var productMessage = _mapper.Map<ProductMessage>(product);
            return productMessage;
        }

        public async override Task<ProductMessage> DeleteProduct(ProductMessage request, ServerCallContext context)
        {
            var productToDel = _context.Products.Find(request.Id);
            _context.Products.Remove(productToDel);
            await _context.SaveChangesAsync();

            return new ProductMessage();
        }
    }
}