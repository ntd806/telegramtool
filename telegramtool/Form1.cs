using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace telegramtool
{
    public partial class Form1 : Form
    {
        public userIO user;
        public User U;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.updateData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPhone adPhone = new AddPhone();
            adPhone.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.updateData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                this.user = new userIO();
                this.user.readUserByFile(path);
                this.user.WriteUserByFile(this.user.getPath());
                dataGridView1.DataSource = this.user.users;
            }
        }

        private void updateData()
        {
            this.user = new userIO();
            var path = user.getPath();
            this.user.readUserByFile(path);
            dataGridView1.DataSource = this.user.users;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Connect", MenuItemConnect_Click));
                m.MenuItems.Add(new MenuItem("Connect"));
                m.MenuItems.Add(new MenuItem("Copy"));
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                m.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void MenuItemConnect_Click(Object sender, EventArgs e)
        {
            By googleResultText = By.XPath("//*[@id='auth-pages']/div/div[2]/div[2]/div/div[2]/button");
            By ResultText = By.XPath("//*[@id='auth-pages']/div/div[2]/div[1]/div/div[3]/div[2]/div[1]");
            By button = By.XPath("//*[@id='auth-pages']/div/div[2]/div[1]/div/div[3]/button[1]");
            By AutheCode = By.XPath("//*[@id='auth-pages']/div/div[2]/div[3]/div/div[3]/div/input");
            By Pasword = By.XPath("//*[@id='auth-pages']/div/div[2]/div[4]/div/div[2]/div/input[2]");
            By login = By.XPath("//*[@id='auth-pages']/div/div[2]/div[4]/div/div[2]/button");

            var webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://web.telegram.org/k/");
            Thread.Sleep(2000);
            webDriver.FindElement(googleResultText).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(ResultText).SendKeys(this.U.UID);

            Thread.Sleep(1000);
            webDriver.FindElement(button).Click();
            Thread.Sleep(10000);
            webDriver.FindElement(AutheCode).SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            webDriver.FindElement(Pasword).SendKeys(this.U.Password);
            Thread.Sleep(1000);
            webDriver.FindElement(login).SendKeys(Keys.Enter);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string UID = Convert.ToString(selectedRow.Cells["UID"].Value);
                string Name = Convert.ToString(selectedRow.Cells["Name"].Value);
                string Password = Convert.ToString(selectedRow.Cells["Password"].Value);
                string Email = Convert.ToString(selectedRow.Cells["Email"].Value);
                string Group = Convert.ToString(selectedRow.Cells["Group"].Value);
                string Friend = Convert.ToString(selectedRow.Cells["Friend"].Value);
                string PrivateKey = Convert.ToString(selectedRow.Cells["PrivateKey"].Value);
                string Note = Convert.ToString(selectedRow.Cells["Note"].Value);
                string Status = Convert.ToString(selectedRow.Cells["Status"].Value);
                string Ld = Convert.ToString(selectedRow.Cells["Ld"].Value);
                int Id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                this.U = new User(Id, UID, Name, Email, Password, Ld, Group, Friend, PrivateKey, Note, Status);
            }
        }
    }
}
