using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Categories;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly CategoryRepository categoryRepository;
        private readonly ProductRepository productRepository;

        public CategoriesController(IMapper mapper, UnitOfWork unitOfWork, CategoryRepository categoryRepository, ProductRepository productRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<CategoryResource>> Create([FromBody] CategoryCreateResource createResource)
        {
            // Map entity
            var entity = this.mapper.Map<CategoryCreateResource, Category>(createResource);

            // Add entity
            this.categoryRepository.Add(entity);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            // Map entity to resource
            var resource = this.mapper.Map<Category, CategoryResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<CategoryResource>> Get(int entityId)
        {
            // Get entity
            var entity = await this.categoryRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            // Map entity to resource
            var resource = this.mapper.Map<Category, CategoryResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region GetByName
        [HttpGet("name/{entityName}")]
        public async Task<ActionResult<CategoryResource>> Get(string entityName)
        {
            // Get entity
            var entity = await this.categoryRepository.GetByNameAsync(entityName, true);
            if (entity == null)
                return NotFound();

            // Map entity to resource
            var resource = this.mapper.Map<Category, CategoryResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResultResource<CategoryResource>>> GetAll()
        {
            // Create query
            var query = new CategoryQuery()
            {
                PageSize = -1
            };

            // Get entities
            var queryResult = await this.categoryRepository.GetAllAsync(query, true);

            // Map entities to resource
            var resource = this.mapper.Map<BaseQueryResult<Category>, BaseQueryResultResource<CategoryResource>>(queryResult);

            return resource;
        }
        #endregion

        #region Update
        [HttpPut("{entityId}")]
        public async Task<ActionResult> Update(int entityId, [FromBody] CategoryUpdateResource updateResource)
        {
            var entity = await this.categoryRepository.GetAsync(entityId, true);
            if (entity == null)
                return NotFound();

            if (updateResource.IsDeleted == true)
            {
                foreach (var product in entity.Products)
                {
                    var entityProduct = await this.productRepository.GetAsync(product.Id);
                    entityProduct.IsDeleted = true;
                    this.productRepository.Update(entityProduct);
                }
            }

            this.mapper.Map<CategoryUpdateResource, Category>(updateResource, entity);

            await this.unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await categoryRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete Entity
            try
            {
                categoryRepository.Remove(entity);

                await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
        #endregion
    }
}
