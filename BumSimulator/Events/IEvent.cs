using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using BumSimulator.MainGame;
using BumSimulator.Stats.Valutas;
using BumSimulator.Stats.Wares;
using BumSimulator.Stats;
using System.Windows;
using System.Windows.Media;

namespace BumSimulator.Events
{
	interface IEvent
	{
		Button Button { get; set; }
		void Start(Game Game);
	}

    class Event : IEvent
    {
        public Button Button { get; set; }
        public List<IAbility> Abilities { get; set; }

        public Event()
        {
            Button = null;
            Abilities = null;
        }
        public Event(Button Button, List<IAbility> Abilities)
        {
            this.Button = Button;
            this.Abilities = Abilities;
        }

        public virtual void Start(Game Game)
        {

            Button.Click += delegate(object sender, RoutedEventArgs e)
            {
                foreach (var x in Abilities)
                {
                    x.DoEffect();
                }

                Game.MakeMove();
            };
        }
        //public virtual void Start(Game Game, IValuta Price)
        //{
        //    Button.Click += delegate(object sender, RoutedEventArgs e)
        //    {
        //        NegativeAbility a = new NegativeAbility(Game.User.Valutes, Price);
        //        if (a.DoEffect() == false)
        //        {
        //            return;
        //        }
        //        foreach (var x in Abilities)
        //        {
        //            x.DoEffect();
        //        }
        //        Game.MakeMove();
        //    };
        //}
    }
    //class EnableEvent : Event
    //{
    //    public EnableEvent() : base() { }
    //    public EnableEvent(Button Button, List<IAbility> abilities) : base(Button, abilities) { }

    //    public override void Start(Game Game)
    //    {
    //        Button.Click += delegate (object sender, RoutedEventArgs e)
    //        {
    //            foreach (var x in Abilities)
    //            {
    //                x.DoEffect();
    //            }
    //            Game.MakeMove();
    //            Button.IsEnabled = !Button.IsEnabled;
    //        };
    //    }
    //    public override void Start(Game Game, IValuta Price)
    //    {
    //        Button.Click += delegate (object sender, RoutedEventArgs e)
    //        {
    //            NegativeAbility a = new NegativeAbility(Game.User.Valutes, Price);
    //            if (a.DoEffect() == false)
    //            {
    //                return;
    //            }
    //            foreach (var x in Abilities)
    //            {
    //                x.DoEffect();
    //            }
    //            Game.MakeMove();
    //            Button.IsEnabled = !Button.IsEnabled;
    //        };
    //    }
    //}
    //class BuySellEvent : Event
    //{
    //    bool IsPressed = false;

    //    public BuySellEvent() : base() { }
    //    public BuySellEvent(Button Button, List<IAbility> abilities) : base(Button, abilities) { }

    //    public override void Start(Game Game)
    //    {
    //        Button.Click += delegate (object sender, RoutedEventArgs e)
    //        {
    //            if (IsPressed == false)
    //            {
    //                foreach (var x in Abilities)
    //                {
    //                    x.DoEffect();
    //                }
    //                Game.MakeMove();
    //                Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush");
    //                IsPressed = !IsPressed;
    //            }
    //            else
    //            {
    //                foreach (var x in Abilities)
    //                {
    //                    if (x is PositiveAbility)
    //                    {
    //                        NegativeAbility xx = new NegativeAbility(x.MainStat, x.TempStat);
    //                        xx.DoEffect();
    //                    }
    //                    else
    //                    {
    //                        PositiveAbility xx = new PositiveAbility(x.MainStat, x.TempStat);
    //                        xx.DoEffect();
    //                    }
    //                }
    //                Game.MakeMove();
    //                Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush");
    //                IsPressed = !IsPressed;
    //            }
    //        };
    //    }

