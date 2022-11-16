using DTO;

namespace BLL
{
    public interface IUserManager
    {
        void createUser(User user);
        User getUserByLogin(string accountNb, string password);
    }
}