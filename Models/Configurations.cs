using System.Globalization;

namespace EffectiveMobileTestTask.Models
{
    public class Configurations
    {
        public string СityDistrict { get; set; }
        public string FirstDeliveryDateTimeString { get; set; }
        public DateTime FirstDeliveryDateTime { get; set; }
        public string DeliveryLog { get; set; }
        public string DeliveryOrder { get; set; }

        public Configurations(string сityDistrict, string firstDeliveryDateTimeString, string deliveryLog, string deliveryOrder)
        {
            СityDistrict = сityDistrict;
            FirstDeliveryDateTimeString = firstDeliveryDateTimeString;
            FirstDeliveryDateTime = DateTime.ParseExact(FirstDeliveryDateTimeString, Globals.FORMAT, CultureInfo.InvariantCulture);
            DeliveryLog = deliveryLog;
            DeliveryOrder = deliveryOrder;
        }
    }
}
