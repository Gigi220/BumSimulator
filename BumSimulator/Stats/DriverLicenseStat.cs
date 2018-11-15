using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BumSimulator.Stats
{
	class DriverLicenseStat : IStat
	{
		bool isDriverLicense;
		public bool IsDriverLicense
		{
			get { return isDriverLicense; }
			set
			{
				isDriverLicense = value;
				OnPropertyChanged("IsDriverLicenseVis");
			}
		}
		public Visibility IsDriverLicenseVis
		{
			get
			{
				if(IsDriverLicense == true)
				{
					return Visibility.Visible;
				}
				return Visibility.Hidden;
			}
		}

		public DriverLicenseStat(bool IsDriverLicense)
		{
			this.IsDriverLicense = IsDriverLicense;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is DriverLicenseStat)
			{
				IsDriverLicense = true;
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is DriverLicenseStat)
			{
				IsDriverLicense = false;
				return true;
			}
			return false;
		}

		public bool Is(IStat IsDriverLicense)
		{
			if (IsDriverLicense is DriverLicenseStat)
			{
				if (this.IsDriverLicense)
				{
					return true;
				}
				System.Windows.MessageBox.Show("Потрібно посвідчення водія");
			}
			return false;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
