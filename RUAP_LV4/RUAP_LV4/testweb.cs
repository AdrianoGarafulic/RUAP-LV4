using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Testweb
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demowebshop.tricentis.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheWebTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Log in")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("agarafulic@etfos.hr");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("pagtrg40");
            driver.FindElement(By.CssSelector("input.button-1.login-button")).Click();

           wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Apparel & Shoes")));
           driver.FindElement(By.LinkText("Apparel & Shoes")).Click();

           wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("img[alt=\"Picture of Blue Jeans\"]")));
           driver.FindElement(By.CssSelector("img[alt=\"Picture of Blue Jeans\"]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-to-cart-button-36")));
            driver.FindElement(By.Id("add-to-cart-button-36")).Click();
            driver.FindElement(By.CssSelector("span.cart-label")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("removefromcart")));
            driver.FindElement(By.Name("removefromcart")).Click();
            driver.FindElement(By.Name("updatecart")).Click();

        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
