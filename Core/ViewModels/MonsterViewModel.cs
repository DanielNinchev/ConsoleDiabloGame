using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Core.ViewModels
{
    public class MonsterViewModel : BeingViewModel
    {
        public MonsterViewModel(string name, double damage, double defense, double baseLife, double life,
            double fireRes, double lightRes, double coldRes, double poisonRes) 
            : base(name, damage, defense, baseLife, life, fireRes, lightRes, coldRes, poisonRes)
        {
        }
    }
}
