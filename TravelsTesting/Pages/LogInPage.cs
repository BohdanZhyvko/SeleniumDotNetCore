using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace TravelsTesting.Pages
{
    class LogInPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LogInPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement inpUsername;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement inpPassword;

        [FindsBy(How = How.ClassName, Using = "loginbtn")]
        private IWebElement btnLogin;

        public void SetUserName(string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(inpUsername));
            inpUsername.Clear();
            inpUsername.SendKeys(value);
        }

        public void SetPassword(string value)
        {
            inpPassword.Clear();
            inpPassword.SendKeys(value);
        }

        public AccountPage ClickLogin()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(btnLogin));
            btnLogin.Click();
            return new AccountPage(driver);
        }

        public AccountPage LogIn(string userName, string password)
        {
            SetUserName(userName);
            SetPassword(password);
            return ClickLogin();
        }
    }
}
