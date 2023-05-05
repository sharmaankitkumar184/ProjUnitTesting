using Microsoft.AspNetCore.Mvc;
using ProjUnitTesting.Models;

namespace ProjUnitTesting.Services
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem GetById(int id);
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem Update(ShoppingItem shoppingData);
        List<ShoppingItem> Remove(int Id);
    }
}
