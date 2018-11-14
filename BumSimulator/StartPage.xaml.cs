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

namespace BumSimulator
{
	/// <summary>
	/// Interaction logic for StartPage.xaml
	/// </summary>
	public partial class StartPage : Page
	{
		public StartPage()
		{
			InitializeComponent();
		}

		private void NewGameButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationWindow win = (NavigationWindow)Window.GetWindow(this);
			win.Content = new MainPage();
			win.Show();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}
	}
}
