using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Contracts
{
    public interface IEntity
    {
        int Id { get; }

        bool IsDeleted { get; set; }

        DateTime DateCreated { get; }
    }
}
