using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BumSimulator.Stats
{
	interface IObject : INotifyPropertyChanged
	{
		void OnPropertyChanged([CallerMemberName]string prop = "");
	}
	interface IStat : IObject
	{
		bool PositiveEffect(IObject otherStat);
		bool NegativeEffect(IObject otherStat);
		bool Is(IObject check);
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
		public virtual bool PositiveEffect(IObject otherStat) { return true; }
		public virtual bool NegativeEffect(IObject otherStat) { return true; }
		public virtual bool Is(IObject otherStat) { return true; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
