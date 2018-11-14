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
    class TransportStat : IStat
    {
        List<ETransport> transports;
        public List<ETransport> Transports
        {
            get { return transports; }
            set
            {
                transports = value;
                OnPropertyChanged("HighestTransport");
            }
        }
		public ETransport HighestTransport
		{
			get
			{
                if(Transports.Count >= 0)
                {
                    ETransport tmp = Transports[0];
                    foreach (ETransport x in Transports)
                    {
                        if ((byte)x > (byte)tmp)
                        {
                            tmp = x;
                        }
                    }
                    return tmp;
                }
                return ETransport.WithoutShoes;
			}
		}

		public TransportStat()
		{
			Transports = new List<ETransport>();
			Transports.Add(ETransport.WithoutShoes);
		}
		public TransportStat(ETransport transport)
		{
			Transports = new List<ETransport>();
			Transports.Add(transport);
		}
		public TransportStat(List<ETransport> transports)
        {
			Transports = new List<ETransport>(transports);
        }

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is TransportStat)
			{
				if ((otherStat as TransportStat).Transports != null)
				{
					foreach (ETransport x in (otherStat as TransportStat).Transports)
					{
						if (this.Transports.Contains(x) == false)
						{
							this.Transports.Add(x);
							OnPropertyChanged("HighestTransport");
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is TransportStat)
			{
				if ((otherStat as TransportStat).Transports != null)
				{
					foreach (ETransport x in (otherStat as TransportStat).Transports)
					{
						if (Transports.Contains(x))
						{
							Transports.Remove(x);
							OnPropertyChanged("HighestTransport");
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool Is(IStat TransportStat)
		{
			if (TransportStat is TransportStat)
			{
				foreach (ETransport x in (TransportStat as TransportStat).Transports)
				{
                    if (Transports.Contains(x) == false || (byte)HighestTransport < (byte)x)
					{
						System.Windows.MessageBox.Show("Потрібно " + x.ToString());
						return false;
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
