using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;

namespace BumSimulator.Stats
{
	class Item : INotifyPropertyChanged
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public ImageSource Image { get; set; }

		public Item()
		{
			ID = 0;
			Name = null;
			Image = null;
		}
		public Item(ImageSource Image)
		{
			this.Image = Image;
		}
		public Item(string Name, ImageSource Image) : this(Image)
		{
			this.Name = Name;
		}
		public Item(string Name, ImageSource Image, int ID) : this(Name, Image)
		{
			this.ID = ID;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	class ItemStat : IStat
	{
		public ObservableCollection<Item> Items { get; set; }

		Item selectedItem;
		public Item SelectedItem
		{
			get { return selectedItem; }
			set
			{
				selectedItem = value;
				OnPropertyChanged("SelectedItem");
			}
		}

		public ItemStat()
		{
			Items = new ObservableCollection<Item>();
		}
		public ItemStat(Item Item)
		{
			this.Items = new ObservableCollection<Item>() { Item };
			SelectedItem = Items[0];
		}
		public ItemStat(ObservableCollection<Item> Items)
		{
			this.Items = new ObservableCollection<Item>(Items);
		}

		public virtual bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is ItemStat)
			{
				if ((otherStat as ItemStat).Items != null)
				{
					foreach (Item x in (otherStat as ItemStat).Items)
					{
						if (this.Items.Contains(x) == false)
						{
							this.Items.Add(x);
							return true;
						}
					}
				}
			}
			return false;
		}
		public virtual bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is ItemStat)
			{
				if ((otherStat as ItemStat).Items != null)
				{
					foreach (Item x in (otherStat as ItemStat).Items)
					{
						if (Items.Contains(x))
						{
							Items.Remove(x);
							if(SelectedItem == x)
							{
								SelectedItem = Items[0];
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		public virtual bool Is(IStat ItemStat)
		{
			if (ItemStat is ItemStat)
			{
				foreach (Item x in (ItemStat as ItemStat).Items)
				{
					if (Items.Contains(x))
					{
						return true;
					}
				}
			}
			return true;
		}

		public void SelectItem()
		{
			SelectedItem = Items[1];
		}
		public void SelectItem(Item TempItem)
		{
			SelectedItem = TempItem;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
