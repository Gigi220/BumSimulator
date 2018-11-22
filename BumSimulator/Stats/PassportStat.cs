using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BumSimulator.Stats
{
	class PassportStat : IStat
	{
		bool passport;
		public bool IsPassport
		{
			get { return passport; }
			set
			{
				passport = value;
				OnPropertyChanged("IsPassport");
			}
		}
		public PassportStat()
		{
			IsPassport = false;
		}
		public PassportStat(bool Is)
		{
			IsPassport = Is;
		}

		public bool PositiveEffect(IObject otherStat)
		{
			if (otherStat is PassportStat)
			{
				IsPassport = true;
				return true;
			}
			return false;
		}

		public bool NegativeEffect(IObject otherStat)
		{
			if (otherStat is PassportStat)
			{
				IsPassport = false;
				return true;
			}
			return false;
		}

		public bool Is(IObject PassportStat)
		{
			if (PassportStat is PassportStat)
			{
				if(IsPassport == (PassportStat as PassportStat).IsPassport)
				{
					return true;
				}
				System.Windows.MessageBox.Show("Потрібно паспорт!");
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
