using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BumSimulator.Stats
{
	class RaitingStat : IStat
	{
		int raiting;
		public int Raiting
		{
			get { return raiting; }
			set
			{
				raiting = value;
				OnPropertyChanged("Raiting");
			}
		}
		public RaitingStat()
		{
			Raiting = 0;
		}
		public RaitingStat(int Raiting)
		{
			this.Raiting = Raiting;
		}

		public bool PositiveEffect(IObject otherStat)
		{
			if (otherStat is RaitingStat)
			{
				Raiting += (otherStat as RaitingStat).Raiting;
				return true;
			}
			return false;
		}

		public bool NegativeEffect(IObject otherStat)
		{
			if (otherStat is RaitingStat)
			{
				Raiting -= (otherStat as RaitingStat).Raiting;
				return true;
			}
			return false;
		}

		public bool Is(IObject RaitingStat)
		{
			if (RaitingStat is RaitingStat)
			{
				if (Raiting >= (RaitingStat as RaitingStat).Raiting)
				{
					return true;
				}
				System.Windows.MessageBox.Show("Потрібно " + (RaitingStat as RaitingStat).Raiting.ToString() + " очок рейтингу");
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
