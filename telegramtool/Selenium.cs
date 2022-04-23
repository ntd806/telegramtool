using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;

namespace telegramtool
{
    public class Selenium
    {
        public ChromeDriver driver;
        private string URL = "https://web.telegram.org/k/";
        public void init(User U)
        {
            /*ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.EnableVerboseLogging = false;
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-crash-reporter");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-in-process-stack-traces");
            options.AddArgument("--disable-logging");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-popup-blocking");
            options.AddArguments("--disable-notifications");
            options.AddArgument("--log-level=3");
            options.AddArgument("--output=/dev/null");


            this.driver = new ChromeDriver(service, options);*/

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-popup-blocking");
            options.AddArguments("--disable-notifications");
            this.driver = new ChromeDriver(options);
            this.login(U);
        }
        public void login(User U)
        {
            By googleResultText = By.XPath("//*[@id='auth-pages']/div/div[2]/div[2]/div/div[2]/button");
            By ResultText = By.XPath("//*[@id='auth-pages']/div/div[2]/div[1]/div/div[3]/div[2]/div[1]");
            By button = By.XPath("//*[@id='auth-pages']/div/div[2]/div[1]/div/div[3]/button[1]");
            By AutheCode = By.XPath("//*[@id='auth-pages']/div/div[2]/div[3]/div/div[3]/div/input");
            By Pasword = By.XPath("//*[@id='auth-pages']/div/div[2]/div[4]/div/div[2]/div/input[2]");
            By login = By.XPath("//*[@id='auth-pages']/div/div[2]/div[4]/div/div[2]/button");

            if (U == null)
            {
                MessageBox.Show("Bạn chưa chọn số điện thoại. Xin vui lòng chọn lại!");
            }
            else
            {
                 this.driver.Navigate().GoToUrl(this.URL);
                 this.runElementByClick(googleResultText, 3000);
                 this.runElementBySendKeys(ResultText, U.UID, 1000);
                 this.runElementByClick(button, 0);
                 var result = Interaction.InputBox("" + "", "Nhập code", "", -1, -1);
                 if (result != "")
                 {
                     this.runElementBySendKeys(AutheCode, result, 0);
                     this.runElementBySendKeys(Pasword, U.Password, 3000);
                     this.runElementBySendKeys(login, Keys.Enter, 0);
                     Thread.Sleep(2000);
                }

                if (result == "")
                 {
                     MessageBox.Show("Bạn chưa nhập code. Xin vui lòng đăng nhập lại!");
                     this.driver.Close();
                 }
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                this.driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void runElementByClick(By by, int sleep)
        {
            Thread.Sleep(sleep);
            if (IsElementPresent(by))
            {
                this.driver.FindElement(by).Click();
            }
            else
            {
                sleep = sleep / 2;
                this.runElementByClick(by, sleep);
            }
            
        }

        private void runElementBySendKeys(By by, string keys, int sleep)
        {
            Thread.Sleep(sleep);
            if (IsElementPresent(by))
            {
                this.driver.FindElement(by).SendKeys(keys);
            }
            else
            {
                sleep = sleep / 2;
                this.runElementBySendKeys(by, keys, sleep);
            }
        }
    }
}
