using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TehnomanijaDomaci
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();

            driver.Url = "https://www.tehnomanija.rs";

            IWebElement elFrizider = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div/div/div/div[2]/div[10]/a[2]"));
            IWebElement elBlender = driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div[4]/div[9]/a[2]"));
            IWebElement elUsisivac = driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div[1]/div[9]/a[2]"));

            IJavaScriptExecutor jse = driver;

            jse.ExecuteScript("arguments[0].click()", elFrizider);
            Thread.Sleep(2000);
            jse.ExecuteScript("arguments[0].click()", elBlender);
            Thread.Sleep(2000);
            jse.ExecuteScript("arguments[0].click()", elUsisivac);

            driver.Navigate().GoToUrl("https://www.tehnomanija.rs/index.php?mod=catalog&op=one_page_checkout");

            IWebElement elFriziderCena = driver.FindElement(By.XPath("//*[@id='one_page_checkout_form']/div[1]/div[1]/div/ul/li[1]/ul/li[2]/div[2]/div[3]"));
            IWebElement elBlenderCena = driver.FindElement(By.XPath("//*[@id='one_page_checkout_form']/div[1]/div[1]/div/ul/li[2]/ul/li[2]/div[2]/div[3]"));
            IWebElement elUsisivacCena = driver.FindElement(By.XPath("//*[@id='one_page_checkout_form']/div[1]/div[1]/div/ul/li[3]/ul/li[2]/div[2]/div[3]"));

            string friziderCena = elFriziderCena.Text;
            string blenderCena = elBlenderCena.Text;
            string usisivacCena = elUsisivacCena.Text;

            string friziderCenaBezViskova = friziderCena.Replace(".", "").Replace(",", ".").Replace(" RSD","");
            string blenderCenaBezViskova = blenderCena.Replace(".", "").Replace(",", ".").Replace(" RSD", "");
            string usisivacCenaBezViskova = usisivacCena.Replace(".", "").Replace(",", ".").Replace(" RSD", "");

            double friziderDecimalniBroj = Double.Parse(friziderCenaBezViskova);
            double blenderDecimalniBroj = Double.Parse(blenderCenaBezViskova);
            double usisivacDecimalniBroj = Double.Parse(usisivacCenaBezViskova);

            double zbir = Math.Abs(friziderDecimalniBroj + blenderDecimalniBroj + usisivacDecimalniBroj);

            Console.WriteLine(friziderCenaBezViskova);
            Console.WriteLine(blenderCenaBezViskova);
            Console.WriteLine(usisivacCenaBezViskova);

            Console.WriteLine(zbir);

            IWebElement elUkupanIznos = driver.FindElement(By.XPath("//*[@id='one_page_checkout_form']/div[2]/div/div[3]/div/ul/li[4]/div[2]"));
            string ukupanIznos = elUkupanIznos.Text;
            string ukupanIznosBezViskova = ukupanIznos.Replace(".", "").Replace(",", ".").Replace(" RSD", "");
            double ukupanIznosDecimalniBroj = Double.Parse(ukupanIznosBezViskova);

            if (ukupanIznosDecimalniBroj > zbir)
            {
                Console.WriteLine("Ukupan iznos je veci od zbira");

            }
            else if (ukupanIznosDecimalniBroj < zbir)
            {
                Console.WriteLine("Ukupan iznos je manji od zbira");
            }
            else
            {
                Console.WriteLine("Ukupan iznos jednak je zbiru");
            }
        }



    }
}
