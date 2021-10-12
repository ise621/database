// Inspired by https://weblog.west-wind.com/posts/2017/dec/12/easy-configuration-binding-in-aspnet-core-revisited

namespace Database
{
    public sealed class AppSettings
    {
        public string Host { get; set; }
        = "";

        public string MetabaseHost { get; set; }
        = "";

        public LoggingSettings Logging { get; set; }
        = new LoggingSettings();

        public sealed class LoggingSettings
        {
            public bool EnableSensitiveDataLogging { get; set; }
            = false;
        }

        public EmailSettings Email { get; set; }
        = new EmailSettings();

        public sealed class EmailSettings
        {
            public string SmtpHost { get; set; }
            = "";

            public int SmtpPort { get; set; }
            = 0;
        }

        public string OpenIdConnectClientSecret { get; set; }
        = "";

        public DatabaseSettings Database { get; set; }
        = new DatabaseSettings();

        public sealed class DatabaseSettings
        {
            public string ConnectionString { get; set; }
            = "";

            public string SchemaName { get; set; }
            = "";
        }
    }
}