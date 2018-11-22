using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using BumSimulator.Settings;
using System.Windows.Navigation;
using System.Windows;

namespace BumSimulator.Stats
{
	class MoodStat : IMainStat
	{
		delegate void DisplayHandler(TimeStat Time);
		DisplayHandler handler;
        IAsyncResult resultObj;
		int points;
		public int Points
		{
			get { return points; }
			set
			{
                if (value < 0)
                    points = 0;
                else if (value > 100)
                    points = 100;
                else
                    points = value;
                OnPropertyChanged("Points");
			}
		}
		public string DiedMessege
		{
			get
			{
				return Settings_.moodDied;
			}
		}
		public MoodStat(int points)
		{
			Points = points;
		}

		public bool PositiveEffect(IObject otherStat)
		{
			if (otherStat is MoodStat)
			{
				Points += (otherStat as MoodStat).Points;
				Settings.UIControls.ChangesMoodBar.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, (ThreadStart)delegate
				{
					Settings.UIControls.ChangesMoodBar.Foreground = Brushes.Green;
					Settings.UIControls.ChangesMoodBar.Content = (otherStat as MoodStat).Points.ToString();
					DoubleAnimation Animation = new DoubleAnimation();
					Animation.From = 1;
					Animation.To = 0;
					Animation.Duration = TimeSpan.FromSeconds(5);
					Settings.UIControls.ChangesMoodBar.BeginAnimation(Button.OpacityProperty, Animation);
				});
				return true;
			}
			return false;
		}

		public bool NegativeEffect(IObject otherStat)
		{
			if(otherStat is MoodStat)
			{
				Points -= (otherStat as MoodStat).Points;
				Settings.UIControls.ChangesMoodBar.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, (ThreadStart)delegate
				{
					Settings.UIControls.ChangesMoodBar.Foreground = Brushes.Red;
					Settings.UIControls.ChangesMoodBar.Content = "-" + (otherStat as MoodStat).Points.ToString();
					DoubleAnimation Animation = new DoubleAnimation();
					Animation.From = 1;
					Animation.To = 0;
					Animation.Duration = TimeSpan.FromSeconds(5);
					Settings.UIControls.ChangesMoodBar.BeginAnimation(Button.OpacityProperty, Animation);
				});
				return true;
			}
			return false;
		}

		public bool Is(IObject MoodStat)
		{
			if (MoodStat is MoodStat)
			{
				if (Points == (MoodStat as MoodStat).Points)
				{
					return true;
				}
			}
			return false;
		}
		static bool flag = false;
		public void Check(TimeStat Time)
        {
            if (Points == 0)
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
                if (Points > 0)
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
			if (flag == true)
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
