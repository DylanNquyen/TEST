using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLTW_BT3.Models;
using THLTW_BT3.Extensions;


public class ShoppingController : Controller
{
    private readonly ApplicationDbContext _context;
    private const string CartSessionKey = "cart";

    public ShoppingController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lấy giỏ hàng từ session hoặc tạo mới

    private ShoppingCart GetCart()
    {
        var cart = HttpContext.Session.GetJson<ShoppingCart>(CartSessionKey);
        if (cart == null)
        {
            cart = new ShoppingCart
            {
                UserId = User.Identity?.Name,
                CreatedAt = DateTime.Now
            };
            HttpContext.Session.SetJson(CartSessionKey, cart);
        }
        return cart;
    }
}

