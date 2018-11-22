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
using BumSimulator.Enums;

namespace BumSimulator.Stats
{
	class Item : IObject
	{
		public EItemIdentify ID { get; set; }
		public string Name { get; set; }
		public ImageSource Image { get; set; }

		public Item(EItemIdentify ID)
		{
			this.ID = ID;
			Name = null;
			Image = null;
		}
		public Item(EItemIdentify ID, ImageSource Image)
		{
			this.ID = ID;
			this.Image = Image;
		}
		public Item(EItemIdentify ID, string Name, ImageSource Image) : this(ID, Image)
		{
			this.Name = Name;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	class ItemsStat : IStat
	{
		EItemIdentify MainID;
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

		public ItemsStat(EItemIdentify MainID)
		{
			Items = new ObservableCollection<Item>();
			this.MainID = MainID;
		}
		public ItemsStat(EItemIdentify MainID, Item Item)
		{
			this.Items = new ObservableCollection<Item>() { Item };
			SelectedItem = Items[0];
			this.MainID = MainID;
		}
		public ItemsStat(EItemIdentify MainID, ItemsStat Items)
		{
			this.Items = new ObservableCollection<Item>();
			PositiveEffect(Items);
			this.MainID = MainID;
		}

		public virtual bool PositiveEffect(IObject otherStat)
		{
			if (otherStat is ItemsStat)
			{
				if ((otherStat as ItemsStat).Items != null || MainID == (otherStat as ItemsStat).MainID)
				{
					foreach (Item x in (otherStat as ItemsStat).Items)
					{
						if (this.Items.Contains(x) == false || MainID == x.ID)
						{
							this.Items.Add(x);
							return true;
						}
					}
				}
			}
			else if (otherStat is Item)
			{
				if ((otherStat as Item) != null)
				{
					if (this.Items.Contains((otherStat as Item)) == false)
					{
						this.Items.Add((otherStat as Item));
						return true;
					}
				}
			}
			return false;
		}
		public virtual bool NegativeEffect(IObject otherStat)
		{
			if (otherStat is ItemsStat)
			{
				if ((otherStat as ItemsStat).Items != null)
				{
					foreach (Item x in (otherStat as ItemsStat).Items)
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
			else if (otherStat is Item)
			{
				if ((otherStat as Item) != null)
				{
					if (this.Items.Contains((otherStat as Item)))
					{
						this.Items.Remove((otherStat as Item));
						if (SelectedItem == (otherStat as Item))
						{
							SelectedItem = Items[0];
						}
						return true;
					}
				}
			}
			return false;
		}

		public virtual bool Is(IObject ItemsStat)
		{
			if (ItemsStat is ItemsStat)
			{
				foreach (Item x in (ItemsStat as ItemsStat).Items)
				{
					if (Items.Contains(x))
					{
						return true;
					}
				}
			}
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
