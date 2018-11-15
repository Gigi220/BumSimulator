using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BumSimulator.Stats;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.MainGame;
using BumSimulator.Stats.Valutas;
using BumSimulator.Events;
using BumSimulator.Stats.Wares;
using System.Windows.Media.Animation;
using BumSimulator.Settings;
using BumSimulator.Enums;

namespace BumSimulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	public partial class MainPage : Page
	{
		Game Game;

		public MainPage()
		{
			InitializeComponent();

			Settings_.SetSettings(EDificultLevel.Normal);

            UIControls.PropertyBackground = PropertyBackground;
			UIControls.MoodBar = MoodBar;
			UIControls.ChangesMoodBar = ChangesMoodBar;
			UIControls.HpBar = HpBar;
			UIControls.ChangesHpBar = ChangesHpBar;
			UIControls.FoodBar = FoodBar;
			UIControls.ChangesFoodBar = ChangesFoodBar;
            UIControls.Page = this;

			Game = new Game(new User("Jack", new TimeStat(), new Valutes(new UAH(2000000), new USD(5000000)), new BottlesStat(20), new TransportStat(),
				new PropertyStat(), new RaitingStat(0), new StatusStat(Enums.EStatus.Homeless), new EducStat(), new MoodStat(50), new HpStat(50), new FoodStat(50)));

			Game.User.TopHead = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\TopHead\Male\TopHead.png"))));
			Game.User.Head = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\Head\Male\Head.png"))));
			Game.User.Tors = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\Tors\Male\Tors.png"))));

			Game.User.LeftHand = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\LeftHand\Male\LeftHand.png"))));
			Game.User.RightHand = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\RightHand\Male\RightHand.png"))));

			Game.User.Pants = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\Pants\Male\Pants.png"))));
			Game.User.Bottom = new ImgStat(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Img\StaffsPart\Bottom\Male\Bottom.png"))));
			

			this.DataContext = Game;


			SetIEvent();
		}

		void SetIEvent()
		{
            //їжа
			CheckEvent Food0Event = new CheckEvent(Food0Button, new List<IAbility>() { new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new PositiveAbility(Game.User.Food, new FoodStat(7)) });
			CheckEvent Food1Event = new CheckEvent(Food1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Food1Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, new UAH(100)), new NegativeAbility(Game.User.Mood, new MoodStat(3)), new NegativeAbility(Game.User.Hp, new HpStat(3)), new PositiveAbility(Game.User.Food, new FoodStat(15)) });
			CheckEvent Food2Event = new CheckEvent(Food2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Food2Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, new UAH(500)), new PositiveAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(2)), new PositiveAbility(Game.User.Food, new FoodStat(35)) });
			CheckEvent Food3Event = new CheckEvent(Food3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Food3Price) },new List<IAbility>() { new NegativeAbility(Game.User.Valutes, new UAH(3000)), new PositiveAbility(Game.User.Mood, new MoodStat(8)), new PositiveAbility(Game.User.Hp, new HpStat(6)), new PositiveAbility(Game.User.Food, new FoodStat(55)) });
            Food0Event.Start(Game);
            Food1Event.Start(Game);
            Food2Event.Start(Game);
            Food3Event.Start(Game);

            //настрій
			CheckEvent Mood0Event = new CheckEvent(Mood0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Mood0Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Mood0Price), new PositiveAbility(Game.User.Mood, new MoodStat(15)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(4)) });
			CheckEvent Mood1Event = new CheckEvent(Mood1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Mood1Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Mood1Price), new PositiveAbility(Game.User.Mood, new MoodStat(35)), new NegativeAbility(Game.User.Hp, new HpStat(3)), new NegativeAbility(Game.User.Food, new FoodStat(4)) });
			CheckEvent Mood2Event = new CheckEvent(Mood2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Mood2Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Mood2Price), new PositiveAbility(Game.User.Mood, new MoodStat(45)), new NegativeAbility(Game.User.Hp, new HpStat(2)), new NegativeAbility(Game.User.Food, new FoodStat(2)) });
			CheckEvent Mood3Event = new CheckEvent(Mood3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Mood3Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Mood3Price), new PositiveAbility(Game.User.Mood, new MoodStat(75)), new NegativeAbility(Game.User.Hp, new HpStat(1)), new NegativeAbility(Game.User.Food, new FoodStat(2)) });
            SubscribeEvent Mood4Event = new SubscribeEvent(Mood4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Mood4Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Mood4Price), new PositiveAbility(Game.User.Mood, new MoodStat(15)), new NegativeAbility(Game.User.Hp, new HpStat(1)), new NegativeAbility(Game.User.Food, new FoodStat(1)) }, new TimeStat(0, 1));
			Mood0Event.Start(Game);
            Mood1Event.Start(Game);
            Mood2Event.Start(Game);
            Mood3Event.Start(Game);
            Mood4Event.Start(Game, Settings_.Mood4Price);

            //hp
			CheckEvent Hp0Event = new CheckEvent(Healthy0Button, new List<IAbility>() { new NegativeAbility(Game.User.Mood, new MoodStat(4)), new PositiveAbility(Game.User.Hp, new HpStat(7)), new NegativeAbility(Game.User.Food, new FoodStat(4)) });
			CheckEvent Hp1Event = new CheckEvent(Healthy1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Hp1Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Hp1Price), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new PositiveAbility(Game.User.Hp, new HpStat(15)), new NegativeAbility(Game.User.Food, new FoodStat(3)) });
			CheckEvent Hp2Event = new CheckEvent(Healthy2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Hp2Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Hp2Price), new NegativeAbility(Game.User.Mood, new MoodStat(3)), new PositiveAbility(Game.User.Hp, new HpStat(30)), new NegativeAbility(Game.User.Food, new FoodStat(3)) });
			CheckEvent Hp3Event = new CheckEvent(Healthy3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Hp3Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Hp3Price), new PositiveAbility(Game.User.Mood, new MoodStat(1)), new PositiveAbility(Game.User.Hp, new HpStat(45)), new PositiveAbility(Game.User.Food, new FoodStat(1)) });
			CheckEvent Hp4Event = new CheckEvent(Healthy4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.Hp4Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.Hp4Price), new PositiveAbility(Game.User.Mood, new MoodStat(2)), new PositiveAbility(Game.User.Hp, new HpStat(65)), new PositiveAbility(Game.User.Food, new FoodStat(3)) });
            Hp0Event.Start(Game);
            Hp1Event.Start(Game);
            Hp2Event.Start(Game);
            Hp3Event.Start(Game);
            Hp4Event.Start(Game);

			//пробєжка
            CheckEvent Sport0Event = new CheckEvent(Sport0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.JoggingShoes)) }, new List<IAbility>() { new PositiveAbility(Game.User.Mood, new MoodStat(2)), new PositiveAbility(Game.User.Hp, new HpStat(8)), new NegativeAbility(Game.User.Food, new FoodStat(4)) });
            Sport0Event.Start(Game);
			//спорт
            SubscribeEvent Sport1Event = new SubscribeEvent(Sport1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.sport1Price) }, new List<IAbility>() { new PositiveAbility(Game.User.Mood, new MoodStat(1)), new PositiveAbility(Game.User.Hp, new HpStat(15)), new NegativeAbility(Game.User.Food, new FoodStat(4)) }, new TimeStat(0, 30));
            SubscribeEvent Sport2Event = new SubscribeEvent(Sport2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.sport2Price) }, new List<IAbility>() { new PositiveAbility(Game.User.Mood, new MoodStat(3)), new PositiveAbility(Game.User.Hp, new HpStat(25)), new NegativeAbility(Game.User.Food, new FoodStat(6)) }, new TimeStat(0, 30));
            Sport1Event.Start(Game);
            Sport2Event.Start(Game);

            //бомжувать
            CheckEvent Sneaking0Event = new CheckEvent(Sneaking0Button, new List<IAbility>() { new PositiveAbility(Game.User.Bottles, new BottlesStat(4)), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new PositiveAbility(Game.User.Raiting, new RaitingStat(1)) });
            CheckEvent Sneaking1Event = new CheckEvent(Sneaking1Button, new List<IAbility>() { new PositiveAbility(Game.User.Bottles, new BottlesStat(6)), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new PositiveAbility(Game.User.Raiting, new RaitingStat(2)) });
            CheckEvent Sneaking2Event = new CheckEvent(Sneaking2Button, new List<IAbility>() { new PositiveAbility(Game.User.Bottles, new BottlesStat(8)), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(5)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new PositiveAbility(Game.User.Raiting, new RaitingStat(3)) });
            CheckEvent Sneaking3Event = new CheckEvent(Sneaking3Button, new List<IAbility>() { new PositiveAbility(Game.User.Bottles, new BottlesStat(12)), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(7)), new NegativeAbility(Game.User.Food, new FoodStat(6)), new PositiveAbility(Game.User.Raiting, new RaitingStat(4)) });
            Sneaking0Event.Start(Game);
            Sneaking1Event.Start(Game);
            Sneaking2Event.Start(Game);
            Sneaking3Event.Start(Game);

			//побори
            CheckEvent LiveByBegging0Event = new CheckEvent(LiveByBegging0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.JoggingShoes)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, new UAH(250)), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new NegativeAbility(Game.User.Raiting, new RaitingStat(1)) });
            CheckEvent LiveByBegging1Event = new CheckEvent(LiveByBegging1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.JoggingShoes)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, new UAH(560)), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new NegativeAbility(Game.User.Raiting, new RaitingStat(2)) });
            CheckEvent LiveByBegging2Event = new CheckEvent(LiveByBegging2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.CheapCar)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, new UAH(2550)), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(5)), new NegativeAbility(Game.User.Food, new FoodStat(5)), new PositiveAbility(Game.User.Raiting, new RaitingStat(3)) });
            CheckEvent LiveByBegging3Event = new CheckEvent(LiveByBegging3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.SportCar)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, new UAH(24050)), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(7)), new NegativeAbility(Game.User.Food, new FoodStat(6)), new PositiveAbility(Game.User.Raiting, new RaitingStat(4)) });
            CheckEvent LiveByBegging4Event = new CheckEvent(LiveByBegging4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Transports, new TransportStat(ETransport.Helicopter)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, new USD(124050)), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(7)), new NegativeAbility(Game.User.Food, new FoodStat(6)), new PositiveAbility(Game.User.Raiting, new RaitingStat(4)) });
            LiveByBegging0Event.Start(Game);
            LiveByBegging1Event.Start(Game);
            LiveByBegging2Event.Start(Game);
            LiveByBegging3Event.Start(Game);
            LiveByBegging4Event.Start(Game);

            //робота
            CheckEvent Work0Event = new CheckEvent(Work0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Tent)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.School)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work0Price), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(3)) });
            CheckEvent Work1Event = new CheckEvent(Work1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Tent)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.School)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work1Price), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)) });
            CheckEvent Work2Event = new CheckEvent(Work2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Room)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.College)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work2Price), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(6)), new NegativeAbility(Game.User.Food, new FoodStat(4)) });
            CheckEvent Work3Event = new CheckEvent(Work3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Mortgage)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.College)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work3Price), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(6)), new NegativeAbility(Game.User.Food, new FoodStat(5)) });
            CheckEvent Work4Event = new CheckEvent(Work4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Apartments)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.University)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work4Price), new NegativeAbility(Game.User.Mood, new MoodStat(5)), new NegativeAbility(Game.User.Hp, new HpStat(4)), new NegativeAbility(Game.User.Food, new FoodStat(5)) });
            CheckEvent Work5Event = new CheckEvent(Work5Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Office)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.StudyInEngland)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work5Price), new NegativeAbility(Game.User.Mood, new MoodStat(4)), new NegativeAbility(Game.User.Hp, new HpStat(3)), new NegativeAbility(Game.User.Food, new FoodStat(5)) });
            CheckEvent Work6Event = new CheckEvent(Work6Button, new List<CheckAbility>() { new CheckAbility(Game.User.Properties, new PropertyStat(EProperty.Office)), new CheckAbility(Game.User.Educ, new EducStat(EEduc.StudyInEngland)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.work6Price), new NegativeAbility(Game.User.Mood, new MoodStat(3)), new NegativeAbility(Game.User.Hp, new HpStat(3)), new NegativeAbility(Game.User.Food, new FoodStat(6)) });
            Work0Event.Start(Game);
            Work1Event.Start(Game);
            Work2Event.Start(Game);
            Work3Event.Start(Game);
            Work4Event.Start(Game);
            Work5Event.Start(Game);
            Work6Event.Start(Game);

            //освіта
            EnableCheckEvent Education0 = new EnableCheckEvent(Education0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.education0Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.education0Price), new PositiveAbility(Game.User.Educ, new EducStat(EEduc.MultiTable)), new PositiveAbility(Game.User.Raiting, new RaitingStat(100)) });
            EnableCheckEvent Education1 = new EnableCheckEvent(Education1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.education1Price), new CheckAbility(Game.User.Passport, new PassportStat(true)), new CheckAbility(Game.User.Transports, new TransportStat(ETransport.JoggingShoes)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.education1Price), new PositiveAbility(Game.User.Educ, new EducStat(EEduc.School)), new PositiveAbility(Game.User.Raiting, new RaitingStat(1000)) });
            EnableCheckEvent Education2 = new EnableCheckEvent(Education2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.education2Price), new CheckAbility(Game.User.Transports, new TransportStat(ETransport.Bike)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.education2Price), new PositiveAbility(Game.User.Educ, new EducStat(EEduc.College)), new PositiveAbility(Game.User.Raiting, new RaitingStat(5000)) });
            EnableCheckEvent Education3 = new EnableCheckEvent(Education3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.education3Price), new CheckAbility(Game.User.Transports, new TransportStat(ETransport.CheapCar)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.education3Price), new PositiveAbility(Game.User.Educ, new EducStat(EEduc.University)), new PositiveAbility(Game.User.Raiting, new RaitingStat(10000)) });
            EnableCheckEvent Education4 = new EnableCheckEvent(Education4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.education4Price), new CheckAbility(Game.User.Transports, new TransportStat(ETransport.SportCar)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.education4Price), new PositiveAbility(Game.User.Educ, new EducStat(EEduc.StudyInEngland)), new PositiveAbility(Game.User.Raiting, new RaitingStat(20000)) });
            Education0.Start(Game);
            Education1.Start(Game);
            Education2.Start(Game);
            Education3.Start(Game);
            Education4.Start(Game);

			//рейтинг
            CheckEvent Raiting0Event = new CheckEvent(Raiting0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.raiting0Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.raiting0Price), new PositiveAbility(Game.User.Raiting, Settings_.raiting0Set) });
            CheckEvent Raiting1Event = new CheckEvent(Raiting1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.raiting1Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.raiting1Price), new PositiveAbility(Game.User.Raiting, Settings_.raiting1Set) });
            CheckEvent Raiting2Event = new CheckEvent(Raiting2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.raiting2Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.raiting2Price), new PositiveAbility(Game.User.Raiting, Settings_.raiting2Set) });
            CheckEvent Raiting3Event = new CheckEvent(Raiting3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.raiting3Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.raiting3Price), new PositiveAbility(Game.User.Raiting, Settings_.raiting3Set) });
            CheckEvent Raiting4Event = new CheckEvent(Raiting4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.raiting4Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.raiting4Price), new PositiveAbility(Game.User.Raiting, Settings_.raiting4Set) });
            Raiting0Event.Start(Game);
            Raiting1Event.Start(Game);
            Raiting2Event.Start(Game);
            Raiting3Event.Start(Game);
            Raiting4Event.Start(Game);

            //нерухомість
            BuySellCheckEvent Property0Event = new BuySellCheckEvent(Realty0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property0Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.property0Price), new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Tent)) });
            SubscribeEvent Property1Event = new SubscribeEvent(Realty1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property1Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() {  new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Room)) }, new TimeStat(0, 5));
            SubscribeEvent Property2Event = new SubscribeEvent(Realty2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property2Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Mortgage)) }, new TimeStat(0, 30));
            BuySellCheckEvent Property3Event = new BuySellCheckEvent(Realty3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property3Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.property3Price), new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Apartments)) });
            BuySellCheckEvent Property4Event = new BuySellCheckEvent(Realty4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property4Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.property4Price), new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Office)) });
            BuySellCheckEvent Property5Event = new BuySellCheckEvent(Realty5Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property5Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.property5Price), new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Cottage)) });
            BuySellCheckEvent Property6Event = new BuySellCheckEvent(Realty6Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.property6Price), new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.property6Price), new PositiveAbility(Game.User.Properties, new PropertyStat(EProperty.Villa)) });
            Property0Event.Start(Game);
            Property1Event.Start(Game, Settings_.property1Price);
            Property2Event.Start(Game, Settings_.property2Price);
            Property3Event.Start(Game);
            Property4Event.Start(Game);
            Property5Event.Start(Game);
            Property6Event.Start(Game);

			//транспорт
            EnableCheckEvent Transport0Event = new EnableCheckEvent(Transport0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport0Price) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport0Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.JoggingShoes)) });
            EnableCheckEvent Transport1Event = new EnableCheckEvent(Transport1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport1Price), new CheckAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport1Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.Bike)) });
            EnableCheckEvent Transport2Event = new EnableCheckEvent(Transport2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport2Price), new CheckAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport2Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.CheapCar)) });
            EnableCheckEvent Transport3Event = new EnableCheckEvent(Transport3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport3Price), new CheckAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport3Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.SportCar)) });
            EnableCheckEvent Transport4Event = new EnableCheckEvent(Transport4Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport4Price), new CheckAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport4Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.Helicopter)) });
            EnableCheckEvent Transport5Event = new EnableCheckEvent(Transport5Button, new List<CheckAbility>() { new CheckAbility(Game.User.Valutes, Settings_.transport5Price), new CheckAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) }, new List<IAbility>() { new NegativeAbility(Game.User.Valutes, Settings_.transport5Price), new PositiveAbility(Game.User.Transports, new TransportStat(ETransport.Yacht)) });
            Transport0Event.Start(Game);
            Transport1Event.Start(Game);
            Transport2Event.Start(Game);
            Transport3Event.Start(Game);
            Transport4Event.Start(Game);
            Transport5Event.Start(Game);

            //соціальні послуги
            EnableCheckEvent CityCouncil0Event = new EnableCheckEvent(CityCouncil0Button, new List<CheckAbility>() { new CheckAbility(Game.User.Raiting, new RaitingStat(2000)) }, new List<IAbility>() { new PositiveAbility(Game.User.Passport, new PassportStat(true)) });
            SubscribeEvent CityCouncil1Event = new SubscribeEvent(CityCouncil1Button, new List<CheckAbility>() { new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.cityCouncil1Gift) }, new TimeStat(0, 1));
            SubscribeEvent CityCouncil2Event = new SubscribeEvent(CityCouncil2Button, new List<CheckAbility>() { new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.cityCouncil2Gift) }, new TimeStat(0, 1));
			EnableCheckEvent CityCouncil3Event = new EnableCheckEvent(CityCouncil3Button, new List<CheckAbility>() { new CheckAbility(Game.User.Passport, new PassportStat(true)) }, new List<IAbility>() { new PositiveAbility(Game.User.Valutes, Settings_.cityCouncil2Gift), new PositiveAbility(Game.User.DriverLicense, new DriverLicenseStat(true)) });
            CityCouncil0Event.Start(Game);
            CityCouncil1Event.Start(Game);
            CityCouncil2Event.Start(Game);
			CityCouncil3Event.Start(Game);

		}

		private void BottlesCountPassButton_Click(object sender, RoutedEventArgs e)
		{
			if (BottlesCountSelling.SelectedItem != null)
			{
				Game.User.Valutes.PositiveEffect(new UAH(Game.User.Bottles.Count * Game.BottlePrice.Price.Count));
				Game.User.Bottles.NegativeEffect(new BottlesStat(Int32.Parse(((ComboBoxItem)BottlesCountSelling.SelectedItem).Content.ToString())));
			}
		}

		private void BottlesAllPassButton_Click(object sender, RoutedEventArgs e)
		{
			Game.User.Valutes.PositiveEffect(new UAH(Game.User.Bottles.Count * Game.BottlePrice.Price.Count));
			Game.User.Bottles.NegativeEffect(new BottlesStat(Game.User.Bottles.Count));
		}

		private void USDCountPassButton_Click(object sender, RoutedEventArgs e)
		{
			if (USDCountSelling.SelectedItem != null)
			{
				if(Game.User.Valutes.NegativeEffect(new UAH((int)(Int32.Parse(((ComboBoxItem)USDCountSelling.SelectedItem).Content.ToString()) * Game.USDPrice.Price))))
				{
					Game.User.Valutes.PositiveEffect(new USD(Int32.Parse(((ComboBoxItem)USDCountSelling.SelectedItem).Content.ToString())));
				}

				
			}
		}

		private void USDAllPassButton_Click(object sender, RoutedEventArgs e)
		{
			int buf = Game.User.Valutes.UAH.Count;
			int buf2 = (int)(Game.User.Valutes.UAH.Count % Game.USDPrice.Price);
			MessageBox.Show(buf2.ToString());
			if (Game.User.Valutes.NegativeEffect(new UAH(Game.User.Valutes.UAH.Count - buf2)))
			{
				Game.User.Valutes.PositiveEffect(new USD((int)(buf / Game.USDPrice.Price)));
			}
		}

		private void Canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double tempH = ((Canvas)sender).Height / 100;
			double tempW = ((Canvas)sender).Width / 100;
			DriverLicenseText.FontSize = tempH * 6;
			Canvas.SetTop(DriverLicenseText, tempH * 65);
			Canvas.SetLeft(DriverLicenseText, tempW * 65);
			
		}
	}
}
