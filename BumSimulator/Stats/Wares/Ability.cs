using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BumSimulator.Stats.Valutas;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace BumSimulator.Stats.Wares
{
	interface IAbility
	{
		bool DoEffect();
	}
    abstract class Ability : IAbility
    {
        public IStat MainStat { get; set; }
        public IObject TempStat { get; set; }

        public Ability()
        {
            TempStat = MainStat = null;
        }
        public Ability(IStat MainStat)
        {
            this.MainStat = MainStat;
            TempStat = null;
        }
        public Ability(IStat MainStat, IObject TempStat)
        {
            this.MainStat = MainStat;
            this.TempStat = TempStat;
        }

        public virtual bool DoEffect() { return true; }
    }
    class ChangeAbility : IAbility
    {
		public delegate void deleg();
		public deleg deleg2 { get; set; }

		public ChangeAbility(deleg Temp)
		{
			deleg2 = Temp;
		}

        public bool DoEffect()
        {
			deleg2();
            return true;
        }
    }

    class NegativeAbility : Ability
    {
        public NegativeAbility() : base() { }
        public NegativeAbility(IStat MainStat) : base(MainStat) { }
        public NegativeAbility(IStat MainStat, IObject TempStat) : base(MainStat, TempStat) { }

        public override bool DoEffect()
        {
            if (TempStat != null && MainStat != null)
            {
                return MainStat.NegativeEffect(TempStat);
            }
            return false;
        }
    }

	class PositiveAbility : Ability
	{
        public PositiveAbility() : base() { }
        public PositiveAbility(IStat MainStat) : base(MainStat) { }
        public PositiveAbility(IStat MainStat, IObject TempStat) : base(MainStat, TempStat) { }

        public override bool DoEffect()
		{
			if (TempStat != null && MainStat != null)
			{
				return MainStat.PositiveEffect(TempStat);
			}
			return false;
		}
	}

	class CheckAbility : Ability
	{
        public CheckAbility() : base() { }
        public CheckAbility(IStat MainStat) : base(MainStat) { }
        public CheckAbility(IStat MainStat, IObject TempStat) : base(MainStat, TempStat) { }

		public bool Check()
		{
			if (TempStat != null && MainStat != null)
			{
				if (MainStat.Is(TempStat) == true)
				{
					return true;
				}
			}
			return false;
		}
	}
}