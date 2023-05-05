using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjUnitTesting.Controllers;
using ProjUnitTesting.Models;
using ProjUnitTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUnitTests.Controller_Testing
{
    public class ShoppingCartControllerTest
    {
        private readonly Mock<IShoppingCartService> shoppingCartService;

        public ShoppingCartControllerTest()
        {
            shoppingCartService = new Mock<IShoppingCartService>();
        }
        private List<ShoppingItem> GetShoppingCartData()
        {
            List<ShoppingItem> shoppingItemData = new List<ShoppingItem>
        {
            new ShoppingItem
            {
                 Id= 1,
                 Name= "Bag",
                 Price= 543,
                 Manufacturer= "puma"
            },
             new ShoppingItem
            {
                 Id= 2,
                 Name= "Bat",
                 Price= 543,
                 Manufacturer= "puma"
            },
             new ShoppingItem
            {
                 Id= 3,
                 Name= "Ball",
                 Price= 543,
                 Manufacturer= "addidas"
            },
        };
            return shoppingItemData;
        }

        [Fact]
        public void GetAllItems_Get()
        {
            //arrange
            var shoppingCartList = GetShoppingCartData();
            shoppingCartService.Setup(x => x.GetAllItems())
                .Returns(shoppingCartList);
            var shoppingCartController = new ShoppingCartController(shoppingCartService.Object);


            //act
            var shoppingCartResult = shoppingCartController.Get();

            //assert
            Assert.NotNull(shoppingCartResult);
            Assert.Equal(GetShoppingCartData().Count(), shoppingCartResult.Count());
            Assert.Equal(GetShoppingCartData().ToString(), shoppingCartResult.ToString());
            Assert.True(shoppingCartList.Equals(shoppingCartResult));
        }
        [Fact]
        public void GetProductByID_Product()
        {
            //arrange
            var shoppingCartList = GetShoppingCartData();
            shoppingCartService.Setup(x => x.GetById(2))
                .Returns(shoppingCartList[1]);
            var shoppingCartController = new ShoppingCartController(shoppingCartService.Object);

            //act
            var shoppingCartResult = shoppingCartController.Get(2);

            //assert
            Assert.NotNull(shoppingCartResult);
            Assert.Equal(shoppingCartList[1].Id, shoppingCartResult.Id);
            Assert.True(shoppingCartList[1].Id == shoppingCartResult.Id);
        }

        [Fact]
        public void AddProduct_Product()
        {
            //arrange
            var shoppingCartList = GetShoppingCartData();
            shoppingCartService.Setup(x => x.Add(shoppingCartList[2]))
                .Returns(shoppingCartList[2]);
            var shoppingCartController = new ShoppingCartController(shoppingCartService.Object);

            //act
            var shoppingCartResult = shoppingCartController.Post(shoppingCartList[2]);

            //assert
            Assert.NotNull(shoppingCartResult);
            Assert.Equal(shoppingCartList[2].Id, shoppingCartResult.Id);
            Assert.True(shoppingCartList[2].Id == shoppingCartResult.Id);
        }

        [Theory]
        [InlineData("Bat")]
        public void CheckShoppingCardExistOrNotByName_Product(string Name)
        {
            //arrange
            var shoppingCartList = GetShoppingCartData();
            shoppingCartService.Setup(x => x.GetAllItems())
                .Returns(shoppingCartList);
            var shoppingCartController = new ShoppingCartController(shoppingCartService.Object);

            //act
            var shoppingCartResult = shoppingCartController.Get();
            var expectedShoppingCardName = shoppingCartResult.ToList()[1].Name;

            //assert
            Assert.Equal(Name, expectedShoppingCardName);
            //Assert.NotEqual(Name, expectedShoppingCardName);

        }
    }
}
