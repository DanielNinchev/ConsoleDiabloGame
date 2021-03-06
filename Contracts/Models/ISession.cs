using ConsoleDiablo.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface ISession
    {
        string AccountName { get; }

        int AccountId { get; }

        bool IsLoggedIn { get; }

        IMenu CurrentMenu { get; set; }

        void LogIn(Account account);

        void LogOut();

        IMenu Back();

        bool PushView(IMenu view);

        void Reset();
    }
}
