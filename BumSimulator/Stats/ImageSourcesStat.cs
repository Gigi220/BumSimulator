using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace BumSimulator.Stats
{
	class ImgStat : INotifyPropertyChanged
	{
		ImageSource imageSource;
		public ImageSource ImageSource
		{
			get { return imageSource; }
			set
			{
				imageSource = value;
				OnPropertyChanged("imageSource");
			}
		}
		public ImgStat(ImageSource ImageSource)
		{
			this.ImageSource = ImageSource;
		}

		public void SetImg(ImageSource ImageSource)
		{
			this.ImageSource = ImageSource;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
