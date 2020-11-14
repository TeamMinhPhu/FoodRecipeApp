using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.Classes
{
	class Paging
	{
		public string PageSelection { get; set; }

		/// <summary>
		/// receives total pages and returns list page
		/// </summary>
		/// <param name="totalPages"></param>
		/// <returns>bindinglist for binding page in view</returns>
		public static BindingList<Paging> UpdatePage(int totalPages)
		{
			BindingList<Paging> result = new BindingList<Paging>();
			for (int i = 1; i <= totalPages; i++)
			{
				result.Add(new Paging() { PageSelection = i.ToString() });
			}
			return result;
		}
	}
}
