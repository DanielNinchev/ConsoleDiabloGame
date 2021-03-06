using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels
{
    public abstract class Entity : IEntity
    {
        public Entity(int id, bool isDeleted, DateTime dateCreated)
        {
            this.Id = id;
            this.IsDeleted = IsDeleted;
            this.DateCreated = dateCreated;
        }

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
