using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Repository
{
  public abstract class MelodijaRepository<T> : IMelodijaRepository<T> where T : class
  {
    private readonly MelodijaContext _melodijaContext;

    protected MelodijaRepository(MelodijaContext melodijaContext)
    {
      _melodijaContext = melodijaContext;
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
      trackChanges ? _melodijaContext.Set<T>() : _melodijaContext.Set<T>().AsNoTracking();

    public void Create(T entity) => _melodijaContext.Set<T>().Add(entity);

    public void Update(T entity) => _melodijaContext.Set<T>().Update(entity);

    public void Delete(T entity) => _melodijaContext.Set<T>().Remove(entity);
  }
}