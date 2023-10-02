using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Customers;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly CustomerRepository customerRepository;

        public CustomersController(IMapper mapper, UnitOfWork unitOfWork, CustomerRepository customerRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
        }

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<CustomerResource>> Get(int entityId)
        {
            // Get entity
            var entity = await this.customerRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            // Map entity to resource
            var resource = this.mapper.Map<Customer, CustomerResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region GetAll
        [HttpGet, Authorize]
        public async Task<ActionResult<BaseQueryResultResource<CustomerResource>>> GetAll()
        {
            // Create query
            var query = new CustomerQuery()
            {
                PageSize = -1
            };

            // Get entities
            var queryResult = await this.customerRepository.GetAllAsync(query, true);

            // Map entities to resource
            var resource = this.mapper.Map<BaseQueryResult<Customer>, BaseQueryResultResource<CustomerResource>>(queryResult);

            return resource;
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await customerRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete Entity
            try
            {
                customerRepository.Remove(entity);

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
