using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BumSimulator.Enums;
using BumSimulator.MainGame;
using BumSimulator.Stats;
using BumSimulator.Stats.Valutas;
using BumSimulator.Stats.Wares;

namespace BumSimulator.Settings
{

	class Settings_
	{
		public static EDificultLevel DificultLevel { get; set; }

		public static int maxPriceForBottle = 0;
		public static int minPriceForBottle = 0;
		public static int maxPriceChangeForBottle = 0;
		public static int minPriceChangeForBottle = 0;
		public static byte PositivePercentForBottle = 0;

		public static decimal maxPriceForUSD = 0;
		public static decimal minPriceForUSD = 0;
		public static int maxPriceChangeForUSD = 0;
		public static int minPriceChangeForUSD = 0;
		public static byte PositivePercentForUSD = 0;

		public static long startUAHuser = 0;
		public static long startUSDuser = 0;

		public static string moodDied = "Ти не вийшов з депресі та наклав на себе руки";
		public static string foodDied = "Ти не поїв і вмер :)";
		public static string hpDied = "За здоров'ям треба слідкувати";
		public static string valutaDied = "Без грошей в сучасному світі не проживеш...";
        public static string warningMessege = "Одна з життєво необідних статистик на критичному рівні, якщо ви протягом 7 днів це не зміните - ви помрете";

		public static UAH Food1Price = new UAH(100);
		public static UAH Food2Price = new UAH(500);
		public static UAH Food3Price = new UAH(3000);

		public static UAH Mood0Price = new UAH(50);
		public static UAH Mood1Price = new UAH(200);
		public static UAH Mood2Price = new UAH(500);
		public static UAH Mood3Price = new UAH(1500);
		public static USD Mood4Price = new USD(1000);

		public static UAH Hp1Price = new UAH(150);
		public static UAH Hp2Price = new UAH(500);
		public static UAH Hp3Price = new UAH(3000);
		public static UAH Hp4Price = new UAH(50000);

		public static UAH work0Price = new UAH(200);
		public static UAH work1Price = new UAH(300);
		public static UAH work2Price = new UAH(1000);
		public static UAH work3Price = new UAH(1500);
		public static UAH work4Price = new UAH(4000);
		public static UAH work5Price = new UAH(30000);
		public static UAH work6Price = new UAH(45000);

		public static UAH education0Price = new UAH(150);
		public static UAH education1Price = new UAH(5000);
		public static UAH education2Price = new UAH(25000);
		public static UAH education3Price = new UAH(200000);
		public static USD education4Price = new USD(100000);

		public static UAH raiting0Price = new UAH(1000);
		public static UAH raiting1Price = new UAH(50000);
		public static UAH raiting2Price = new UAH(150000);
		public static USD raiting3Price = new USD(10000);
		public static USD raiting4Price = new USD(5000000);
        public static RaitingStat raiting0Set = new RaitingStat(500);
        public static RaitingStat raiting1Set = new RaitingStat(50000);
        public static RaitingStat raiting2Set = new RaitingStat(100000);
        public static RaitingStat raiting3Set = new RaitingStat(250000);
        public static RaitingStat raiting4Set = new RaitingStat(500000);

		public static UAH property0Price = new UAH(400);
		public static UAH property1Price = new UAH(15000);
		public static UAH property2Price = new UAH(40000);
		public static USD property3Price = new USD(300000);
		public static USD property4Price = new USD(1000000);
		public static USD property5Price = new USD(3000000);
		public static USD property6Price = new USD(5000000);

		public static UAH transport0Price = new UAH(2000);
		public static UAH transport1Price = new UAH(10000);
		public static USD transport2Price = new USD(2000);
		public static USD transport3Price = new USD(50000);
		public static USD transport4Price = new USD(600000);
		public static USD transport5Price = new USD(2000000);

        public static UAH cityCouncil1Gift = new UAH(50);
        public static UAH cityCouncil2Gift = new UAH(50);

        public static UAH sport1Price = new UAH(15000);
        public static USD sport2Price = new USD(2000);

		public static short daysDied = 7;

		public static void SetSettings(EDificultLevel dificultLevel)
		{
			DificultLevel = dificultLevel;
			switch (DificultLevel)
			{
				case EDificultLevel.Easy:
					{
						Settings_.maxPriceForBottle = 10;
						Settings_.minPriceForBottle = 5;
						Settings_.maxPriceChangeForBottle = 3;
						Settings_.minPriceChangeForBottle = 0;
						Settings_.PositivePercentForBottle = 85;

						Settings_.maxPriceForUSD = 30;
						Settings_.minPriceForUSD = 18;
						Settings_.maxPriceChangeForUSD = 2;
						Settings_.minPriceChangeForUSD = 0;
						Settings_.PositivePercentForUSD = 85;
					}
					break;
				case EDificultLevel.Normal:
					{
						Settings_.maxPriceForBottle = 8;
						Settings_.minPriceForBottle = 2;
						Settings_.maxPriceChangeForBottle = 3;
						Settings_.minPriceChangeForBottle = 1;
						Settings_.PositivePercentForBottle = 65;

						Settings_.maxPriceForUSD = 45;
						Settings_.minPriceForUSD = 24;
						Settings_.maxPriceChangeForBottle = 3;
						Settings_.minPriceChangeForBottle = 0;
						Settings_.PositivePercentForUSD = 60;
					}
					break;
				case EDificultLevel.Hard:
					{
						Settings_.maxPriceForBottle = 6;
						Settings_.minPriceForBottle = 1;
						Settings_.maxPriceChangeForBottle = 1;
						Settings_.minPriceChangeForBottle = 0;
						Settings_.PositivePercentForUSD = 55;

						Settings_.maxPriceForUSD = 55;
						Settings_.minPriceForUSD = 30;
						Settings_.maxPriceChangeForUSD = 4;
						Settings_.minPriceChangeForUSD = 0;
						Settings_.PositivePercentForUSD = 55;
					}
					break;
			}
		}
	}
}
