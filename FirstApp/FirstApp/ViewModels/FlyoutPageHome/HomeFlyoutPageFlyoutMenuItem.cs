﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp
{
	public class HomeFlyoutPageFlyoutMenuItem
	{
		public HomeFlyoutPageFlyoutMenuItem()
		{
			TargetType = typeof(HomeFlyoutPageFlyoutMenuItem);			
		}

		public int Id { get; set; }
		public string Title { get; set; }

		public Type TargetType { get; set; }
	}
}