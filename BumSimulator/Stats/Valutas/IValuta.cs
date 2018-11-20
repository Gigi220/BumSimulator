using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.Settings;
using System.Windows;
using System.Windows.Navigation;
using System.Threading;

namespace BumSimulator.Stats.Valutas
{
    interface IValuta : IMainStat
    {
		int Count { get; set; }
    }

	class Valutes : IMainStat
	{
		UAH uah;
		USD usd;
		public UAH UAH
		{
			get { return uah; }
			set { uah = value; OnPropertyChanged("UAH"); }
		}
		public USD USD
		{
			get { return usd; }
			set { usd = value; OnPropertyChanged("USD"); }
		}
		public string DiedMessege
		{
			get
			{
				return Settings_.valutaDied;
			}
		}

		public Valutes()
		{
			UAH = null;
			USD = null;
		}
		public Valutes(UAH UAH, USD USD)
		{
			this.UAH = UAH;
			this.USD = USD;
		}

		public bool PositiveEffect(IStat otherStat)
		{
            if (otherStat is UAH)
            {
                return UAH.PositiveEffect(otherStat);
            }
            else if (otherStat is USD)
            {
                return USD.PositiveEffect(otherStat);
            }
            return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
            if (otherStat is UAH)
            {
                if(UAH.Is(otherStat))
                {
                    return UAH.NegativeEffect(otherStat);
                }
            }
            else if (otherStat is USD)
            {
                if (USD.Is(otherStat))
                {
                    return USD.NegativeEffect(otherStat);
                }
            }
            return false;
		}

		public bool Is(IStat otherStat)
		{
			if (otherStat is UAH)
			{
                return UAH.Is(otherStat);
			}
            else if (otherStat is USD)
            {
                return USD.Is(otherStat);
            }
			return false;
		}

		public void Check(TimeStat Time)
		{
            UAH.Check(Time);
            USD.Check(Time);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	class UAH : IValuta
	{
		delegate void DisplayHandler(TimeStat Time);
		DisplayHandler handler;
		IAsyncResult resultObj;
		int count;
		public int Count
		{
			get { return count; }
			set
			{
				count = value;
				OnPropertyChanged("Count");
			}
		}
		public string DiedMessege
		{
			get
			{
				return Settings_.valutaDied;
			}
		}

		public UAH()
		{
			Count = 0;
		}
		public UAH(int Count)
		{
			this.Count = Count;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is UAH)
			{
				Count += (otherStat as UAH).Count;
				return true;
			}
            else if (otherStat is Valutes)
            {
                Count += (otherStat as Valutes).UAH.Count;
                return true;
            }
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is UAH)
			{
				Count -= (otherStat as UAH).Count;
				return true;
			}
            else if(otherStat is Valutes)
            {
                Count -= (otherStat as Valutes).UAH.Count;
                return true;
            }
			return false;
		}

		public bool Is(IStat UAH)
		{
			if (UAH is UAH)
			{
				if (Count >= (UAH as UAH).Count)
				{
					return true;
				}
                System.Windows.MessageBox.Show("Потрібно " + (UAH as UAH).Count.ToString() + " шекелів");
			}
			return false;
		}

		static bool flag = false;
        public void Check(TimeStat Time)
        {
            if (Count < 0)
            {
                if (handler == null)
                {
                    UIControls.Win = new NavigationWindow();
                    handler = new DisplayHandler(CheckLoseEvent);
                    resultObj = handler.BeginInvoke(Time, new AsyncCallback(AsyncCompleted), null);

                }
            }
        }
        void CheckLoseEvent(TimeStat Time)
        {
            TimeStat loseday = new TimeStat(Time);
			loseday.AddDate(Settings_.daysDied);
			System.Windows.MessageBox.Show(Settings_.warningMessege);
            while (true)
            {
                if (Count > 0)
                {
                    handler = null;
					flag = false;
					return;
                }
                else if (loseday.Is(Time))
                {
                    System.Windows.MessageBox.Show(DiedMessege);
					flag = true;
					return;
                }
            }
        }
        static void AsyncCompleted(IAsyncResult resObj)
        {
			if(flag)
			{
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win = (NavigationWindow)System.Windows.Window.GetWindow(UIControls.Page); }, System.Windows.Threading.DispatcherPriority.Background, null);
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win.Content = new StartPage(); }, System.Windows.Threading.DispatcherPriority.Background, null);
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win.Show(); }, System.Windows.Threading.DispatcherPriority.Background, null);
			}
        }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	class USD : IValuta
	{
        delegate void DisplayHandler(TimeStat Time);
		DisplayHandler handler;
        IAsyncResult resultObj; 
		int count;
		public int Count
		{
			get { return count; }
			set
			{
				count = value;
				OnPropertyChanged("Count");
			}
		}
		public string DiedMessege
		{
			get
			{
				return Settings_.valutaDied;
			}
		}

		public USD()
		{
			Count = 0;
		}
		public USD(int Count)
		{
			count = Count;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is USD)
			{
				Count += (otherStat as USD).Count;
				return true;
			}
            else if (otherStat is Valutes)
            {
                Count += (otherStat as Valutes).USD.Count;
                return true;
            }
            return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
            if (otherStat is USD)
            {
                Count -= (otherStat as USD).Count;
                return true;
            }
            else if (otherStat is Valutes)
            {
                Count += (otherStat as Valutes).USD.Count;
                return true;
            }
            return false;
		}

        public bool Is(IStat USD)
		{
            if (USD is USD)
			{
                if (Count >= (USD as USD).Count)
				{
					return true;
				}
                System.Windows.MessageBox.Show("Потрібно " + (USD as USD).Count.ToString() + " капусти");
			}
			return false;
		}

		static bool flag = false;
        public void Check(TimeStat Time)
        {
            if (Count < 0)
            {
                if (handler == null)
                {
                    UIControls.Win = new NavigationWindow();
                    handler = new DisplayHandler(CheckLoseEvent);
                    resultObj = handler.BeginInvoke(Time, new AsyncCallback(AsyncCompleted), null);

                }
            }
        }
        void CheckLoseEvent(TimeStat Time)
        {
            TimeStat loseday = new TimeStat(Time);
			loseday.AddDate(Settings_.daysDied);
			System.Windows.MessageBox.Show(Settings_.warningMessege);
            while (true)
            {
                if (Count > 0)
                {
                    handler = null;
					flag = false;
					return;
                }
                else if (loseday.Is(Time))
                {
                    System.Windows.MessageBox.Show(DiedMessege);
					flag = true;
					return;
                }
            }
        }
        static void AsyncCompleted(IAsyncResult resObj)
        {
			if (flag)
			{
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win = (NavigationWindow)System.Windows.Window.GetWindow(UIControls.Page); }, System.Windows.Threading.DispatcherPriority.Background, null);
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win.Content = new StartPage(); }, System.Windows.Threading.DispatcherPriority.Background, null);
				UIControls.Win.Dispatcher.Invoke((ThreadStart)delegate { UIControls.Win.Show(); }, System.Windows.Threading.DispatcherPriority.Background, null);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
