using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDiablo.DataModels
{
    public class Inventory : IInventory
    {
        private int id;
        private int load;
        private int characterId;

        public Inventory(int id, int characterId, int load, IEnumerable<int> itemIds)
        {
            this.id = id;
            this.characterId = characterId;
            this.load = load;
            this.ItemIds = new List<int>(itemIds);
        }

        public int Capacity => 60;

        public ICollection<int> ItemIds { get; set; } = new List<int>();

        public int CharacterId
        {
            get { return characterId; }
            set { characterId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Load
        {
            get { return load; }
            set { load = value; }
        }

    }
}
