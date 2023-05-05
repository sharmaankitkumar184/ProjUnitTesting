using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjUnitTesting.Models;

namespace ProjUnitTesting.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly DataContext _context;
        public ShoppingCartService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _context.ShoppingItem.ToList();
        }

        public ShoppingItem GetById(int id)
        {
            var shoppingCart = _context.ShoppingItem.FirstOrDefault(m => m.Id == id);
            if (shoppingCart == null)
                throw new Exception("ShoppingCart not found");
            return shoppingCart;
        }

        public ShoppingItem Add(ShoppingItem newItem)
        {
            var result = _context.Add(newItem);
            _context.SaveChangesAsync();
            return result.Entity;
        }

        public ShoppingItem Update(ShoppingItem shoppingData)
        {
            if (shoppingData == null || shoppingData.Id == 0)
                throw new Exception("Bad Request");

            var shoppingItem = _context.ShoppingItem.Find(shoppingData.Id);
            if (shoppingItem == null)
                throw new Exception("shoppingItem Not Found");

            shoppingItem.Name= shoppingData.Name;
            shoppingItem.Price = shoppingData.Price;
            shoppingItem.Manufacturer= shoppingData.Manufacturer;


            _context.SaveChangesAsync();
            return (shoppingItem);
        }
        public List<ShoppingItem> Remove(int Id)
        {
            var shoppingItem = _context.ShoppingItem.Find(Id);

            if (shoppingItem == null) throw new Exception($"Invalid ShoppingItem Id {shoppingItem?.Id}"); ;
            _context.ShoppingItem.Remove(shoppingItem);
            _context.SaveChangesAsync();
            return (_context.ShoppingItem.ToList());
        }
    }
}
