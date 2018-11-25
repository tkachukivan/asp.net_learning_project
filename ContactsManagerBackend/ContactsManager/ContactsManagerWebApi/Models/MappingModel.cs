using AutoMapper;
using ContactsManagerDAL;

namespace ContactsManagerBL
{
    public static class MappingModel
    {
        public static IMapper Mapper;

        public static void InitializeMapping()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Contact, ContactModel>());

            Mapper = config.CreateMapper();
        }
    }
}
