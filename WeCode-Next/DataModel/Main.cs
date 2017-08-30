using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCode_Next.DataModel
{
    public class Icon
    {
        public string Graph { get; set; }
    }
    public class Item
    {
        public string Font { get; set; }
        public string Graph { get; set; }
    }
    public class URI
    {
        public string Intro { get; set; }
        public string Content { get; set; }
    }
    public class Nav
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public Type PageType { get; set; }
    }
    public class GUID
    {
        public string ID { get; set; }
    }
}
