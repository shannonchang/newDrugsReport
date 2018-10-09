using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewDrugs.Models;

namespace NewDrugs.Service
{
	public class ComponentService
	{
		private List<SelectListItem> list = new List<SelectListItem>();

		/// <summary>
		/// 下拉式選單專用，chooseYN 為Y者，會於list第一項加入"請選擇"
		/// </summary>
		/// <param name="needYN"></param>
		/// <returns></returns>
		public List<SelectListItem> SelectListMappingHandler(List<TbCommonData> model, string chooseYN)
		{
			List<SelectListItem> dropDownlist = new List<SelectListItem>();

			if (chooseYN == "Y")
			{
				dropDownlist.Add(
					new SelectListItem()
					{
						Value = "0",
						Text = "請選擇"
					}
				);
			}

			foreach (var items in model)
			{
				dropDownlist.Add(
					new SelectListItem()
					{
						Value = items.COMM_CODE,
						Text = items.COMM_VALUE
					}
				);
			}

			return dropDownlist;
		}
	}
}