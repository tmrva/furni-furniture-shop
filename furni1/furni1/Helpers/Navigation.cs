using Microsoft.AspNetCore.Mvc.Rendering;

namespace furni1.Helpers
{
	public static class Navigation
	{
		public static string IsActive(ViewContext viewContext, string controller, string action)
		{
			string currentController = viewContext.RouteData.Values["controller"]?.ToString();
			string currentAction = viewContext.RouteData.Values["action"]?.ToString();
			if (currentController == controller && currentAction == action)
			{
				return "active";
			}
			else
			{
				return "";
			}
		}
	}
}
