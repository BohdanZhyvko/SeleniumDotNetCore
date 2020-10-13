using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace TravelsTesting.Pages.Admin
{
    class AdminLoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AdminLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//form[contains(@class, 'form-signin')]//input[@name='email']")]
        private IWebElement inpEmail;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement inpPassword;

        [FindsBy(How = How.XPath, Using = "//*[@type='submit']")]
        private IWebElement btnLogin;

        public void SetEmail(string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(inpEmail));
            inpEmail.Clear();
            inpEmail.SendKeys(value);
        }

        public void SetPassword(string value)
        {
            inpPassword.Clear();
            inpPassword.SendKeys(value);
        }

        public AdminPage ClickLogin()
        {
            btnLogin.Click();
            return new AdminPage(driver);
        }

        public AdminPage LogIn(string email, string password)
        {
            SetEmail(email);
            SetPassword(password);
            return ClickLogin();
        }
    }
}
