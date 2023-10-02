using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            // Mapping DataModel to Resource
            CreateMap<Order, OrderResource>();
            CreateMap<OrderProduct, OrderProductResource>();

            // Mapping Resource to DataModel
            CreateMap<OrderCreateResource, Order>();
            CreateMap<OrderProductCreateResource, OrderProduct>();
        }
    }
}
