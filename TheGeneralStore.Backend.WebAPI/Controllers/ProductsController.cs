using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly ProductRepository productRepository;

        public ProductsController(IMapper mapper, UnitOfWork unitOfWork, ProductRepository productRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<ProductResource>> Create([FromBody] ProductCreateResource createResource)
        {
            // Map entity
            var entity = this.mapper.Map<ProductCreateResource, Product>(createResource);

            // Add entity
            this.productRepository.Add(entity);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            // Map entity to resource
            var resource = this.mapper.Map<Product, ProductResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<ProductResource>> Get(int entityId)
        {
            // Get entity
            var entity = await this.productRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            // Map entity to resource
            var resource = this.mapper.Map<Product, ProductResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResultResource<ProductResource>>> GetAll()
        {
            // Create query
            var query = new ProductQuery()
            {
                PageSize = -1
            };

            // Get entities
            var queryResult = await this.productRepository.GetAllAsync(query, true);

            // Map entities to resource
            var resource = this.mapper.Map<BaseQueryResult<Product>, BaseQueryResultResource<ProductResource>>(queryResult);

            return resource;
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await productRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete Entity
            try
            {
                productRepository.Remove(entity);

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
