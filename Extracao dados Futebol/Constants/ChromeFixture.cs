using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extracao_dados_Futebol.Constants
{
    public class ChromeFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        //Setup
        public ChromeFixture(string path, ChromeOptions options)
        {
            Driver = new ChromeDriver(path, options);
        }

        //TearDown
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
