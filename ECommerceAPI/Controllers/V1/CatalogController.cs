using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
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
        public CatalogController(IBaseService<Catalog, CatalogVM> catalogService)
        {
            _catalogService = catalogService;
        }
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
    }
}
