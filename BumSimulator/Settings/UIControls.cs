using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BumSimulator.Settings
{
	public static class UIControls
	{
		public static Grid PropertyBackground { get; set; }

		public static Label MoodBar { get; set; }
		public static Label ChangesMoodBar { get; set; }

		public static Label HpBar { get; set; }
		public static Label ChangesHpBar { get; set; }

		public static Label FoodBar { get; set; }
		public static Label ChangesFoodBar { get; set; }
        public static MainPage Page { get; set; }

        public static NavigationWindow Win { get; set; }
	}
}
