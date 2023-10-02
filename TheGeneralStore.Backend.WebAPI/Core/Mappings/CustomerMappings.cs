using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Customers;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            // Mapping DataModel to Resource
            CreateMap<Customer, CustomerResource>();

            // Mapping Resource to DataModel
            CreateMap<CustomerCreateResource, Customer>();
        }
    }
}
