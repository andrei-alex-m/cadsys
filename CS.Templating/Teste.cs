using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;
namespace CS.Templating
{
    public class XmlTeste
    {
        public XmlTeste()
        {
            XDocument doc = XDocument.Load("Dictionary.xml");
            var groups = doc.Descendants("Dictionary").GroupBy(x=>x.Element("DICTIONARYCODE").Value);
            Console.WriteLine("Dictionary");
            foreach(var g in groups)
            {
                Console.WriteLine($"{g.Key}");
                foreach (var item in g)
                {
                    Console.WriteLine($"\t{item.Element("DICTIONARYITEMCODE").Value}; {item.Element("DICTIONARYITEMNAME").Value}");
                }
            }

            Console.WriteLine("");

            //foreach (var item in doc.Descendants("Dictionary").Select(x => x.Element("DICTIONARYCODE").Value).Distinct())
            //{
            //    Console.WriteLine(item);
            //}

        }
    }
}
