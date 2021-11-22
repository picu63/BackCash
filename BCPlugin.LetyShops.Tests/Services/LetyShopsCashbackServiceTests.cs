using BCPlugin.Interfaces.Repositories;
using BCPlugin.LetyShops.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.Models;
using BCPlugin.Models;
using MockQueryable.Moq;
using OpenQA.Selenium.Chrome;

namespace BCPlugin.LetyShops.Tests.Services;

[TestFixture]
public class LetyShopsCashbackServiceTests
{
    private MockRepository mockRepository;

    private Mock<HttpMessageHandler> mockHttpMessageHandler;
    private Mock<IPluginDbContext> mockPluginDbContext;

    private readonly List<ShopUriAssociation> shopUriAssociations = new()
    {
        new ShopUriAssociation
        {
            ShopId = 1,
            RelativePath = "shops/ccc-pl"
        },
        new ShopUriAssociation
        {
            ShopId = 2,
            RelativePath = "shops/allegro-pl"
        },
        new ShopUriAssociation
        {
            ShopId = 3,
            RelativePath = "shops/pyszne-pl"
        }
    };

    [SetUp]
    public void SetUp()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockHttpMessageHandler = this.mockRepository.Create<HttpMessageHandler>();
        this.mockPluginDbContext = this.mockRepository.Create<IPluginDbContext>();
        mockPluginDbContext.Setup(context => context.ShopUriAssociations)
            .Returns(shopUriAssociations.AsQueryable().BuildMockDbSet().Object);
    }

    private LetyShopsCashbackService CreateService()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("headless"); // comment this to see browser
        return new LetyShopsCashbackService(
            this.mockPluginDbContext.Object, new ChromeDriver(chromeOptions));
    }

    // Arguments in test case must be taken from original site
    [TestCase(1, 3.25, "ccc")]
    [TestCase(2, 1.45, "allegro")]
    [TestCase(3, 2, "pyszne")]
    public async Task ShouldReturnActualCashback(long id,decimal actualPromo, string shopName)
    {
        var guidCast = id;
        // Arrange
        var service = this.CreateService();
        Shop cccShop = new Shop() { Id = guidCast, Name = shopName };
        Category category = null;

        // Act
        var result = await service.GetCashback(
            cccShop,
            category);

        // Assert
        Assert.AreEqual(actualPromo, result.Amount);
    }
}