using System.Globalization;

namespace EffectiveMobileTestTask.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string District { get; set; }
        public string Date { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Delivery(int id, double weight, string district, string date)
        {
            Id = id;
            Weight = weight;
            District = district;
            Date = date;
            DeliveryDate = DateTime.ParseExact(Date, Globals.FORMAT, CultureInfo.InvariantCulture);
        }
    }
}
