using DTO;

namespace DAL
{
    public interface IUserDB
    {
        void createUser(User user);
        User getUserByAccountAndPassword(string accountNb, string password);
    }
}