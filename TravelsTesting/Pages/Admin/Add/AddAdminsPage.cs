using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace TravelsTesting.Pages.Admin.Add
{
    class AddAdminsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AddAdminsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "fname")]
        private IWebElement inpFName;

        [FindsBy(How = How.Name, Using = "lname")]
        private IWebElement inpLName;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement inpEmail;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement inpPassword;

        [FindsBy(How = How.ClassName, Using = "select2-results")]
        private IWebElement ddCountry;

        [FindsBy(How = How.XPath, Using = "//*[@class='panel-footer']/button")]
        private IWebElement btnSubmit;
        
        public void SetFirstName(string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(inpFName));
            inpFName.Clear();
            inpFName.SendKeys(value);
        }

        public void SetLastName(string value)
        {
            inpLName.Clear();
            inpLName.SendKeys(value);
        }

        public void SetEmail(string value)
        {
            inpEmail.Clear();
            inpEmail.SendKeys(value);
        }

        public void SetPassword(string value)
        {
            inpPassword.Clear();
            inpPassword.SendKeys(value);
        }

        public void SelectCountry(string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("select2-chosen")))
                .Click();
            var elem = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("select2-input")));
            elem.SendKeys(value);
            elem.SendKeys("\n");
        }

        public void ClickSubmit()
        {
            btnSubmit.Click();
        }

        public AdminPage AddNewAdmin(string firstName, string lastName, string email, string password, string country) 
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPassword(password);
            SelectCountry(country);
            ClickSubmit();
            return new AdminPage(driver);
        }
    }
}
