using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AccountManager : IAccountManager
    {
        private IAccountDB accountDB;
        public AccountManager(IAccountDB accountDB)
        {
            this.accountDB = accountDB;
        }

        public List<Account> getAccountsByOwner(string owner)
        {
            return accountDB.getAccountsByOwner(owner);
        }

        public void createAccount(Account account)
        {
            accountDB.createAccount(account);
        }
    }
}
