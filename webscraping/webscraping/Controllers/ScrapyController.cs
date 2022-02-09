using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HtmlAgilityPack;
using webscraping.Models;
using System.Text.RegularExpressions;

namespace webscraping.Controllers
{
    [ApiController]
    public class ScrapyController : ControllerBase
    {
        //GET: api/Scrapy
        [HttpGet]
        [Route("api/scrapydata")]
        public List<nepseIndices> Get()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<nepseIndices> data = new List < nepseIndices >();
            var web = new HtmlWeb();
            var doc = web.Load("http://www.nepalstock.com/");
            var nodes = doc.DocumentNode.SelectNodes("//html/body/div[5]/div[2]/div[3]/div[2]/table[1]/tbody/tr[position()<5]");
            var nodsub = doc.DocumentNode.SelectNodes("///html/body/div[5]/div[2]/div[3]/div[2]/table[2]/tbody/tr");
            foreach ( var node in  nodes)
            {
             
                string Index = node.SelectSingleNode("td[1]/b").InnerText.Trim();
                string current = node.SelectSingleNode("td[2]").InnerText.Trim();
                string point_chnage = node.SelectSingleNode("td[3]").InnerText.Trim();
                string Percent_change = node.SelectSingleNode("td[4]").InnerText.Trim();
                string image = node.SelectSingleNode("td[5]/img").GetAttributeValue("src", null).Trim();
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
            foreach (var node in nodes)
            {

            }
                return data;
        }

        //// GET: api/Scrapy/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Scrapy
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Scrapy/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
