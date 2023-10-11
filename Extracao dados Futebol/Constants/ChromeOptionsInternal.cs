using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extracao_dados_Futebol.Constants
{
    public class ChromeOptionsInternal
    {
        public ChromeOptionsInternal(bool visible)
        {
            Options(visible);
        }

        public ChromeOptions Options(bool visible)
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            //options.AddArgument("--window-size=1920,1080");
            //options.AddArgument("--no-sandbox");
            //if (visible)
            //{
            //    options.AddArgument("--headless");
            //}
            options.AddArguments("--disable-infobars");
            options.AddArguments("--remote-allow-origins=*");
            options.AddArguments("--disable-crash-reporter");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-in-process-stack-traces");
            options.AddArguments("--disable-logging");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--log-level=2");
            //options.AddArgument("--output=/dev/null");
            return options;
        }
    }
}
