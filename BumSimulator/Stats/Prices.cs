using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.Stats.Valutas;
using BumSimulator.Settings;

namespace BumSimulator.Stats
{
	class BottlePrice : IPriceStat
	{
		IValuta price;
        string change;
		public IValuta Price
		{
			get { return price; }
			set
			{
				price = value;
                OnPropertyChanged("StringPrice");
			}
		}
        public string StringPrice
        {
            get 
            {
                return Price.Count.ToString() + " (" + change + ")"; 
            }
        }
		public BottlePrice(IValuta price)
		{
			Price = price;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is BottlePrice)
			{
				//Price = Price.PositiveEffect(otherStat);
				if (Price.Count < Settings_.minPriceForBottle)
					Price.Count = Settings_.minPriceForBottle;
				else if (Price.Count > Settings_.maxPriceForBottle)
					Price.Count = Settings_.maxPriceForBottle;
                change = "+" + (otherStat as BottlePrice).Price.Count.ToString();
				return true;
			}
			else if (otherStat is IValuta)
			{
                Price = new UAH(Price.Count + (otherStat as IValuta).Count);
				if (Price.Count < Settings_.minPriceForBottle)
					Price.Count = Settings_.minPriceForBottle;
				else if (Price.Count > Settings_.maxPriceForBottle)
					Price.Count = Settings_.maxPriceForBottle;
                change = "+" + (otherStat as IValuta).Count.ToString();
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is BottlePrice)
			{
                Price.NegativeEffect(otherStat);
				if (Price.Count < Settings_.minPriceForBottle)
					Price.Count = Settings_.minPriceForBottle;
				else if (Price.Count > Settings_.maxPriceForBottle)
					Price.Count = Settings_.maxPriceForBottle;
                change = "-" + (otherStat as BottlePrice).Price.Count.ToString();
				return true;
			}
			else if (otherStat is IValuta)
			{
                Price = new UAH(Price.Count - (otherStat as IValuta).Count);
				if (Price.Count < Settings_.minPriceForBottle)
					Price.Count = Settings_.minPriceForBottle;
				else if (Price.Count > Settings_.maxPriceForBottle)
					Price.Count = Settings_.maxPriceForBottle;
                change = "-" + (otherStat as IValuta).Count.ToString();
				return true;
			}
			return false;
		}

		public bool Is(IStat BottlesStat)
		{
			if (BottlesStat is BottlePrice)
			{
				if (Price.Count == (BottlesStat as BottlePrice).Price.Count)
				{
					return true;
				}
			}
			return false;
		}

		public void Graph()
		{
			Random rand = new Random();
			if (rand.Next(0, 100) <= Settings_.PositivePercentForBottle)
			{
				rand = new Random();
				int bug = rand.Next(Settings_.minPriceChangeForBottle, Settings_.maxPriceChangeForBottle);

				PositiveEffect(new UAH(bug));
			}
			else
			{
				rand = new Random();
				int bug = rand.Next(Settings_.minPriceChangeForBottle, Settings_.maxPriceChangeForBottle);

				NegativeEffect(new UAH(bug));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	class USDPrice : IPriceStat
	{
		decimal price;
        string change;
		public decimal Price
		{
			get { return price; }
			set
			{
				price = value;
                OnPropertyChanged("StringPrice");
			}
		}
        public string StringPrice
        {
            get
            {
                return Price.ToString() + " (" + change + ")";
            }
        }
		public USDPrice(decimal price)
		{
			Price = price;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is USDPrice)
			{
				Price -= (otherStat as USDPrice).Price;
				if (Price < Settings_.minPriceForUSD)
					Price = Settings_.minPriceForUSD;
				else if (Price > Settings_.maxPriceForUSD)
					Price = Settings_.maxPriceForUSD;
                change = "-" + (otherStat as USDPrice).Price.ToString();
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is USDPrice)
			{
				Price += (otherStat as USDPrice).Price;
				if (Price < Settings_.minPriceForUSD)
					Price = Settings_.minPriceForUSD;
				else if (Price > Settings_.maxPriceForUSD)
					Price = Settings_.maxPriceForUSD;
                change = "+" + (otherStat as USDPrice).Price.ToString();
				return true;
			}
			return false;
		}
		

		public bool Is(IStat BottlesStat)
		{
			if (BottlesStat is USDPrice)
			{
				if (Price == (BottlesStat as USDPrice).Price)
				{
					return true;
				}
			}
			return false;
		}

		public void Graph()
		{
			Random rand = new Random();


			decimal bug = (decimal)rand.NextDouble();
			bug += rand.Next(Settings_.minPriceChangeForUSD, Settings_.maxPriceChangeForUSD);
			bug = Math.Round(bug, 2, MidpointRounding.ToEven);

			rand = new Random();
			if (rand.Next(0, 100) <= Settings_.PositivePercentForUSD)
			{
				PositiveEffect(new USDPrice(bug));
			}
			else
			{
				NegativeEffect(new USDPrice(bug));
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
