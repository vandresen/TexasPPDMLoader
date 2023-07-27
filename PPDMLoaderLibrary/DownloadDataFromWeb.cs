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
        private readonly string wellBoreUrl = @"https://mft.rrc.texas.gov/link/d551fb20-442e-4b67-84fa-ac3f23ecabb4";
        private readonly string wellApiUrl = @"https://mft.rrc.texas.gov/link/1eb94d66-461d-4114-93f7-b4bc04a70674";
        private readonly string wellPermitUrl = @"https://mft.rrc.texas.gov/link/91a36fea-4dad-4f26-96c3-30843d0e0315";
        private readonly string fullWellboreUrl = @"https://mft.rrc.texas.gov/link/b070ce28-5c58-4fe2-9eb7-8b70befb7af9";

        public DownloadDataFromWeb(string path = "C:\temp")
        {
            _path = path;
        }

        public void DownloadWells(string countyCode)
        {
            string url = wellBoreUrl;
            string file = "well" + countyCode + ".zip";
            string filePath = _path + "/" + file;
            bool download = true;
            if (File.Exists(filePath))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                DateTime currentDate = DateTime.Now;
                if (currentDate < lastWriteTime.AddDays(14))
                {
                    Console.WriteLine("The wellbore file was last written less than 14 days ago.");
                    download = false;
                }
            }
            if (download) ChromeDownload(url, file);

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
            string filePath = _path + "/" + file;
            bool download = true;
            if (File.Exists(filePath))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                DateTime currentDate = DateTime.Now;
                if (currentDate < lastWriteTime.AddDays(14))
                {
                    Console.WriteLine("The wellbore API file was last written less than 14 days ago.");
                    download = false;
                }
            }
            if (download) ChromeDownload(url, file);
        }

        public void DownloadFullWellboreData()
        {
            string url = fullWellboreUrl;
            string file = "dbf900.txt.gz";
            string filePath = _path + "/" + file;
            bool download = true;
            if (File.Exists(filePath))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                DateTime currentDate = DateTime.Now;
                if (currentDate < lastWriteTime.AddMonths(1))
                {
                    Console.WriteLine("The full wellbore file was last written less than 1 month ago.");
                    download= false;
                }
            }
            if (download) ChromeDownload(url, file);
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
