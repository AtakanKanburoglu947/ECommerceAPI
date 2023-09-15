using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IBaseService<Catalog, CatalogVM> _catalogService;
        private readonly IProductsByCatalogService _productsByCatalogService;
        public CatalogController(IBaseService<Catalog, CatalogVM> catalogService, IProductsByCatalogService productsByCatalogService)
        {
            _catalogService = catalogService;
            _productsByCatalogService = productsByCatalogService;
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("Add-Catalog")]
        public async Task<IActionResult> Add([FromBody] CatalogVM CatalogVM)
        {

            try
            {
                Task<Catalog> newCatalog = _catalogService.Add(CatalogVM);

                return Ok(await newCatalog);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-All-Catalogs")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _catalogService.GetAll());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _catalogService.GetById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetCatalogByName(string name)
        {
            try
            {
                return Ok(await _catalogService.Filter(x => x.Name == name));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("Update-Catalog")]
        public async Task<IActionResult> UpdateCatalog([FromBody] Catalog Catalog)
        {

            try
            {
                await _catalogService.Update(Catalog);
                return Ok(Catalog);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("Delete-Catalog")]
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            try
            {
                await _catalogService.Delete(id);
                return Ok($"Deleted the Catalog with id: {id}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-Products-By-Catalog-Name")]
        public IActionResult GetProductsByCatalogName(string catalogName) {
            try
            {
                return Ok(_productsByCatalogService.GetProductsByCatalogName(catalogName));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        
        }
    }
}
