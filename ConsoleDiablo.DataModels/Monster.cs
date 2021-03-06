using ConsoleDiablo.App.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels
{
    public class Monster : IMonster
    {
        public Monster(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public double Life { get; set; }
        public double BaseLife { get; set; }
        public string DamageType { get ; set ; }
        public double Defense { get ; set ; }
        public double FireResistance { get ; set ; }
        public double ColdResistance { get ; set ; }
        public double LightningResistance { get ; set ; }
        public double PoisonResistance { get ; set ; }
        public bool IsAlive { get; set; } = true;
    }
}
