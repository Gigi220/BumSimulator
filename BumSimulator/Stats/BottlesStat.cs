using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.Stats.Valutas;

namespace BumSimulator.Stats
{
    class BottlesStat : IStat
	{
		int count;
		public int Count
		{
			get { return count; }
			set
			{
				if(value >= 0)
					count = value;
				OnPropertyChanged("Count");
			}
		}
		public BottlesStat(int count)
		{
			Count = count;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is BottlesStat)
			{
				Count += (otherStat as BottlesStat).Count;
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is BottlesStat)
			{
				Count -= (otherStat as BottlesStat).Count;
				return true;
			}
			return false;
		}

		public bool Is(IStat BottlesStat)
		{
			if (BottlesStat is BottlesStat)
			{
				if (Count >= (BottlesStat as BottlesStat).Count)
				{
					return true;
				}
				System.Windows.MessageBox.Show("Потрібно " + (BottlesStat as BottlesStat).Count.ToString() + " бутилок");
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
