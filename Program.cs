using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace ConsoleApp3
{
    class Program
    {

        public static async Task<string> GetTitle (string url)
        {
            // Khởi tạo http client
            var httpClient = new HttpClient();

            //////    // Thiết lập các Header nếu cần
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            try
            {
                // Thực hiện truy vấn GET
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                Console.WriteLine("Starting read data");

                // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                string htmltext = await response.Content.ReadAsStringAsync();
              
                var doc = new HtmlDocument();
                doc.LoadHtml(htmltext);
                var name = doc.DocumentNode
                          .SelectSingleNode("//h1")
                          .InnerText;
                

                return name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task<List<string>> GetWebContent(string url)
        {
            // Khởi tạo http client
            var httpClient = new HttpClient();

            //////    // Thiết lập các Header nếu cần
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            try
            {
                // Thực hiện truy vấn GET
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                Console.WriteLine("Starting read data");

                // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                string htmltext = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(htmltext);
                var htmlNodes = doc.DocumentNode.SelectNodes("//a");

                List<String> a = new List<String>();
                foreach (var node in htmlNodes)
                {
                    //var c = await GetTitle(node.Attributes["href"].Value);
                    a.Add(node.Attributes["href"].Value);
                }

                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        static async Task Main(string[] args)
        {
            var c = await GetWebContent("https://vnexpress.net/bong-da");
            c.ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    } 
}
