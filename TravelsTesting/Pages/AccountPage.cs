using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace TravelsTesting.Pages
{
    class AccountPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AccountPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "h4")]
        private IWebElement lblDate;

        [FindsBy(How = How.XPath, Using = "//*[@title='home']")]
        private IWebElement lnkHome;

        public string GetDateText() 
        {
            WaitForPage();
            return lblDate.Text.Trim();
        }

        public HomePage ClickHome()
        {
            WaitForPage();
            lnkHome.Click();
            return new HomePage(driver);
        }

        private void WaitForPage()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(lblDate));
        }
    }
}
