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
	class ImgStat : IStat
	{
		ImageSource imageSource;
		ImageSource defaultSource;
		public ImageSource ImageSource
		{
			get { return imageSource; }
			set
			{
				imageSource = value;
				OnPropertyChanged("ImageSource");
			}
		}
		public ImageSource DefaultSource
		{
			get { return defaultSource; }
			set
			{
				defaultSource = value;
			}
		}

		public ImgStat()
		{
			this.ImageSource = null;
			this.DefaultSource = null;
		}
		public ImgStat(ImageSource ImageSource)
		{
			this.ImageSource = ImageSource;
		}
		public ImgStat(ImageSource ImageSource, ImageSource DefaultSource)
		{
			this.ImageSource = ImageSource;
			this.DefaultSource = DefaultSource;
		}

		public void SetImg(ImageSource ImageSource)
		{
			this.ImageSource = ImageSource;
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is ImgStat)
			{
				if((otherStat as ImgStat).ImageSource != null)
				{
					SetImg((otherStat as ImgStat).ImageSource);
				}
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is ImgStat)
			{
				if(this.DefaultSource != null)
				SetImg(DefaultSource);
				return true;
			}
			return false;
		}

		public bool Is(IStat PropertyStat)
		{
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
