using Dul.Articles;

namespace BookApp.Shared
{
    //public interface ICrudRepositoryBase<T, TIdentifier>
    //{
    //    Task<T> AddAsync(T model); // add 
    //    Task<List<T>> GetAllAsync(); // get list
    //    Task<T> GetByIdAsync(int id); // get detail
    //    Task<bool> UpdateAsync(T model); // update
    //    Task<bool> DeleteAsync(int id); //delete
    //}

    public interface IBookCrudRepository<T> : ICrudRepositoryBase<T, int>   
    {
        // Empty
    }
}
 