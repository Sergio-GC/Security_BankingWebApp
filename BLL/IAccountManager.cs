using DTO;

namespace BLL
{
    public interface IAccountManager
    {
        void createAccount(Account account);
        List<Account> getAccountsByOwner(string owner);
    }
}