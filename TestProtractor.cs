using System;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Protractor;


namespace ProtractorSample
{

    [TestClass]
    public class TestProtractor
    {

        private StringBuilder verificationErrors = new StringBuilder();
        private IWebDriver driver;
        private NgWebDriver ngDriver;
        private WebDriverWait wait;
        private readonly int wait_seconds = 30;
        private Actions actions;
        ChromeOptions driverOptions;
        private String base_url = "http://www.way2automation.com/angularjs-protractor/banking";


        public string TestDirectory { get; private set; }

        [TestInitialize]
        public void Start()
        {

            driverOptions = new ChromeOptions();
            driverOptions.AddAdditionalCapability("useAutomationExtension", false);
            driver = new ChromeDriver(driverOptions);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(wait_seconds));
            ngDriver = new NgWebDriver(driver);
            actions = new Actions(driver);

        }


        [TestCleanup]
        public void Stop()
        {

            driver.Close();
            driver.Quit();
        }






        [TestMethod]
        public void PerformNgClickAction()
        {

            driver.Navigate().GoToUrl(base_url);
            ngDriver.Url = driver.Url;
            ngDriver.FindElement(By.CssSelector("[ng-click='customer()']")).Click();


        }

        [TestMethod]
        public void PerformNgByRepeaterAction()
        {
            driver.Navigate().GoToUrl(base_url);
            ngDriver.Url = driver.Url;
            ngDriver.FindElement(By.CssSelector("[ng-click='customer()']")).Click();
            var ng_customers = ngDriver.FindElements(NgBy.Repeater("cust in Customers"));
            ng_customers[3].Click();

        }

        [TestMethod]
        public void PerformNgByModelAction()
        {
            driver.Navigate().GoToUrl(base_url);
            ngDriver.Url = driver.Url;
            ngDriver.FindElement(By.CssSelector("[ng-click='customer()']")).Click();
            ReadOnlyCollection<NgWebElement> ng_customers = ngDriver.FindElements(NgBy.Repeater("cust in Customers"));
            for (int iCust = 0; iCust < ng_customers.Count; iCust++)
            {
                ng_customers[3].Click();
            }
            ngDriver.FindElement(By.CssSelector("[type='submit']")).Click();
            ngDriver.WaitForAngular();
            ReadOnlyCollection<NgWebElement> ng_accounts = ngDriver.FindElements(NgBy.Model("accountNo"));
            int iaccounts = ng_accounts.Count;
            var selectedaccount = ngDriver.FindElement(NgBy.Binding("accountNo"));
            var selectedaccountno = selectedaccount.Text;
            ngDriver.WaitForAngular();

        }

        [TestMethod]
        public void SearchCustomer()
        {
            driver.Navigate().GoToUrl(base_url);
            ngDriver.Url = driver.Url;
            ngDriver.FindElement(By.CssSelector("[ng-click='manager()']")).Click();
            ngDriver.FindElement(By.CssSelector("[ng-click='showCust()']")).Click();
            var ng_customersearch = ngDriver.FindElement(NgBy.Model("searchCustomer"));
            ng_customersearch.SendKeys("Harry");
            ngDriver.WaitForAngular();


        }

        [TestMethod]
        public void DeleteCustomer()
        {


            driver.Navigate().GoToUrl(base_url);
            ngDriver.Url = driver.Url;
            ngDriver.FindElement(By.CssSelector("[ng-click='manager()']")).Click();
            ngDriver.FindElement(By.CssSelector("[ng-click='showCust()']")).Click();
            ngDriver.WaitForAngular();
            ReadOnlyCollection<NgWebElement> ng_customers = ngDriver.FindElements(NgBy.Repeater("cust in Customers"));
            foreach (var cust in ng_customers)
            {
                var a = cust.FindElements(By.TagName("td"));
                foreach (var ngWebElement in a)
                {
                    if (ngWebElement.Text.Equals("Neville"))
                    {
                        ngDriver.FindElement(By.CssSelector("[ng-click='deleteCust(cust)']")).Click();
                        break;
                    }
                }
            }


        }
    }
}
