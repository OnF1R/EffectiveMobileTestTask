using EffectiveMobileTestTask.Models;
using EffectiveMobileTestTask.Services;
using System.Globalization;
using System.Text.Json;
using NLog;
using NLog.Targets;
namespace EffectiveMobileTestTask
{
    internal class Program
    {
        public const string CITY_DISTRICT = "_cityDistrict";
        public const string FIRST_DELIVERY_DATE_TIME = "_firstDeliveryDateTime";
        public const string DELIVERY_LOG = "_deliveryLog";
        public const string DELIVERY_ORDER = "_deliveryOrder";

        static void Main(string[] args)
        {
            try
            {
                var configurations = JsonService<Configurations>.Read(Globals.CONFIGURATION);

                for (int i = 0; i < args.Length; i += 2)
                {
                    switch (args[i])
                    {
                        case CITY_DISTRICT:
                            configurations.СityDistrict = args[i + 1];
                            break;
                        case FIRST_DELIVERY_DATE_TIME:
                            configurations.FirstDeliveryDateTime = DateTime.ParseExact(args[i + 1], Globals.FORMAT, CultureInfo.InvariantCulture);
                            break;
                        case DELIVERY_LOG:
                            configurations.DeliveryLog = args[i + 1];
                            break;
                        case DELIVERY_ORDER:
                            configurations.DeliveryOrder = args[i + 1];
                            break;
                    }
                }

                var target = new FileTarget();
                target.FileName = configurations.DeliveryLog;
                target.Layout = "${date:format=yy-MM-dd HH\\:MM\\:ss} ${logger} ${message}";
                NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
                Logger errorLogger = LogManager.GetLogger("ERROR");
                Logger infoLogger = LogManager.GetLogger("Info");

                infoLogger.Info("Configuration setuped");

                var deliveries = JsonService<RootDeliveries>.Read(Globals.DATA).Delivery;

                infoLogger.Info($"deliveries read from file {Globals.DATA}");

                var deliveryService = new DeliveryService(configurations);

                infoLogger.Info("Create devilery service");

                var filtered = deliveryService.Filter(deliveries);

                infoLogger.Info("Filter deliveries complete");

                deliveryService.SaveDeliveryOrderResult(filtered);

                infoLogger.Info("Save delivery order result complete");
            }
            #region Exceptions
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не найден файл");
                Console.WriteLine(ex.FileName);
                Console.ResetColor();
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка в файле с данными");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            #endregion Exception
        }
    }
}
