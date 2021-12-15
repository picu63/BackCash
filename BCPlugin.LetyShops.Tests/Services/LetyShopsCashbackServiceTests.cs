using BCPlugin.LetyShops.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BC.Interfaces;
using BC.Models;
using MockQueryable.Moq;
using OpenQA.Selenium.Chrome;

namespace BCPlugin.LetyShops.Tests.Services;

[TestFixture]
public class LetyShopsCashbackServiceTests
{
    private MockRepository mockRepository;

    private Mock<HttpMessageHandler> mockHttpMessageHandler;
    private Mock<IBCContext> mockPluginDbContext;


    private readonly List<ShopUriAssociation> shopUriAssociations = new()
    {
        new ShopUriAssociation
        {
            Id = 1,
            Shop = new Shop(){Id = 1, Name = "CCC"},
            RelativePath = "shops/ccc-pl"
        },
        new ShopUriAssociation
        {
            Id = 2,
            Shop = new Shop(){Id = 2, Name = "Allegro"},
            RelativePath = "shops/allegro-pl"
        },
        new ShopUriAssociation
        {
            Id = 3,
            Shop = new Shop(){Id = 3, Name = "Pyszne.pl"},
            RelativePath = "shops/pyszne-pl"
        }
    };

    [SetUp]
    public void SetUp()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockHttpMessageHandler = this.mockRepository.Create<HttpMessageHandler>();
        this.mockPluginDbContext = this.mockRepository.Create<IBCContext>();
        mockPluginDbContext.Setup(context => context.ShopUriAssociations)
            .Returns(shopUriAssociations.AsQueryable().BuildMockDbSet().Object);
    }

    private LetyShopsCashbackService CreateService()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("headless"); // comment this to see browser
        return new LetyShopsCashbackService(
            this.mockPluginDbContext.Object, new ChromeDriver(chromeOptions), new HttpClient(){BaseAddress = new Uri("https://letyshops.com/pl")});
    }

    // Arguments in test case must be taken from original site
    [TestCase(1, 3.25, "ccc")]
    [TestCase(2, 1.45, "allegro")]
    [TestCase(3, 2, "pyszne")]
    public async Task ShouldReturnActualCashback(long id,decimal actualPromo, string shopName)
    {
        var shopId = id;
        // Arrange
        var service = this.CreateService();
        Shop shop = new Shop() { Id = shopId, Name = shopName };
        Category category = null;

        // Act
        var result = await service.GetCashback(
            shop,
            category);

        // Assert
        Assert.AreEqual(actualPromo, result.Amount);
    }
}