using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjUnitTesting.Models;
using ProjUnitTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUnitTests
{
    public class ShoppingCartServiceFake : IShoppingCartService
    {
        private readonly DataContext _context;
        public ShoppingCartServiceFake(DataContext context)
        {
            _context = context;
        }
        private readonly List<ShoppingItem> _shoppingCart;
        public ShoppingCartServiceFake()
        {
            _shoppingCart = new List<ShoppingItem>()
            {
                new ShoppingItem() { Id = 1,
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new ShoppingItem() { Id = 2,
                    Name = "Diary Milk", Manufacturer="Cow", Price = 4.00M },
                new ShoppingItem() { Id = 3,
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey", Price = 12.00M }
            };
        }


        public ShoppingItem Add(ShoppingItem newItem)
        {
            _shoppingCart.Add(newItem);
            return newItem;
            //var result = _context.Add(newItem);
            //_context.SaveChangesAsync();
            //return result.Entity;
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _context.ShoppingItem.ToList();
        }

        public ShoppingItem GetById(int id)
        {
            return _shoppingCart.Where(a => a.Id == id)
           .FirstOrDefault();
            //return _context.ShoppingItem.FirstOrDefault(m => m.Id == id);

        }

        public List<ShoppingItem> Remove(int Id)
        {
            var shoppingItem = _context.ShoppingItem.Find(Id);

            if (shoppingItem == null) throw new Exception($"Invalid ShoppingItem Id {shoppingItem?.Id}"); ;
            _context.ShoppingItem.Remove(shoppingItem);
            //_context.SaveChangesAsync();
            return (_context.ShoppingItem.ToList());
        }

        public ShoppingItem Update(ShoppingItem shoppingData)
        {
            if (shoppingData == null || shoppingData.Id == 0)
                throw new Exception("Bad Request");

            var shoppingItem = _context.ShoppingItem.Find(shoppingData.Id);
            if (shoppingItem == null)
                throw new Exception("shoppingItem Not Found");

            shoppingItem.Name = shoppingData.Name;
            shoppingItem.Price = shoppingData.Price;
            shoppingItem.Manufacturer = shoppingData.Manufacturer;

            return (shoppingItem);
        }
    }
}
