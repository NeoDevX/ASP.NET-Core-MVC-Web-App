namespace WebApp.Services.Data
{
    public interface IDataUpdater
    {
        bool Update<T>(T entity) where T : class;
        bool Add<T>(T entity) where T : class;
        bool Delete<T>(T entity) where T : class;
        bool Save();
    }
}