﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_dot_core.Classes.Paging
{
    [ViewComponent(Name = "Pager")]
    public class PagerViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(IPagingList pagingList)
        {
            return View(PagingOptions.Current.ViewName, pagingList);
        }
    }
}
