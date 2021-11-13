using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Interfaces;
using BC.Models;
using BCPlugin.Interfaces.Services;
using BCPlugin.LetyShops.Controllers;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace BCPlugin.LetyShops.Tests.Controllers
{
    public class CashbackControllerTests
    {
        private Mock<IBCContext> context;
        private Mock<ICashbackService> service;
        private readonly List<Category> categoriesData = new()
        {
            new Category { Id = Guid.NewGuid() },
            new Category { Id = Guid.NewGuid() },
            new Category { Id = Guid.NewGuid() }
        };

        private readonly List<Shop> shopsData = new()
        {
            new Shop { Id = Guid.NewGuid()},
            new Shop { Id = Guid.NewGuid()},
            new Shop { Id = Guid.NewGuid()}
        };

        [SetUp]
        public void Setup()
        {
            context = new Mock<IBCContext>(MockBehavior.Strict);
            context.Setup(c => c.Categories).Returns(categoriesData.AsQueryable().BuildMockDbSet().Object);
            context.Setup(c => c.Shops).Returns(shopsData.AsQueryable().BuildMockDbSet().Object);

            service = new Mock<ICashbackService>(MockBehavior.Loose);
        }

        [Test]
        public async Task ShouldReturnCashback()
        {
            var shopId = shopsData[0].Id;
            var categoryId = categoriesData[0].Id;

            service.Setup(s => s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>()))
                .Returns(Task.FromResult(new Cashback() { Amount = 2.4m, Type = CashbackType.Cash }));

            var cashbackController = new CashbackController(context.Object, service.Object);

            var cashback = (await cashbackController.GetCashback(shopId, categoryId)).Value;

            Assert.AreEqual(2.4m, cashback.Amount);
            Assert.AreEqual(CashbackType.Cash, cashback.Type);
            context.Verify(c=>c.Shops, Times.Once);
            context.Verify(c=>c.Categories, Times.Once);
            service.Verify(s=>s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>()), Times.Once);
        }

        [Test]
        public async Task NotExistingShopShouldReturnNotFound()
        {
            var shopId = Guid.NewGuid();
            var categoryId = categoriesData[0].Id;

            var cashbackController = new CashbackController(context.Object, service.Object);

            var result = await cashbackController.GetCashback(shopId, categoryId);
            Assert.That(result.Result is NotFoundObjectResult);
            context.Verify(c => c.Shops, Times.Once);
            service.Verify(s => s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>()), Times.Never);
        }
        [Test]
        public async Task NotExistingCategoryShouldReturnNotFound()
        {
            var shopId = shopsData[0].Id;
            var categoryId = Guid.NewGuid();

            var cashbackController = new CashbackController(context.Object, service.Object);

            var result = await cashbackController.GetCashback(shopId, categoryId);
            Assert.That(result.Result is NotFoundObjectResult);
            context.Verify(c => c.Shops, Times.Once);
            service.Verify(s => s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>()), Times.Never);
        }
    }
}