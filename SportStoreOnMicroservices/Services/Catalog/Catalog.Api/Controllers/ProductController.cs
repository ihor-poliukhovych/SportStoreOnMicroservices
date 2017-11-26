using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Persistence.Context;
using Catalog.Api.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [Authorize(Roles = "test")]
    [Route("api/catalog/[controller]")]
    public class ProductController : Controller
    {
        private readonly CatalogDbContext _catalogContext;

        public ProductController(CatalogDbContext context)
        {
            _catalogContext = context;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<CatalogItem>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Items()
        {
            var items = await _catalogContext.CatalogItems.OrderBy(c => c.Name).ToListAsync();

            return Ok(items);
        }

        [HttpGet("items/{id:int}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogItem), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var item = await _catalogContext.CatalogItems.SingleOrDefaultAsync(ci => ci.Id == id);
            
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<CatalogItem>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CatalogTypes()
        {
            var items = await _catalogContext.CatalogTypes.ToListAsync();

            return Ok(items);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<CatalogItem>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CatalogBrands()
        {
            var items = await _catalogContext.CatalogBrands.ToListAsync();

            return Ok(items);
        }

        [HttpPut("items")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateProduct([FromBody] CatalogItem productToUpdate)
        {
            var catalogItem = await _catalogContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);
            if (catalogItem == null)
                return NotFound(new {Message = $"Item with id {productToUpdate.Id} not found."});

            var oldPrice = catalogItem.Price;
            var raiseProductPriceChangedEvent = oldPrice != productToUpdate.Price;

            catalogItem = productToUpdate;
            _catalogContext.CatalogItems.Update(catalogItem);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new {id = productToUpdate.Id}, null);
        }

        [HttpPost("items")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] CatalogItem product)
        {
            var item = new CatalogItem
            {
                CatalogBrandId = product.CatalogBrandId,
                CatalogTypeId = product.CatalogTypeId,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
            
            _catalogContext.CatalogItems.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new {id = item.Id}, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _catalogContext.CatalogItems.SingleOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();

            _catalogContext.CatalogItems.Remove(product);

            await _catalogContext.SaveChangesAsync();

            return NoContent();
        }
    }
}