using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.DataModels.Characters
{
	public abstract class Character : Entity, ICharacter
	{
        private string name;
		private string type;
		private double damage;
		private double defense;
		private double baseLife;
		private double life;
		private double baseMana;
		private double mana;
		private double lifeRegenerationMultiplier;
		private double manaRegenerationMultiplier;
		private double experience;
		private int level;
		private double fireResistance;
		private double lightningResistance;
		private double coldResistance;
		private double poisonResistance;
		private int gearId;
		private int inventoryId;
		private double moneyBalance;
		private int accountId;

		protected Character(int id,
			string name,
			string type,
			bool isDeleted,
			double damage,
			double defense,
			double baseLife,
			double baseMana,
			int gearId, int inventoryId, int accountId) : base(id, isDeleted, DateTime.Now)
		{
			this.Name = name;
			this.Type = type;
			this.Damage = damage;
			this.Defense = defense;
			this.BaseLife = baseLife;
			this.Life = baseLife;
			this.LifeRegenerationMultiplier = baseLife * 0.001;
			this.BaseMana = baseMana;
			this.Mana = baseMana;
			this.ManaRegenerationMultiplier = baseMana * 0.001;
			this.Experience = 0;
			this.Level = 1;
			this.FireResistance = 0;
			this.LightningResistance = 0;
			this.ColdResistance = 0;
			this.PoisonResistance = 0;
			this.GearId = gearId;
			this.InventoryId = inventoryId;
			this.MoneyBalance = 0;
			this.AccountId = accountId;
		}

        protected Character(
					int id,
					string name,
					string type,
					bool isDeleted,
					DateTime dateCreated,
					double damage,
					double defense,
					double baseLife,
					double life,
					double lifeRegenerationMultiplier,
					double baseMana,
					double mana,
					double manaRegenerationMultiplier,
					double experience,
					int level,
					double fireResistance,
					double lightningResistance,
					double coldResistance,
					double poisonResistance,
					int gearId,
					int inventoryId,
					double moneyBalance,
					int accountId) : base(id, isDeleted, dateCreated)
        {
			this.Name = name;
			this.Type = type;
			this.Damage = damage;
			this.Defense = defense;
			this.BaseLife = baseLife;
			this.Life = baseLife;
			this.LifeRegenerationMultiplier = baseLife * 0.001;
			this.BaseMana = baseMana;
			this.Mana = baseMana;
			this.ManaRegenerationMultiplier = baseMana * 0.001;
			this.Experience = experience;
			this.Level = (int)Math.Max(Math.Floor(experience / 1000), 1);
			this.FireResistance = fireResistance;
			this.LightningResistance = lightningResistance;
			this.ColdResistance = coldResistance;
			this.PoisonResistance = poisonResistance;
			this.GearId = gearId;
			this.InventoryId = inventoryId;
			this.MoneyBalance = moneyBalance;
			this.AccountId = accountId;
		}

        public string DamageType { get; set; }

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        public double PoisonResistance
		{
			get { return poisonResistance; }
			set { poisonResistance = value; }
		}

		public double ColdResistance
		{
			get { return coldResistance; }
			set { coldResistance = value; }
		}

		public double LightningResistance
		{
			get { return lightningResistance; }
			set { lightningResistance = value; }
		}

		public double FireResistance
		{
			get { return fireResistance; }
			set { fireResistance = value; }
		}

		public double MoneyBalance
		{
			get { return moneyBalance; }
			set { moneyBalance = value; }
		}

		public double Defense
		{
			get { return defense; }
			set { defense = value; }
		}

		public int Level
		{
			get { return level; }
			set { level = value; }
		}

		public double Experience
		{
			get { return experience; }
			set { experience = value; }
		}

		public double Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		public double ManaRegenerationMultiplier
		{
			get { return manaRegenerationMultiplier; }
			set { manaRegenerationMultiplier = value; }
		}

        public int GearId
        {
            get { return gearId; }
            set { gearId = value; }
        }

        public int InventoryId
        {
            get { return inventoryId; }
            set { inventoryId = value; }
        }

        public double LifeRegenerationMultiplier
		{
			get { return lifeRegenerationMultiplier; }
			set { lifeRegenerationMultiplier = value; }
		}

		public double Mana
		{
			get { return mana; }
			set { mana = value; }
		}

		public double BaseMana
		{
			get { return baseMana; }
			set { baseMana = value; }
		}

		public double Life
		{
			get { return life; }
			set { life = value; }
		}

		public double BaseLife
		{
			get { return baseLife; }
			set { baseLife = value; }
		}

		public string Type
        {
            get { return type; }
            set { type = value; }
        }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public bool IsAlive { get; set; } = true;

		public override string ToString()
		{
			const string format = "{0} - Level: {1}, Life: {2}/{3}, Mana: {4}/{5}, Status: {6}";

			var result = string.Format(format,
				Name,
				Level,
				Life,
				BaseLife,
				Mana,
				BaseMana,
				IsAlive ? "Alive" : "Dead");

			return result;
		}
	}
}
