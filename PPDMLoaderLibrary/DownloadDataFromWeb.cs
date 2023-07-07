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
using System.IO.Compression;

namespace PPDMLoaderLibrary
{
    public class DownloadDataFromWeb
    {
        private readonly string _path;
        private readonly string wellBoreUrl = @"https://mft.rrc.texas.gov/link/4e9023eb-e4ee-45b8-81b7-aec1494c1e8e";
        private readonly string wellApiUrl = @"https://mft.rrc.texas.gov/link/7ed6883a-875d-4e24-a7e5-1614d5968389";
        private readonly string wellPermitUrl = @"https://mft.rrc.texas.gov/link/91a36fea-4dad-4f26-96c3-30843d0e0315";
        private readonly string fullWellboreUrl = @"https://mft.rrc.texas.gov/link/9ef1955f-cf26-4bd4-8030-1253eb772cf9";

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

        public void DownloadFullWellboreData()
        {
            string url = fullWellboreUrl;
            string file = "dbf900.txt.gz";
            ChromeDownload(url, file);

            string extractedFilePath = Path.Combine(_path, Path.GetFileNameWithoutExtension(file));
            string extractPath = _path + @"\extract";
            string zipPath = _path + @"\" + file;
            using (FileStream sourceFileStream = File.OpenRead(zipPath))
            using (FileStream extractedFileStream = File.Create(extractedFilePath))
            using (GZipStream gzipStream = new GZipStream(sourceFileStream, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(extractedFileStream);
            }
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(450));
            wait.Until<bool>(x => fileExist = File.Exists(filePath));
            driver.Quit();
        }
    }
}
