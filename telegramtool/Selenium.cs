using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace telegramtool
{
    public class Selenium
    {
        public ChromeDriver driver;

        public void init(User U)
        {
            this.driver = new ChromeDriver();
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
               
                this.driver.Navigate().GoToUrl("https://web.telegram.org/k/");
                this.runElementByClick(googleResultText, 3000);
                this.runElementBySendKeys(ResultText, U.UID, 1000);
                this.runElementByClick(button, 1000);
                this.runElementBySendKeys(Pasword, U.Password, 15000);
                this.runElementBySendKeys(login, Keys.Enter, 1000);
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
