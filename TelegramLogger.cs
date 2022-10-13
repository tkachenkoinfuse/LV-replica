using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using ServiceWithHangfire.DAL;
using X.Extensions.Logging.Telegram;

namespace ServiceWithHangfire
{
    public class TelegramLogger
    {
        //подключаем телеграмм логгер. Создаем объект логгера который будет логировать и отправлять сообщения в телеграмм.
        // перед использованием нужно вызвать registerTelegramLogger (в Startup например)

        public IConfiguration _config { get; private set; }
        //public static ILoggerFactory TLoggerFactory { get; set; }
        public static ILogger<Program> TLogger { get; set; }

        public void registerTelegramLogger()
        {
            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            TelegramBotSettings telegramBotSettings = _config.GetSection("TelegramBotSettings").Get<TelegramBotSettings>();
            var options = new TelegramLoggerOptions
            {
                AccessToken = telegramBotSettings.AccessToken,
                ChatId = telegramBotSettings.ChatId,
                LogLevel = LogLevel.Warning,
                Source = telegramBotSettings.Source
            };

            var factory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddTelegram(options)
                    .AddConsole();
            }
            );

            var logger = factory.CreateLogger<Program>();
            TLogger = logger;
        }
        
    }
}
