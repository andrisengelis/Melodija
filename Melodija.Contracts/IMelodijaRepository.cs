using System.Linq;

namespace Melodija.Contracts
{
  public interface IMelodijaRepository<T> where T : class
  {
    IQueryable<T> FindAll(bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}