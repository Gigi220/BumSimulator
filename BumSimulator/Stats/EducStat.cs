using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BumSimulator.Enums;

namespace BumSimulator.Stats
{
	class EducStat : IStat
	{
		List<EEduc> educ;
		public List<EEduc> Educ
		{
			get { return educ; }
			set
			{
				educ = value;
				OnPropertyChanged("HighestProperty");
			}
		}
		public EEduc HighestProperty
		{
			get
			{
				EEduc tmp = educ[0];
				foreach (EEduc x in educ)
				{
					if ((byte)x > (byte)tmp)
					{
						tmp = x;
					}
				}
				return tmp;
			}
		}
		public EducStat()
		{
			Educ = new List<EEduc>();
			Educ.Add(EEduc.None);
		}
		public EducStat(EEduc educ)
		{
			Educ = new List<EEduc>();
			Educ.Add(educ);
		}
		public EducStat(List<EEduc> educ)
		{
			Educ = new List<EEduc>(educ);
		}

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is EducStat)
			{
				if ((otherStat as EducStat).Educ != null)
				{
					foreach (EEduc x in (otherStat as EducStat).Educ)
					{
						if (Educ.Contains(x) == false)
						{
							Educ.Add(x);
							OnPropertyChanged("HighestEduc");
							return true;
						}
					}
				}
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is EducStat)
			{
				if ((otherStat as EducStat).Educ != null)
				{
					foreach (EEduc x in (otherStat as EducStat).Educ)
					{
						if (Educ.Contains(x))
						{
							Educ.Remove(x);
							OnPropertyChanged("HighestEduc");
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool Is(IStat PropertyStat)
		{
			if (PropertyStat is EducStat)
			{
				if ((PropertyStat as EducStat).Educ != null)
				{
					foreach (EEduc x in (PropertyStat as EducStat).Educ)
					{
						if (Educ.Contains(x) == false)
						{
							System.Windows.MessageBox.Show("Потрібно " + x.ToString());
							return false;
						}
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
