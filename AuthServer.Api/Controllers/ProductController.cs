using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ProjectBaseController
    {
        private readonly IService<Product, ProductDto> _service;

        public ProductController(IService<Product, ProductDto> service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetProducts() 
        {
            return ActionResultInstance(await _service.GetAllAsync());
        }
        public async Task<IActionResult> GetById(ProductDto productDto)
        {
            return ActionResultInstance(await _service.GetByIdAsync(productDto.Id));
        }
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _service.AddAsync(productDto));
        }
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _service.Update(productDto, productDto.Id));
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionResultInstance(await _service.Remove(id));
        }

    }
}
