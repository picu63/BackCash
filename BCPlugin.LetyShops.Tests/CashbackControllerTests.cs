using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Interfaces;
using BCModels;
using BCPlugin.Interfaces.Api;
using BCPlugin.Interfaces.Services;
using BCPluginLetyShops.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace BCPlugin.LetyShops.Tests
{
    public class CashbackControllerTests
    {
        private Mock<IBCContext> _context;
        private Mock<ICashbackService> _service;
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
            _context = new Mock<IBCContext>(MockBehavior.Strict);
            _context.Setup(c => c.Categories).Returns(categoriesData.AsQueryable().BuildMockDbSet().Object);
            _context.Setup(c => c.Shops).Returns(shopsData.AsQueryable().BuildMockDbSet().Object);

            _service = new Mock<ICashbackService>(MockBehavior.Loose);
        }

        [Test]
        public async Task ShouldReturnCashbackPercentage()
        {
            var shopId = shopsData[0].Id;
            var categoryId = categoriesData[0].Id;
            _service.Setup(s => s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>())).Returns(2);

            var cashbackController = new CashbackController(_context.Object, _service.Object);

            var cashback = await cashbackController.GetCashback(shopId, categoryId);

            Assert.Greater(cashback, 0);
            Assert.AreEqual(2, cashback);
            _context.Verify(c=>c.Shops, Times.Once);
            _context.Verify(c=>c.Categories, Times.Once);
            _service.Verify(s=>s.GetCashback(It.IsAny<Shop>(), It.IsAny<Category>()), Times.Once);
        }
    }
}