using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TravelsTesting.Pages
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "dropdown-login")]
        private IWebElement ddMyAccaunt;

        [FindsBy(How = How.LinkText, Using = "Login")]
        private IWebElement lnkLogin;

        [FindsBy(How = How.XPath, Using = "//*[@id='MenuHorizon28_01']//*[@class='price']//parent::*[@class='content clearfix']")]
        private IList<IWebElement> lstFeaturedHotels;

        private void ClickMyAccaunt() 
        {
            ddMyAccaunt.Click();
        }

        public int GetTheCheapestFeaturedHotelsPrice()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("HOTELS")));
            var lstPrices = lstFeaturedHotels
                .Select(x => Convert.ToInt32(Regex.Match(x.Text.Trim(), "\\d+").Value))
                .OrderBy(p => p)
                .ToList();

            return Convert.ToInt32(lstPrices.First());
        }

        public string GetTheCheapestFeaturedHotelsName(int price)
        {
            var hotel = lstFeaturedHotels
                .First(x => Convert.ToInt32(Regex.Match(x.Text.Trim(), "\\d+").Value) == price);

            return hotel.FindElement(By.TagName("h5")).Text.Trim();
        }

        public LogInPage ClickLogin()
        {
            ClickMyAccaunt();
            lnkLogin.Click();
            return new LogInPage(driver);
        }
    }
}
