using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaClassToXCodeConversion.Model
{
    internal class PathItem
    {
        public string GText { get; set; }
        public string EText { get; set; }
        public string HText { get; set; }
        public string EVText { get; set; }
        public List<string> VItems { get; set; } = new List<string>();
        public string ClassName { get; set; }
    }
}
