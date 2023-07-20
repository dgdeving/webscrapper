using System.Security.Policy;

namespace WebScraperConsoleApp
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;


            var webScraper = new WebScraper();
            await webScraper.ScrapeDataAsync();

            Console.WriteLine("Web scraping completed.");
        }


        //static async Task Main(string[] args)
        //{
        //    string url = "https://kream.co.kr/products/7";
        //    string htmlContent = await DownloadWebpageAsync(url);

        //    if (htmlContent != null)
        //    {
        //        Console.WriteLine("Webpage content:");
        //        Console.WriteLine(htmlContent);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to download the webpage.");
        //    }
        //}

        //private static async Task<string> DownloadWebpageAsync(string url)
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            var response = await httpClient.GetAsync(url);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return await response.Content.ReadAsStringAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error downloading the webpage: {ex.Message}");
        //    }

        //    return null;
        //}


    }
}