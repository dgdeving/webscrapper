using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using ClosedXML.Excel;
using WebScraperConsoleApp.Models;

namespace WebScraperConsoleApp
{
    public class WebScraper
    {
        public async Task ScrapeDataAsync()
        {
            int consecutiveBadRequests = 0;
            int productNumber = 1;
            List<Product> products = new List<Product>();

            while (consecutiveBadRequests < 150)
            {
                string url = $"https://kream.co.kr/products/{productNumber}";
                string htmlContent = await DownloadWebpageAsync(url);

                if (htmlContent != null)
                {
                    var data = ParseHtml(htmlContent);

                    if (data != null)
                    {
                        Console.WriteLine($"English Name: {data.EnglishName}");
                        Console.WriteLine($"Name: {data.Name}");
                        Console.WriteLine($"Brand Name: {data.BrandName}");
                        Console.WriteLine($"URL: {data.Url}");
                        Console.WriteLine($"Model Number: {data.ModelNumber}");

                        products.Add(data);


                        consecutiveBadRequests = 0;
                    }
                    else
                    {
                        consecutiveBadRequests++;
                    }
                }
                else
                {
                    consecutiveBadRequests++;
                }

                productNumber++;
            }

            // Save data to Excel or database
            SaveDataToExcel(products);
        }

        private static async Task<string> DownloadWebpageAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading the webpage: {ex.Message}");
            }

            return null;
        }

        private Product ParseHtml(string htmlContent)
        {
            var product = new Product();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            //XPath selectors based on the provided HTML structure
            var englishNameNode = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='title']");
            var nameNode = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='sub_title']");
            var brandNode = htmlDoc.DocumentNode.SelectSingleNode("//a[@class='brand']");
            var modelNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//dd[@class='product_info']");
            var urlNode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");


            if (englishNameNode != null)
            {
                product.EnglishName = englishNameNode.InnerText.Trim();
            }

            if (nameNode != null)
            {
                product.Name = nameNode.InnerText.Trim();
            }

            if (brandNode != null)
            {
                product.BrandName = brandNode.InnerText.Trim();

            }

            if (modelNumberNode != null)
            {
                product.ModelNumber = modelNumberNode.InnerText.Trim();
            }

            if (urlNode != null)
            {
                product.Url = urlNode.GetAttributeValue("content", "");
            }

            return product;
        }

        public static void SaveDataToExcel(List<Product> data)
        {
            using (var workbook = new XLWorkbook())
            {
                // Create a new worksheet
                var worksheet = workbook.Worksheets.Add("Products");

                // Set headers for the columns
                worksheet.Cell(1, 1).Value = "English Name";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Brand Name";
                worksheet.Cell(1, 4).Value = "URL";
                worksheet.Cell(1, 5).Value = "Model Number";

                // Populate the data
                for (int i = 0; i < data.Count; i++)
                {
                    Product product = data[i];

                    worksheet.Cell(i + 2, 1).Value = product.EnglishName;
                    worksheet.Cell(i + 2, 2).Value = product.Name;
                    worksheet.Cell(i + 2, 3).Value = product.BrandName;
                    worksheet.Cell(i + 2, 4).Value = product.Url;
                    worksheet.Cell(i + 2, 5).Value = product.ModelNumber;
                }

                // Save the Excel file
                workbook.SaveAs("products.xlsx");
            }
        }
    }

    //private void SaveDataToDatabase(List<DataModel> data) { /*...*/ }
}

