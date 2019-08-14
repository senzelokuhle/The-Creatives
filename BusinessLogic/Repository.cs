using DataAccessLogic;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogic
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private EntityContext _context;
        private IDbSet<T> dbEntity;
        public Repository()
        {
            _context = new EntityContext();

            dbEntity = _context.Set<T>();
        }


        public IEnumerable<T> GetModel()
        {
            return dbEntity.ToList();
        }

        public T GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(T model)
        {
            dbEntity.Add(model);
        }
        public void DeleteModel(int modelID)
        {
            T model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
        }

        public void UpdateModel(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
