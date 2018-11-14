using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;

namespace BumSimulator.Stats
{
	class YearsDays
	{
		int years;
		short days;
		public int Years
		{
			get
			{
				return years;
			}
			set
			{
				years = value;
				OnPropertyChanged("Years");
			}
		}
		public short Days
		{
			get
			{
				return days;
			}
			set
			{
				days = value;
				OnPropertyChanged("Days");
			}
		}
		public YearsDays()
		{
			Years = 18;
			Days = 1;
		}
		public YearsDays(int year, short day)
		{
			Years = year;
			Days = day;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
	class TimeStat : IStat
	{
		int years;
		short days;
		public int Years
		{
			get
			{
				return years;
			}
			set
			{
				years = value;
				OnPropertyChanged("TimeToString");
			}
		}
		public short Days
		{
			get
			{
				return days;
			}
			set
			{
				days = value;
				if(days > 365)
				{
					AddYears(days / 365);
					days = (short)(days % 365);
				}
				OnPropertyChanged("TimeToString");
			}
		}

		public string TimeToString
		{
			get
			{
				return Years + " років, " + Days + " день";
			}
		}

		public TimeStat()
		{
			Years = 18;
			Days = 1;
		}
		public TimeStat(int year, short day)
		{
			Years = year;
			Days = day;
		}
		public TimeStat(TimeStat TimeStat)
		{
			Years = TimeStat.Years;
			Days = TimeStat.Days;
		}

		public void AddDays(short days)
		{
			Days += days;
		}
		public void AddYears(int years)
		{
			Years += years;
		}
		public void AddDate(TimeStat TimeStat)
		{
			Years += TimeStat.Years;
			Days += TimeStat.Days;
		}

		public static bool operator ==(TimeStat MainTime, TimeStat TempTime)
		{
			if(MainTime.Years == TempTime.Years && MainTime.Days == TempTime.Days)
			{
				return true;
			}
			return false;
		}
		public static bool operator !=(TimeStat MainTime, TimeStat TempTime)
		{
			if (MainTime.Years != TempTime.Years && MainTime.Days != TempTime.Days)
			{
				return true;
			}
			return false;
		}

		public bool Is(IStat TimeStat)
		{
			if (TimeStat is TimeStat)
			{
				if (Days == (TimeStat as TimeStat).Days && Years == (TimeStat as TimeStat).Years)
				{
					return true;
				}
			}
			return false;
		}

		byte flag = 0;
		public void ChangeSeason(Grid grid)
		{
			if ((Days >= 0 && Days <= 59) || (Days > 334 && Days <= 365) && flag != 1)
			{
				grid.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, (ThreadStart)delegate { grid.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\Seasons\winter.jpg")))); });
				flag = 1;
			}
			else if (Days > 59 && Days <= 151 && flag != 2)
			{
				grid.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, (ThreadStart)delegate { grid.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\Seasons\spring.jpg")))); });
				flag = 2;
			}
			else if (Days > 151 && Days <= 243 && flag != 3)
			{
				grid.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, (ThreadStart)delegate { grid.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\Seasons\summer.jpg")))); });
				flag = 3;
			}
			else if (Days > 243 && Days <= 334 && flag != 4)
			{
				grid.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, (ThreadStart)delegate { grid.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\Seasons\autumn.jpg")))); });
				flag = 4;
			}
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is TimeStat)
			{
				AddDays(((TimeStat)otherStat).Days);
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat) { return true; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
