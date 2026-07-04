using Core.Entities;
using Core.Interfaces;
using Infrastructure.CartService;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class CartController(ICartService CartService) : BaseApiController
{
    [HttpGet]

    public async Task<ActionResult<ShoppingCart>> GetCardById (string id)
    {
        var cart = await CartService.GetCartAsync(id);
        return Ok(cart ?? new ShoppingCart{Id = id});
    }

    [HttpPost]
     public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var updatecart = await CartService.SetCartAsync(cart);
        if(updatecart == null) return BadRequest("problem creating cart");
        return updatecart;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var result = await CartService.DeleteCartAsync(id);
        if(!result) return BadRequest("problem deleting cart");
        return Ok();
    }

}