using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface IGameService
    {
        ICollection<int> ItemPool { get; set; }
        ICollection<int> PartyIds { get; set; }
        ICollection<int> Shop { get; set; }
    }
}
