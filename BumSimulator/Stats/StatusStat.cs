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
    class StatusStat : IStat
    {
        EStatus status;
        public EStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Points");
            }
        }
        public StatusStat(EStatus points)
        {
            Status = points;
        }

		public bool PositiveEffect(IStat otherStat)
		{
			if (otherStat is StatusStat)
			{
				Status = (otherStat as StatusStat).Status;
				return true;
			}
			return false;
		}
		public bool NegativeEffect(IStat otherStat)
		{
			if (otherStat is StatusStat)
			{
				Status = (otherStat as StatusStat).Status;
				return true;
			}
			return false;
		}

		public bool Is(IStat StatusStat)
		{
			if (StatusStat is StatusStat)
			{
				if(Status == (StatusStat as StatusStat).Status)
				{
					System.Windows.MessageBox.Show("Потрібно " + (StatusStat as StatusStat).Status.ToString() + " очків статусу");
					return true;
				}
			}
			return false;
		}

		public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
