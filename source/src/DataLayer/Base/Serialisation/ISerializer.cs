namespace DataLayer.Base.Serialisation
{
    public interface ISerializer<T>
    {
        void Serialize(T obj, string filePath);
        T Deserialize(string filePath);
    }
}
