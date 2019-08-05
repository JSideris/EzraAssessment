using EzraAssessmentServer.Db;
using EzraAssessmentServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Services
{
    /// <summary>
    /// Generic service for CRUD operations. The reason for this abstraction is that it greatly simplifies the process of adding additional CRUD entities, minimizing repeated code.
    /// </summary>
    /// <typeparam name="TCreateDTO">Creation data transfer object.</typeparam>
    /// <typeparam name="TGetDTO">Get data transfer object.</typeparam>
    /// <typeparam name="TUpdateDTO">Update data transfer object.</typeparam>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public abstract class GenericService<TCreateDTO, TGetDTO, TUpdateDTO, TEntity> : IGenericService<TCreateDTO, TGetDTO, TUpdateDTO, TEntity>
        where TEntity : EntityBase
    {

        EzraAssessmentDbContext dbContext;

        public GenericService(EzraAssessmentDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="model">Model for the new entity.</param>
        /// <returns>A get DTO for the new entity.</returns>
        public async Task<TGetDTO> Create(TCreateDTO model)
        {
            // Map the DTO fields to the new entity.
            var entity = AutoMapper.Mapper.Map<TEntity>(model);

            // Add to the DB.
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            // Return the GET DTO.
            return AutoMapper.Mapper.Map<TGetDTO>(entity);
        }

        /// <summary>
        /// Hard-deletes an entity from the DB. This removes the row.
        /// </summary>
        /// <param name="id">ID of the entity to delete.</param>
        /// <returns>True, if the item was successfully deleted.</returns>
        public async Task<bool> HardDelete(int id)
        {
            // Get the entity.
            var entity = await GetEntityById(id);
            dbContext.Remove(entity);
            var changes = await dbContext.SaveChangesAsync();

            // Return true if changes were made to the DB.
            return changes > 0;
        }

        /// <summary>
        /// Soft-deletes an entity from the DB. Sets the IsDeleted flag to true.
        /// </summary>
        /// <param name="id">ID of the entity to delete.</param>
        /// <returns>True, if the item was successfully deleted.</returns>
        public async Task<bool> SoftDelete(int id)
        {
            // Get the entity.
            var entity = await GetEntityById(id);

            // "Delete" it.
            entity.IsDeleted = true;
            dbContext.Update(entity);
            var changes = await dbContext.SaveChangesAsync();

            // Return true if changes were made to the DB.
            return changes > 0;
        }

        /// <summary>
        /// Gets all the entities from the DB. Use for testing purposes only.
        /// </summary>
        /// <returns>A list of entities.</returns>
        public async Task<IEnumerable<TGetDTO>> GetAll()
        {
            // Get all the entities.
            var entities = await dbContext.Set<TEntity>().ToListAsync();

            // Map to DTOs and return.
            return AutoMapper.Mapper.Map<IEnumerable<TGetDTO>>(entities);
        }

        /// <summary>
        /// Returns a paginated list of items.
        /// </summary>
        /// <param name="quantityPerPage">The page size.</param>
        /// <param name="pageNumber">Page number, starting at 1.</param>
        /// <param name="searchQuery">Search query.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TGetDTO>> GetAll(int quantityPerPage, int pageNumber, string searchQuery)
        {
            // Get all the entities from this page.
            var entities = dbContext.Set<TEntity>().Skip(quantityPerPage * (pageNumber - 1)).Take(pageNumber);

            // Filter the list based on the provided search query.
            entities = FilterQuery(searchQuery, entities);

            // Map to DTOs and return.
            return AutoMapper.Mapper.Map<IEnumerable<TGetDTO>>(await entities.ToListAsync());
        }

        /// <summary>
        /// Gets a single entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity to get.</param>
        /// <returns></returns>
        public async Task<TGetDTO> GetById(int id)
        {
            // Get the entity.
            var entity = await GetEntityById(id);

            // Map to DTO and return.
            return AutoMapper.Mapper.Map<TGetDTO>(entity);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="model">Model for the entity.</param>
        /// <returns>A get DTO for this model.</returns>
        public async Task<TGetDTO> Update(int id, TUpdateDTO model)
        {
            // Get the entity.
            var entity = await GetEntityById(id);

            // Map the changes into the entity.
            AutoMapper.Mapper.Map(model, entity);

            // Save.
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            // Return the entity.
            return AutoMapper.Mapper.Map<TGetDTO>(entity);
        }

        private async Task<TEntity> GetEntityById(int id)
        {
            // Good practice would be to throw a custom exception if this item cannot be found, which can be caught in the controller.
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Filters the result set down using a search query. Something that can be fulfilled later using reflection, as needed.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="queryable">The queryable list to filter.</param>
        /// <returns></returns>
        private IQueryable<TEntity> FilterQuery(string searchQuery, IQueryable<TEntity> queryable){
            return queryable;
        }
    }
}
