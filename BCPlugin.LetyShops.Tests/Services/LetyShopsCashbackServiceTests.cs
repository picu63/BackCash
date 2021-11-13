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
            ShopId = new Guid("63bc0a4b-e878-4f69-901d-3cd540c8224f"),
            RelativePath = "shops/ccc-pl"
        },
        new ShopUriAssociation
        {
            ShopId = new Guid("a2ae6acb-46cf-40a8-9353-c247410373a9"),
            RelativePath = "shops/allegro-pl"
        },
        new ShopUriAssociation
        {
            ShopId = new Guid("be6d93a3-bbdb-4dee-862c-98b7fa407f7d"),
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
#if !DEBUG
        chromeOptions.AddArgument("headless");
#endif
        return new LetyShopsCashbackService(
            this.mockPluginDbContext.Object, new ChromeDriver(chromeOptions));
    }

    // Arguments in testcase must be taken from original site
    [TestCase("63bc0a4b-e878-4f69-901d-3cd540c8224f", 3.25, "ccc")]
    [TestCase("a2ae6acb-46cf-40a8-9353-c247410373a9", 1.45, "allegro")]
    [TestCase("be6d93a3-bbdb-4dee-862c-98b7fa407f7d", 2, "pyszne")]
    public async Task ShouldReturnActualCashback(string id,decimal actualPromo, string shopName)
    {
        var guidCast = new Guid(id);
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