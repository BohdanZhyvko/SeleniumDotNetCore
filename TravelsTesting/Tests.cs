using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TravelsTesting.Pages;
using System;
using System.IO;
using TravelsTesting.Pages.Admin;
using TravelsTesting.Pages.Admin.Add;

namespace TravelsTesting
{
    [TestFixture]
    public class Excercise1Test
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }

        [TestCase("https://www.phptravels.net/home", "user@phptravels.com", "demouser")]
        public void Excercise1(string url, string loginName, string password)
        {
            driver.Navigate().GoToUrl(url);
            var homePage = new HomePage(driver);
            LogInPage loginPage = homePage.ClickLogin();
            AccountPage accountPage = loginPage.LogIn(loginName, password);
            Assert.AreEqual(DateTime.Now.ToString("dd MMMM yyyy"), accountPage.GetDateText(), "Date is wrong");
            homePage = accountPage.ClickHome();
            var price = homePage.GetTheCheapestFeaturedHotelsPrice();
            Assert.Fail($"The cheapest Features Hotel is : '{homePage.GetTheCheapestFeaturedHotelsName(price)}' with Price: ${price}");
        }

        [TestCase("https://www.phptravels.net/admin", "admin@phptravels.com", "demoadmin", "FirstName", "LastName", "newadmin@phptravels.com", "Fiji")]
        public void Excercise2(string url, string loginName, string password, string firstName, string lastName, string email, string country)
        {
            driver.Navigate().GoToUrl(url);
            var adminLoginPage = new AdminLoginPage(driver);
            AdminPage adminPage = adminLoginPage.LogIn(loginName, password);
            adminPage.ClickAdmins();
            AddAdminsPage addAdminsPage = adminPage.ClickAdd();
            adminPage = addAdminsPage.AddNewAdmin(firstName, lastName, email, password, country);
            try 
            { 
                Assert.IsTrue(adminPage.IsRowExist(firstName, lastName, email), "Admin is not created"); 
            }
            finally
            {
                adminPage.ClickDeleteAdmin(email);
            }
        }
    }
}

