using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace PPDMLoaderLibrary
{
    public class DownloadDataFromWeb
    {
        private readonly string _path;
        private readonly string wellBoreUrl = @"https://mft.rrc.texas.gov/link/4e9023eb-e4ee-45b8-81b7-aec1494c1e8e";
        private readonly string wellApiUrl = @"https://mft.rrc.texas.gov/link/7ed6883a-875d-4e24-a7e5-1614d5968389";

        public DownloadDataFromWeb(string path = "C:\temp")
        {
            _path = path;
        }

        public void DownloadWells(string countyCode)
        {
            string url = wellBoreUrl;
            string file = "well" + countyCode + ".zip";
            ChromeDownload(url, file);

            string zipPath = _path + @"\" + file;
            string extractPath = _path + @"\extract";
            if (Directory.Exists(extractPath))
            {
                DirectoryInfo di = new DirectoryInfo(extractPath);
                di.Delete(true);
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public void DownloadApiData(string countyCode)
        {
            string url = wellApiUrl;
            string file = "api" + countyCode + ".dbf";
            ChromeDownload(url, file);
        }

        private void ChromeDownload(string url, string file)
        {
            IWebDriver driver;
            bool fileExist = false;
            string filePath = _path + "/" + file;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            new DriverManager().SetUpDriver(new ChromeConfig());

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", _path);

            driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(url);

            try
            {
                driver.FindElement(By.LinkText(file));
            }
            catch (NoSuchElementException)
            {
                Exception error = new Exception($"No data for this county");
                driver.Quit();
                throw error;
            }

            driver.FindElement(By.LinkText(file)).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until<bool>(x => fileExist = File.Exists(filePath));
            driver.Quit();
        }
    }
}
