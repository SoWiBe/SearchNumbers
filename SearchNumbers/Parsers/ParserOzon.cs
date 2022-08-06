using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchNumbers.Parsers
{
    internal class ParserOzon
    {
        public static async Task<string> GetInfo()
        {
            var context = BrowsingContext.New(Configuration.Default);
            var doc = await context.OpenAsync("https://www.ozon.ru/highlight/utsenennye-tovary-553120/");

            var info = doc.GetElementsByClassName("dr dr0 r0d rd2 tsBodyL vj9")[0].GetElementsByTagName("span");
            var name = info[0].TextContent.Trim();
            return name;
        }
    }
}
