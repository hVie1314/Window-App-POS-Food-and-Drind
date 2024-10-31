using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POS.Interfaces
{
    public interface IDao<T>
    {
        // Retrieve all items with pagination, search, and sorting
        Tuple<int, List<T>> GetAll(
            int page = 1, 
            int rowsPerPage = 10, 
            string keyword = "", 
            bool ascending = true);

        // Insert a new item of type T
        int Insert(T entity);

        // Update an existing item of type T
        bool Update(T entity);

        // Delete an item by ID
        bool Delete(int id);

        // Retrieve a single item by ID
        T GetById(int id);
    }
}