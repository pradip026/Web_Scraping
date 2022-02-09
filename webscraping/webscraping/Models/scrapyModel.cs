using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webscraping.Models
{
    public class nepseIndices
    {
        public string Index { get; set; }
        public string current { get; set; }
        public string point_change { get; set; }
        public string Percent_change { get; set; }
        public string image { get; set; }
    }
    public class nepsesubind
    {
        public string subindices { get; set; }
        public string currentind { get; set; }
        public string point_chnageind { get; set; }
        public string Percent_chnageind { get; set; }
        public string imageind { get; set; }
    }
    public class ListViewModel
    {
        public List<nepseIndices> marktsum { set; get; }
        public List<nepsesubind> subindices { set; get; }
    }
}
