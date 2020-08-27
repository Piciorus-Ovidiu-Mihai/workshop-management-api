
namespace WorkshopsManagement.Models
{
    public class Account
    {
        private int id;
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        public Account(int id,string username, string password,
            string firstName, string lastName, string email)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public Account() { }
        public int getId()
        {
            return id;
        }

    }
}
