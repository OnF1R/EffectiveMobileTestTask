using EffectiveMobileTestTask.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EffectiveMobileTestTask.Services
{
    public class DeliveryService
    {
        private Configurations _configurations;

        public DeliveryService (Configurations configurations)
        {
            _configurations = configurations;
        }

        public List<Delivery> Filter(List<Delivery> deliveries)
        {
            var filtered = new List<Delivery>();
            filtered = deliveries
                .Where(d => d.District == _configurations.СityDistrict 
                    && d.DeliveryDate < _configurations.FirstDeliveryDateTime.AddMinutes(30)
                    && d.DeliveryDate != _configurations.FirstDeliveryDateTime)
                .ToList();

            return filtered;
        }

        public void SaveDeliveryOrderResult(List<Delivery> deliveries)
        {
            using (FileStream fstream = new FileStream(_configurations.DeliveryOrder, FileMode.OpenOrCreate))
            {
                var serialized = JsonSerializer.Serialize(deliveries);
                byte[] buffer = Encoding.Default.GetBytes(serialized);
                fstream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
