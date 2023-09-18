using System.Text.Json;

namespace DataLayer.Base.Serialisation
{
    public class JsonSerialisationService<T> : ISerializer<T>
    {
        public T Deserialize(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Serialize(T obj, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"File path cannot be null or empty!");

            if (obj == null)
                throw new ArgumentException($"Data cannot be null!");

            if (File.Exists(filePath))
                File.Delete(filePath);

            using FileStream stream = File.Open(filePath, FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(stream, obj, obj.GetType(), JsonSerializerOptions.Default);
        }
    }
}
