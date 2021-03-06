using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Menus
{
    public interface IIdHoldingMenu : IMenu
    {
        int Id { get; set; }
        void SetId(params int[] ids);
    }
}
