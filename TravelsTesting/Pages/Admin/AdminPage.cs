using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using TravelsTesting.Pages.Admin.Add;
using System.Collections.Generic;
using System.Linq;

namespace TravelsTesting.Pages.Admin
{
    class AdminPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AdminPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='ACCOUNTS']/preceding-sibling::a")]
        private IWebElement lnkAccounts;

        [FindsBy(How = How.XPath, Using = "//*[@id='ACCOUNTS']//*[contains(@href, 'admins')]")]
        private IWebElement lnkAdmins;

        [FindsBy(How = How.XPath, Using = "//*[@class='add_button']/button")]
        private IWebElement btnAdd;

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'table-striped table-hover')]/tbody/tr")]
        private IList<IWebElement> trAdmins;
        
        private void ClickAccounts()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(lnkAccounts));
            lnkAccounts.Click();
        }

        public void ClickAdmins()
        {
            ClickAccounts();
            wait.Until(ExpectedConditions.ElementToBeClickable(lnkAdmins));
            lnkAdmins.Click();
        }

        public AddAdminsPage ClickAdd()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(btnAdd));
            btnAdd.Click();
            return new AddAdminsPage(driver);
        }

        public bool IsRowExist(string firstName, string lastName, string email)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(btnAdd));
            if (0 < trAdmins.Count())
            {
                return trAdmins.Any(x =>
                   x.Text.Contains(firstName)
                   && x.Text.Contains(lastName)
                   && x.Text.Contains(email));
            }
            return false;
        }

        public void ClickDeleteAdmin(string email)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("ui-pnotify-title")));
            if (0 < trAdmins.Count())
            {
                trAdmins.First(x => x.Text.Contains(email))
                    .FindElement(By.ClassName("btn-danger"))
                    .Click();
                driver.SwitchTo().Alert().Accept();
                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ui-pnotify-title")));
            }
        }
    }
}
