using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.Enums;

namespace BumSimulator.Stats
{
	class PropertyStat : IStat
	{
		List<EProperty> properties;
		public List<EProperty> Properties
		{
			get { return properties; }
			set
			{
				properties = value;
				OnPropertyChanged("HighestProperty");
			}
		}
		public EProperty HighestProperty
		{
			get
			{
				EProperty tmp = properties[0];
				foreach (EProperty x in properties)
				{
					if ((byte)x > (byte)tmp)
					{
						tmp = x;
					}
				}
				return tmp;
			}
		}
		public PropertyStat()
		{
			Properties = new List<EProperty>();
			Properties.Add(EProperty.Box);
		}
		public PropertyStat(EProperty property)
		{
			Properties = new List<EProperty>();
			Properties.Add(property);
		}
		public PropertyStat(List<EProperty> properties)
		{
			Properties = new List<EProperty>(properties);
		}

		public bool PositiveEffect(IObject otherStat)
		{
			if(otherStat is PropertyStat)
			{
				if ((otherStat as PropertyStat).Properties != null)
				{
					foreach (EProperty x in (otherStat as PropertyStat).Properties)
					{
						if (Properties.Contains(x) == false)
						{
							Properties.Add(x);
							OnPropertyChanged("HighestProperty");
							return true;
						}
					}
				}
			}
			return false;
		}
		public bool NegativeEffect(IObject otherStat)
		{
			if (otherStat is PropertyStat)
			{
				if ((otherStat as PropertyStat).Properties != null)
				{
					foreach (EProperty x in (otherStat as PropertyStat).Properties)
					{
						if (Properties.Contains(x))
						{
							Properties.Remove(x);
							OnPropertyChanged("HighestProperty");
							return true;
						}
					}
				}	
			}
			return false;
		}

		public bool Is(IObject PropertyStat)
		{
			if (PropertyStat is PropertyStat)
			{
				if ((PropertyStat as PropertyStat).Properties != null)
				{
					foreach (EProperty x in (PropertyStat as PropertyStat).Properties)
					{
						if (Properties.Contains(x) == false || (byte)HighestProperty < (byte)x)
						{
							System.Windows.MessageBox.Show("Потрібно " + x.ToString());
							return false;
						}
					}
				}
			}
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
