namespace WRMAutotests.Utility
{
    public class User
    {
        private String email;
        private String password;
        private String gmailPassword = "";

        public User(String email, String password)
        {
            this.email = email;
            this.password = password;
        }

        public User(String email, String password, String gmailPassword)
        {
            this.email = email;
            this.password = password;
            this.gmailPassword = gmailPassword;
        }

        public String GetEmail()
        {
            return email;
        }

        public String GetPassword()
        {
            return password;
        }

        public String GetGmailPassword()
        {
            return gmailPassword;
        }

    }

}
