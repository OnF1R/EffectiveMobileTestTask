using System.Text.Json;

namespace EffectiveMobileTestTask.Services
{
    public class JsonService<T>
    {
        public static T Read(string path)
        {
            string json = File.ReadAllText(path);

            T deserialized = JsonSerializer.Deserialize<T>(json);

            return deserialized;
        }
    }
}
