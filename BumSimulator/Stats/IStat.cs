using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BumSimulator.Stats
{
	interface IStat : INotifyPropertyChanged
	{
		bool PositiveEffect(IStat otherStat);
		bool NegativeEffect(IStat otherStat);
		bool Is(IStat check);
		void OnPropertyChanged([CallerMemberName]string prop = "");
	}
	interface IMainStat : IStat
	{
		string DiedMessege { get; }
		void Check(TimeStat Time);
	}
	interface IPriceStat : IStat
	{
		void Graph();
	}
	abstract class Stat : IStat
	{
		public virtual bool PositiveEffect(IStat otherStat) { return true; }
		public virtual bool NegativeEffect(IStat otherStat) { return true; }
		public virtual bool Is(IStat otherStat) { return true; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
