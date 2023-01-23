using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace WRMAutotests.Utility
{
    public class MailRepository
    {
        private readonly string mailServer, login, password;
        private readonly int port;
        private readonly bool ssl;

        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }

        public MailRepository(User user) : this("imap.gmail.com", 993, true, user.GetEmail(), user.GetGmailPassword())
        {

        }

        public IList<MimeMessage> GetUnreadEmails()
        {
            IList<MimeMessage> result = new List<MimeMessage>();
            using (var client = new ImapClient())
            {
                client.Connect(mailServer, port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                SearchResults results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
                foreach (var uniqueId in results.UniqueIds)
                {
                    MimeMessage message = inbox.GetMessage(uniqueId);
                    result.Add(message);
                }

                client.Disconnect(true);
            }
            return result;
        }


        public IList<MimeMessage> GetUnreadEmailsByPartOfSubjectAndReciveEmails(String partOfSubject, String email)
        {
            IList<MimeMessage> emails = GetUnreadEmails();
            IList<MimeMessage> result = new List<MimeMessage>();
            foreach (MimeMessage message in emails)
            {
                if (message.Subject.Contains(partOfSubject))
                {
                    if (message.GetRecipients().Any(r => r.Address.Equals(email)))
                    {
                        result.Add(message);
                    }
                }
            }
            return result;
        }

        public IList<MimeMessage> GetUnreadEmailsByPartOfSubjectAndRecivedAndAfterDateTimeEmails(String partOfSubject, String email, DateTime dateTime)
        {
            IList<MimeMessage> emails = GetUnreadEmailsByPartOfSubjectAndReciveEmails(partOfSubject, email);
            IList<MimeMessage> result = new List<MimeMessage>();
            foreach (MimeMessage message in emails)
            {
                if (message.Date.ToUniversalTime() >= dateTime.ToUniversalTime())
                {
                    result.Add(message);
                }

            }
            return result;
        }

        public IList<MimeMessage> GetUnreadEmailsByPartOfSubjectAndRecivedAndAfterDateTimeAndPartOfBodyEmails(String partOfSubject, String email, DateTime dateTime, String partOfBody)
        {
            IList<MimeMessage> emails = GetUnreadEmailsByPartOfSubjectAndRecivedAndAfterDateTimeEmails(partOfSubject, email, dateTime);
            IList<MimeMessage> result = new List<MimeMessage>();
            foreach (MimeMessage message in emails)
            {
                if (message.HtmlBody.Contains(partOfBody))
                {
                    result.Add(message);
                }

            }
            return result;
        }

        public IEnumerable<string> GetUnreadMailsBodys()
        {
            var messages = new List<string>();

            using (var client = new ImapClient())
            {
                client.Connect(mailServer, port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
                foreach (var uniqueId in results.UniqueIds)
                {
                    var message = inbox.GetMessage(uniqueId);

                    messages.Add(message.HtmlBody);

                    //Mark message as read
                    //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }

                client.Disconnect(true);
            }

            return messages;
        }

        public IEnumerable<string> GetAllMailsBodys()
        {
            var messages = new List<string>();

            using (var client = new ImapClient())
            {
                client.Connect(mailServer, port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                var results = inbox.Search(SearchOptions.All, SearchQuery.NotSeen);
                foreach (var uniqueId in results.UniqueIds)
                {
                    var message = inbox.GetMessage(uniqueId);

                    messages.Add(message.HtmlBody);

                    //Mark message as read
                    //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }

                client.Disconnect(true);
            }

            return messages;
        }
    }
}
