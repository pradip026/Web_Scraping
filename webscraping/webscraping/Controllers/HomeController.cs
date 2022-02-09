using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webscraping.Models;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace webscraping.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<nepseIndices> data = new List<nepseIndices>();
            List<nepsesubind> data1 = new List<nepsesubind>();
            var web = new HtmlWeb();
            var doc = web.Load("http://www.nepalstock.com/");
            var nodes = doc.DocumentNode.SelectNodes("//html/body/div[5]/div[2]/div[3]/div[2]/table[1]/tbody/tr[position()<5]");
            var nodsub = doc.DocumentNode.SelectNodes("//html/body/div[5]/div[2]/div[3]/div[2]/table[2]/tbody/tr[position()<14]");
            foreach (var node in nodes)
            {

                string Index = node.SelectSingleNode("td[1]/b").InnerText.Trim();
                string current = node.SelectSingleNode("td[2]").InnerText.Trim();
                string point_chnage = node.SelectSingleNode("td[3]").InnerText.Trim();
                string Percent_change = node.SelectSingleNode("td[4]").InnerText.Trim();
                string image = node.SelectSingleNode("td[5]/img").GetAttributeValue("src",null).Trim();
                var step1 = Regex.Replace(Percent_change, @"<[^>]+>|&nbsp;", "").Trim();
                var step2 = Regex.Replace(step1, @"\s{2,}", " ");
                data.Add(new nepseIndices()
                {
                    Index = Index,
                    current = current,
                    point_change = point_chnage,
                    Percent_change = step2,
                    image = image
                });
            }
            foreach (var nod in nodsub)
            {
                string Indexind = nod.SelectSingleNode("td[1]/b").InnerText.Trim();
                string currentind = nod.SelectSingleNode("td[2]").InnerText.Trim();
                string point_chnageind = nod.SelectSingleNode("td[3]").InnerText.Trim();
                string Percent_changeind = nod.SelectSingleNode("td[4]").InnerText.Trim();
                string imageind = nod.SelectSingleNode("td[5]/img").GetAttributeValue("src", null).Trim();
                var step1ind = Regex.Replace(Percent_changeind, @"<[^>]+>|&nbsp;", "").Trim();
                var step2ind = Regex.Replace(step1ind, @"\s{2,}", " ");
                data1.Add(new nepsesubind()
                {
                    subindices = Indexind,
                    currentind = currentind,
                    point_chnageind = point_chnageind,
                    Percent_chnageind = step2ind,
                    imageind = imageind
                });
            }
            var vm = new ListViewModel();
             vm.marktsum = data;
             vm.subindices = data1;
            

            return View(vm);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "You Can find Real Time Stock Data.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "You can contact Us at:";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