    //    public override void Start(Game Game, IValuta Price)
    //    {
    //        Button.Click += delegate (object sender, RoutedEventArgs e)
    //        {
    //            if (IsPressed == false)
    //            {
    //                NegativeAbility a = new NegativeAbility(Game.User.Valutes, Price);
    //                if (a.DoEffect() == false)
    //                {
    //                    return;
    //                }
    //                foreach (var x in Abilities)
    //                {
    //                    x.DoEffect();
    //                }
    //                Game.MakeMove();
    //                Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush");
    //                IsPressed = !IsPressed;
    //            }
    //            else
    //            {
    //                foreach (var x in Abilities)
    //                {
    //                    if (x is PositiveAbility)
    //                    {
    //                        NegativeAbility xx = new NegativeAbility(x.MainStat, x.TempStat);
    //                        xx.DoEffect();
    //                    }
    //                    else
    //                    {
    //                        PositiveAbility xx = new PositiveAbility(x.MainStat, x.TempStat);
    //                        xx.DoEffect();
    //                    }
    //                }
    //                PositiveAbility na = new PositiveAbility(Game.User.Valutes, Price);
    //                na.DoEffect();

    //                Game.MakeMove();
    //                Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush");
    //                IsPressed = !IsPressed;
    //            }
    //        };
    //    }
    //}



    class CheckEvent : Event
	{
		public List<CheckAbility> CheckList { get; set; }

		public CheckEvent() : base()
		{
			CheckList = new List<CheckAbility>();
		}
        public CheckEvent(Button Button, List<IAbility> abilities)
            : base(Button, abilities)
		{
			CheckList = new List<CheckAbility>();
		}
        public CheckEvent(Button Button, List<CheckAbility> checkList, List<IAbility> abilities)
		{
			this.Button = Button;
			CheckList = checkList;
			Abilities = abilities;
		}

		public override void Start(Game Game)
		{
			Button.Click += delegate (object sender, RoutedEventArgs e)
			{
				if (CheckList != null && CheckList.Count > 0)
				{
					foreach (var x in CheckList)
					{
						if (x.Check() == false)
						{
							return;
						}
					}
				}
				foreach (var x in Abilities)
				{
					x.DoEffect();
				}
				Game.MakeMove();
			};
		}
	}
	class EnableCheckEvent : CheckEvent
	{
		public EnableCheckEvent() : base() { }
        public EnableCheckEvent(Button Button, List<IAbility> abilities) : base(Button, abilities) { }
        public EnableCheckEvent(Button Button, List<CheckAbility> checkList, List<IAbility> abilities) : base(Button, checkList, abilities) { }

		public override void Start(Game Game)
		{
			Button.Click += delegate (object sender, RoutedEventArgs e)
			{
				if(CheckList != null && CheckList.Count > 0)
				{
					foreach (var x in CheckList)
					{
						if (x.Check() == false)
						{
							return;
						}
					}
				}

				foreach (var x in Abilities)
				{
					x.DoEffect();
				}
				Game.MakeMove();
				Button.IsEnabled = !Button.IsEnabled;
			};
		}
	}
	class BuySellCheckEvent : CheckEvent
	{
		bool IsPressed = false;

		public BuySellCheckEvent() : base() { }
        public BuySellCheckEvent(Button Button, List<IAbility> abilities) : base(Button, abilities) { }
        public BuySellCheckEvent(Button Button, List<CheckAbility> checkList, List<IAbility> abilities) : base(Button, checkList, abilities) { }

		public override void Start(Game Game)
		{
			Button.Click += delegate (object sender, RoutedEventArgs e)
			{
				if (IsPressed == false)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								return;
							}
						}
					}
					foreach (var x in Abilities)
					{
						x.DoEffect();
					}

