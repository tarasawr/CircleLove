namespace Services
{
    public interface ISaveSystem
    {
        void Save(object data);
        T Load<T>();
        bool HasSave();
        void DeleteSave();
    }
}