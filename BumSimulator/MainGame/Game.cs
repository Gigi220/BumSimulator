using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BumSimulator.Stats;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using BumSimulator.Settings;
using BumSimulator.Stats.Valutas;

namespace BumSimulator.MainGame
{
    class Game
    {
		public User User { get; set; }
		public BottlePrice BottlePrice { get; set; }
		public USDPrice USDPrice { get; set; }
		//public DriverCard DriverCard { get; set; }

		public Game(User User)
		{
			this.User = User;
			BottlePrice = new BottlePrice(new UAH(5));
			USDPrice = new USDPrice(25);
			MakeMove();
		}

		public async void MakeMove()
		{
			User.MakeMove();
			User.CheckLose();
			await Task.Run(() => BottlePrice.Graph());
			await Task.Run(() => USDPrice.Graph());
		}
	}
}
