using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagment.Services.Employees
{
    public interface IDbEmployeeService<Entity, TEntity, in TPK> where TEntity : class where Entity : class
    {
        /// <summary>
        /// Get All Data
        /// </summary>       
        /// <returns></returns>
        IEnumerable<Entity> GetAll();

        /// <summary>
        /// Get Data by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Entity Get(TPK id);

        /// <summary>
        /// Create new data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Entity Create(TEntity entity);

        /// <summary>
        /// Update Existing records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Entity Update(TEntity entity);

        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Entity Delete(TPK id);

        /// <summary>
        /// Update specific property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="JsonPatcDocument"></param>
        /// <returns></returns>
        Entity Patch(JsonPatchDocument entity, TPK id);
    }
}
