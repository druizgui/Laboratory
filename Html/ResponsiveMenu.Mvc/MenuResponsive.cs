﻿namespace ResponsiveMenu.Mvc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    //https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-3.1
    public class MenuResponsive : ViewComponent
    {
        private const string DefaultIcon = "fa fa-square";

        public async Task<IViewComponentResult> InvokeAsync(
            IList<MenuItem> aspItems,
            //, string hoverColor = "#515151",
            //string hoverBackground = "#515151",
            //string defaultColor = "#515151",
            //string defaultBackground = "#ffffff",
            string defaultIcon)
        {
            if (defaultIcon == null) defaultIcon = DefaultIcon;


            foreach (var item in aspItems)
            {
                if (item.Icon == null) item.Icon = DefaultIcon;
                //if (item.Color == null) item.Color = defaultColor;
                //if (item.Background== null) item.Background = defaultBackground;
                if (item.Icon == null) item.Icon = defaultIcon;
            }

            var model = new MenuModel
            {
                Items = aspItems,
                HoverBackground = "#3e3e3e",
                HoverColor = "#fffedb"
            };

            return View(model);
        }
    }

    public class MenuModel
    {
        public string HoverColor { get; set; }
        public string HoverBackground { get; set; }
        public IList<MenuItem> Items { get; set; }
    }

    public class MenuItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Background { get; set; }

        //style="color: #515151;background: #7FFFD4"><div class="ak-menu"><i class="fa fa-users"></i><p>Customers      </p></div></a></div>
    }
}
