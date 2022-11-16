using DTO;

namespace DAL
{
    public interface IAccountDB
    {
        List<Account> getAccountsByOwner(string owner);
        void createAccount(Account account);
    }
}