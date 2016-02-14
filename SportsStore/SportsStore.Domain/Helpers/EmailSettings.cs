namespace SportsStore.Domain.Helpers
{
    public class EmailSettings
    {
        public string MailToAddress = "emailto@email.com";
        public string MailFromAddress = "emailfrom@email.com";
        public bool UseSsl = true;
        public string UserName = "emailfrom@email.com";
        public string Password = "xxxxxxx";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Sports_Store_Emails";
    }
}