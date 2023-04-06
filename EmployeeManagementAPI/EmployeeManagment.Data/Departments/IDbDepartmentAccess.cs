using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagment.Services.Departments
{
    public interface IDbDepartmentAccess<Entity, TEntity, in TPK> where TEntity : class where Entity : class
    {
        /// <summary>
        /// Get All Data
        /// </summary>       
        /// <returns></returns>
        IEnumerable<Entity> GetAll();
    }
}
