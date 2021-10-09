using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using AventStack.ExtentReports;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class LinkGroupDemoClass
    {
        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();

            //Step: When I open the home page
            driver.Navigate().GoToUrl("https://www.linkgroup.eu/");

            //Step: Given I have agreed to the cookie policy
            driver.FindElement(By.XPath("//button[@id='btnAccept']")).Click();

            //Step: Then the page is displayed
            IWebElement pageTitle = driver.FindElement(By.XPath("//div[@id='las-logo']//a//img"));
            Assert.AreEqual(true, pageTitle.Displayed);

            //Step: When I search for 'Leeds'
            Actions action = new Actions(driver);
            IWebElement hoveringSearchBar = driver.FindElement(By.XPath("//a[@id='navbardrop']//i"));
            action.MoveToElement(hoveringSearchBar).ClickAndHold().Build().Perform();

            driver.FindElement(By.XPath("//input[@class='form-control mr-sm-2']")).SendKeys("Leeds");
            action.SendKeys(Keys.Enter);

            //Step: Then the search results are displayed
            List<string> newList = new List<string>();
            IList<IWebElement> listOfSearchRes = driver.FindElements(By.XPath("//div//section[@id='SearchResults']"));
            foreach (IWebElement ele in listOfSearchRes)
            {
                newList.Add(ele.Text);
                foreach(string addedText in newList)
                {
                    Assert.AreEqual(true, addedText.Contains("Leeds"));
                }
            }

            //Step: When I have opened the Found Solutions page
            driver.Navigate().GoToUrl("https://www.linkfundsolutions.co.uk/");
            //driver.SwitchTo().Window(driver.WindowHandles.ToList().Last());

            //Step: When I view Funds
            hoveringSearchBar = driver.FindElement(By.XPath("//div//a[@id='navbarDropdown']"));
            action.MoveToElement(hoveringSearchBar).ClickAndHold().Build().Perform();

            //Step: Then I can select the investment managers for <Jurisdiction> investors
            IWebElement ukInvestor = driver.FindElement(By.XPath("(//a[@class='nav-link btn-link'])[1]"));
            IWebElement irishInvestor = driver.FindElement(By.XPath("(//a[@class='nav-link btn-link'])[2]"));
            IWebElement swissInvestor = driver.FindElement(By.XPath("(//a[@class='nav-link btn-link'])[3]"));
            Assert.AreEqual(true, ukInvestor.Displayed);
            Assert.AreEqual(true, ukInvestor.Enabled);
            Assert.AreEqual(true, irishInvestor.Displayed);
            Assert.AreEqual(true, irishInvestor.Enabled);
            Assert.AreEqual(true, swissInvestor.Displayed);
            Assert.AreEqual(true, swissInvestor.Enabled);

            driver.Close();
            driver.Quit();
        }

    }
}