					Game.MakeMove();
					Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush");
					IsPressed = !IsPressed;
				}
				else
				{
					foreach (var x in Abilities)
					{
						if (x is PositiveAbility)
						{
							NegativeAbility xx = new NegativeAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
                        else if (x is NegativeAbility)
						{
							PositiveAbility xx = new PositiveAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
					}

					Game.MakeMove();
					Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush");
					IsPressed = !IsPressed;
				}
			};
		}
	}

	class SubscribeEvent : CheckEvent
	{
		public TimeStat Time { get; set; }
		public CancellationTokenSource cts;
		protected CancellationToken Token;
		protected bool IsPressed;

		public SubscribeEvent() : base()
		{
			IsPressed = false;
			Time = null;
		}
        public SubscribeEvent(Button Button, List<IAbility> abilities, TimeStat time)
			: base(Button, abilities)
		{
			IsPressed = false;
			Time = time;
		}
        public SubscribeEvent(Button Button, List<CheckAbility> CheckList, List<IAbility> abilities, TimeStat time)
			: base(Button, CheckList, abilities)
		{
			IsPressed = false;
			Time = time;
		}

		public override  void Start(Game Game)
		{
			Button.Click += delegate(object sender, RoutedEventArgs e)
			{
				Game.MakeMove();
				if (IsPressed == false)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								return;
							}
						}
					}

					cts = new CancellationTokenSource();
					Token = cts.Token;
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;

					foreach (var x in Abilities)
					{
						x.DoEffect();
					}
                    
					AsyncStart(Game, Token);
				}
				else
				{
					cts.Cancel();
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });// = );
					foreach (var x in Abilities)
					{
						if (x is PositiveAbility)
						{
							NegativeAbility xx = new NegativeAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
						else if (x is NegativeAbility)
						{
							PositiveAbility xx = new PositiveAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
					}
					Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush");
					IsPressed = !IsPressed;
				}
			};
		}
		public virtual void Start(Game Game, IValuta Price)
		{
			Button.Click += delegate(object sender, RoutedEventArgs e)
			{
				if (IsPressed == false)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								return;
							}
						}
					}

					foreach (var x in Abilities)
					{
						x.DoEffect();
					}

					cts = new CancellationTokenSource();
					Token = cts.Token;
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;

					Game.MakeMove();

					AsyncStart(Game, Token, Price);
				}
				else
				{
					cts.Cancel();
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });// = );
					foreach (var x in Abilities)
					{
						if (x is PositiveAbility)
						{
							NegativeAbility xx = new NegativeAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
						else if (x is NegativeAbility)
						{
							PositiveAbility xx = new PositiveAbility(((Ability)x).MainStat, ((Ability)x).TempStat);
							xx.DoEffect();
						}
					}
					Game.MakeMove();
					Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush");
					IsPressed = !IsPressed;
				}
			};
		}

		protected virtual void FuncStart(Game Game, CancellationToken token)
		{
			int daystmp = Game.User.Times.Days + Time.Days;
			short daybuf = (short)(Game.User.Times.Days + 1);
			while (true)
			{
				if (token.IsCancellationRequested)
				{
					cts.Dispose();
					return;
				}
				if (Game.User.Times.Days == daystmp)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								cts.Cancel();
								cts.Dispose();
								IsPressed = !IsPressed;
								return;
							}
						}
					}

					daystmp = Game.User.Times.Days + Time.Days;
				}
				if(Game.User.Times.Days == daybuf)
				{
					foreach (var x in Abilities)
					{
						x.DoEffect();
					}

					daybuf = (short)(Game.User.Times.Days + 1);
				}
			}
		}
		protected virtual void FuncStart(Game Game, CancellationToken token, IValuta Price)
		{
			TimeStat daystmp = new TimeStat(Game.User.Times);
			daystmp.AddDate(Time);

			TimeStat daybuf = new TimeStat(Game.User.Times);
			daybuf.AddDays(1);
			while (true)
			{
				if (token.IsCancellationRequested)
				{
					cts.Dispose();
					return;
				}
				if (Game.User.Times == daybuf)
				{
					foreach (var x in Abilities)
					{
						x.DoEffect();
					}

					daybuf = daybuf = new TimeStat(Game.User.Times);
					daybuf.AddDays(1);
				}
				if (Game.User.Times == daystmp)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								cts.Cancel();
								cts.Dispose();
								IsPressed = !IsPressed;
								return;
							}
						}
					}

					NegativeAbility a = new NegativeAbility(Game.User.Valutes, Price);
					a.DoEffect();

					daystmp = new TimeStat(Game.User.Times);
					daystmp.AddDate(Time);
				}
				
			}
		}
		protected async void AsyncStart(Game Game, CancellationToken token)
		{
			if (token.IsCancellationRequested)
				return;
			await Task.Run(() => FuncStart(Game, token));
		}
		protected async void AsyncStart(Game Game, CancellationToken token, IValuta Price)
		{
			if (token.IsCancellationRequested)
				return;
			await Task.Run(() => FuncStart(Game, token, Price));
		}
	}

	class EnableSubscribeEvent : SubscribeEvent
	{
		public EnableSubscribeEvent() : base() { }
        public EnableSubscribeEvent(Button Button, TimeStat time, List<IAbility> abilities) : base(Button, abilities, time) { }

		public override void Start(Game Game)
		{
			Button.Click += delegate (object sender, RoutedEventArgs e)
            {
                Game.MakeMove();
				if (IsPressed == false)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								return;
							}
						}
					}

					cts = new CancellationTokenSource();
					Token = cts.Token;
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;
					Button.IsEnabled = !Button.IsEnabled;

					foreach (var x in Abilities)
					{
						x.DoEffect();
					}

					AsyncStart(Game, Token);
				}
				else
				{
					cts.Cancel();
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;
				}
			};
		}
		public override void Start(Game Game, IValuta Price)
		{
			Button.Click += delegate (object sender, RoutedEventArgs e)
			{
                Game.MakeMove();
				if (IsPressed == false)
				{
					if (CheckList != null && CheckList.Count > 0)
					{
						foreach (var x in CheckList)
						{
							if (x.Check() == false)
							{
								return;
							}
						}
					}

					cts = new CancellationTokenSource();
					Token = cts.Token;
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;

					AsyncStart(Game, Token, Price);
				}
				else
				{
					cts.Cancel();
					Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });// = );
					IsPressed = !IsPressed;
				}
			};
		}
	}
	//class BuySellSubscribeEvent : SubscribeEvent
	//{
	//	public BuySellSubscribeEvent() : base() { }
	//	public BuySellSubscribeEvent(Button Button, TimeStat time, List<IAbility> abilities) : base(Button, abilities, time) { }

	//	public override void Start(Game Game)
	//	{
	//		Button.Click += delegate (object sender, RoutedEventArgs e)
	//		{
	//			if (IsPressed == false)
	//			{
	//				cts = new CancellationTokenSource();
	//				Token = cts.Token;
	//				IsPressed = !IsPressed;
	//				Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });
	//				AsyncStart(Game, Token);
	//			}
	//			else if (IsPressed == true)
	//			{
	//				cts.Cancel();
	//				IsPressed = !IsPressed;
	//				Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });
	//				foreach (var x in Abilities)
	//				{
	//					if (x is PositiveAbility)
	//					{
	//						NegativeAbility xx = new NegativeAbility(x.MainStat, x.TempStat);
	//						xx.DoEffect();
	//					}
	//					else
	//					{
	//						PositiveAbility xx = new PositiveAbility(x.MainStat, x.TempStat);
	//						xx.DoEffect();
	//					}
	//				}
	//			}
	//		};
	//	}
	//	public override void Start(Game Game, IValuta Price)
	//	{
	//		Button.Click += delegate (object sender, RoutedEventArgs e)
	//		{
	//			if (IsPressed == false)
	//			{
	//				cts = new CancellationTokenSource();
	//				Token = cts.Token;
	//				IsPressed = !IsPressed;
	//				Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("TButtonGradientBrush"); });
	//				AsyncStart(Game, Token, Price);
	//			}
	//			else if (IsPressed == true)
	//			{
	//				cts.Cancel();
	//				IsPressed = !IsPressed;
	//				Button.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (ThreadStart)delegate { Button.Background = (Brush)App.Current.TryFindResource("FButtonGradientBrush"); });
	//				foreach (var x in Abilities)
	//				{
	//					if (x is PositiveAbility)
	//					{
	//						NegativeAbility xx = new NegativeAbility(x.MainStat, x.TempStat);
	//						xx.DoEffect();
	//					}
	//					else
	//					{
	//						PositiveAbility xx = new PositiveAbility(x.MainStat, x.TempStat);
	//						xx.DoEffect();
	//					}
	//				}
	//			}
	//		};
	//	}
	//}
}
