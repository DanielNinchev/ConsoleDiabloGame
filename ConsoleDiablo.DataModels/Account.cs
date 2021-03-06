using System;
using System.Collections.Generic;

namespace ConsoleDiablo.DataModels
{
    public class Account : Entity
    {
        public Account(): this(0,"","")
        {
            this.Characters = new List<int>();
        }

        public Account(int id, string accountName, string password) 
            : this(id,accountName,password, new List<int>())
        {
            this.Id = id;
            this.AccountName = accountName;
            this.Password = password;
        }

        public Account(int id, string accountName, string password, IEnumerable<int> characters) : base(id, false, DateTime.Now)
        {
            this.Id = id;
            this.AccountName = accountName;
            this.Password = password;
            this.Characters.AddRange(new List<int>(characters));
        }

        public List<int> Characters { get; set; } = new List<int>();
        public string AccountName { get; set; }
        public string Password { get; set; }


        //ID
        //IsDeleted
        //DateCreated
        //DateUpdated
        //UserIdCreated
        //UserIdUpdated
    }
}
