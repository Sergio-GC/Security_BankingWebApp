namespace DTO
{
    public class User
    {
        public string accountNb { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public List<Account> accounts { get; set; }
    }
}