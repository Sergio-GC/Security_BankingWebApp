using DAL;
using DTO;

namespace BLL
{
    public class UserManager : IUserManager
    {
        private IUserDB userDb;

        public UserManager(IUserDB userDb)
        {
            this.userDb = userDb;
        }

        public User getUserByLogin(string accountNb, string password)
        {
            return userDb.getUserByAccountAndPassword(accountNb, password);
        }

        public void createUser(User user)
        {
            userDb.createUser(user);
        }
    }
}