namespace Melodija.Contracts
{
  public interface IMelodijaRepository<T> where T : class
  {
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}