using System;
using System.Collections.Generic;

namespace Swappler.Services.Interfaces
{
    public interface IService<TEntity, TStatus>
    {
        /// <summary>
        /// Add entity record to database.
        /// </summary>
        /// <param name="entity">Entity which will be added.</param>
        /// <returns>True if entity is successfully added, false otherwise.</returns>
        TStatus Add(TEntity entity);

        /// <summary>
        /// Remove entity record from database.
        /// </summary>
        /// <param name="entity">Entity which will be removed.</param>
        /// <returns>True if entity is successfully removed, false otherwise.</returns>
        TStatus Remove(TEntity entity);

        /// <summary>
        /// Update specified fields for this entity or if updateFields parametar is null or has 0 length than
        /// all fields will be updated.
        /// </summary>
        /// <param name="entity">Entity which will be updated</param>
        /// <param name="updateFields">Names of user fields to be updated, null or 0 length means full update</param>
        /// <returns>True if entity is successfully updated, False otherwise.</returns>
        TStatus Update(TEntity entity, params string[] updateFields);

        /// <summary>
        /// Querying entity with provided where predicate.
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns>List of entities or null if none found</returns>
        List<TEntity> FindWhere(Func<TEntity, bool> wherePredicate);
    }
}
