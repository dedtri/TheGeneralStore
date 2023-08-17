using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Customers;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            // API to Resource
            CreateMap<Customer, CustomerResource>();

            // Resource to API
            CreateMap<CustomerCreateResource, Customer>();
        }
    }
}
