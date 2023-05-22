using WebApp.Data;

namespace WebApp.Services.Data
{
    public class DataUpdater : IDataUpdater
    {
        private readonly ApplicationDataContext _dataContext;

        public DataUpdater(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Update<T>(T entity) where T : class
        {
            _dataContext.Update(entity);
            return Save();
        }

        public bool Add<T>(T entity) where T : class
        {
            _dataContext.Add(entity);
            return Save();
        }

        public bool Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}