using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.RequestHelpers;
namespace API.Controllers;


public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{
  
  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePageResult(repo,spec,specParams.PageIndex,specParams.PageSize);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product is null) return NotFound();

        return product;
    }

    [HttpPost]

    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if (await repo.SaveAllAsync()) return CreatedAtAction("CreateProduct", new {id= product.Id}, product);
        return BadRequest("Product was not created");
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if(product.Id != id) return BadRequest("Id is not equal to the product's Id");
        bool found = repo.Exists(id);

        if (!found) return NotFound("Product was not found");

        repo.Update(product);

         if (await repo.SaveAllAsync()) return NoContent();

         return BadRequest("Product was not updated");
        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product is null ) return NotFound("Product was not found");

        repo.Remove(product);

        if (await repo.SaveAllAsync()) return NoContent();

         return BadRequest("Product was not deleted");
    }

    [HttpGet("brands")]

    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]

    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
    }


}
