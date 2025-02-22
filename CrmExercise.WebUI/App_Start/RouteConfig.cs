﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CrmExercise.WebUI
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
        name: null,
        url: "Page{page}",
        defaults: new {Controller = "Account", action = "List"}
        );

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Account", action = "List", id = UrlParameter.Optional}
        );
    }
  }
}
