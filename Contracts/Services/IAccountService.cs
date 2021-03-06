using ConsoleDiablo.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface IAccountService
    {
        bool TryCreatingAnAccount(string accountName, string password);

        bool TryLoggingIn(string accountName, string password);

        string GetAccount(int accountId);

        Account GetAccountById(int accountId);

        int CountCharactersInAnAccount(int accountId);
    }
}
