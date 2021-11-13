using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BC.Models;
using BCPlugin.Interfaces.Repositories;
using BCPlugin.Interfaces.Services;
using OpenQA.Selenium;

namespace BCPlugin.LetyShops.Services;

public class LetyShopsCashbackService : ICashbackService
{
    private readonly IPluginDbContext context;
    private readonly IWebDriver driver;

    public LetyShopsCashbackService(IPluginDbContext context, IWebDriver driver)
    {
        this.context = context;
        this.driver = driver;
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri("https://letyshops.com/pl/")
        };
    }

    public HttpClient HttpClient { get; }

    public async Task<Cashback> GetCashback(Shop shop, Category category)
    {
        var shopUri = context.ShopUriAssociations.SingleOrDefault(a => a.ShopId == shop.Id)?.RelativePath;
        if (shopUri is null)
        {
            return null;
        }
        var uri = new Uri(HttpClient.BaseAddress, shopUri);
        driver.Navigate().GoToUrl(uri);
        var innerText = driver.FindElementOrDefault(By.ClassName("b-shop-teaser__cash"))?.Text;
        if (innerText is null)
        {
            innerText = driver.FindElementOrDefault(By.ClassName("b-shop-teaser__new-cash"))?.Text;
        }
        // format 'd,dd %' || 'd,dd {currency code}'
        var amount = GetAmountFromString(innerText);
        var type = GetCashBackTypeFromString(innerText);
        return await Task.FromResult(new Cashback() {Amount = amount, Type = type});
    }

    private decimal GetAmountFromString(string text)
    {
        var regex = new Regex(@"\b\d+([\.,]\d+)?"); //finds all numbers with , or . decimal character
        text = regex.Matches(text).First().Value.Replace('.', ',');
        return decimal.Parse(text);
    }

    private CashbackType GetCashBackTypeFromString(string text)
    {
        return text.Trim().EndsWith('%') ? CashbackType.Percentage : CashbackType.Cash;
    }
}

public static class SeleniumHelperExtensions
{
    public static IWebElement FindElementOrDefault(this ISearchContext driver, By by)
    {
        try
        {
            return driver.FindElement(by);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}