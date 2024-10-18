using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POS.Interfaces
{
    public interface IDao<T> where T : class
    {
        IEnumerable<T> GetAll();        // Lấy tất cả các đối tượng T từ database
        T GetById(int id);              // Lấy đối tượng T theo ID
        void Add(T entity);             // Thêm đối tượng T vào database
        void Update(T entity);          // Cập nhật đối tượng T
        void Delete(int id);            // Xóa đối tượng T theo ID
    }
}