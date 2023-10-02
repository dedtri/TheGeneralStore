using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders;

namespace TheGeneralStore.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly OrderRepository orderRepository;

        public OrdersController(IMapper mapper, UnitOfWork unitOfWork, OrderRepository orderRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.orderRepository = orderRepository;
        }

        #region Create
        [HttpPost()]
        public async Task<ActionResult<OrderResource>> Create([FromBody] OrderCreateResource createResource)
        {
            // Map entity
            var entity = this.mapper.Map<OrderCreateResource, Order>(createResource);

            // Add entity
            this.orderRepository.Add(entity);

            // Save changes
            await this.unitOfWork.SaveChangesAsync();

            // Map entity to resource
            var resource = this.mapper.Map<Order, OrderResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region Get
        [HttpGet("{entityId}")]
        public async Task<ActionResult<OrderResource>> Get(int entityId)
        {
            // Get entity
            var entity = await this.orderRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            // Map entity to resource
            var resource = this.mapper.Map<Order, OrderResource>(entity);

            return Ok(resource);
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<BaseQueryResultResource<OrderResource>>> GetAll([FromQuery] OrderQueryResource filter)
        {
            if (filter.PageSize == 0)
                filter.PageSize = -1;

            // Create query
            var query = new OrderQuery
            {
                CustomerId = filter.CustomerId
            };

            // Get entities
            var queryResult = await this.orderRepository.GetAllAsync(query, true);

            // Map entities to resource
            var resource = this.mapper.Map<BaseQueryResult<Order>, BaseQueryResultResource<OrderResource>>(queryResult);

            return resource;
        }
        #endregion

        #region Delete
        [HttpDelete("delete/{entityId}")]
        public async Task<ActionResult> Delete(int entityId)
        {
            // Get entitty
            var entity = await orderRepository.GetAsync(entityId);
            if (entity == null)
                return NotFound();

            //Delete Entity
            try
            {
                orderRepository.Remove(entity);

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
