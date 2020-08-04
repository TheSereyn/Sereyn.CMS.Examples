using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sereyn.CMS.Examples.Blog.Models
{
    public class NavItems
    {
        public List<NavItem> Items { get; set; } = new List<NavItem>();

        public void SetItemActive(string route)
        {
            int index = Items.FindIndex(x => x.Active == true);

            if (index != -1)
                Items[index].Active = false;

            index = Items.FindIndex(x => x.Route.Contains(route));

            if (index != -1)
                Items[index].Active = true;
        }

    }

    public class NavItem
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public bool Active { get; set; } = false;
    }
}
