using AutoMapper;
using ContactsManagerDAL;

namespace ContactsManagerBL
{
    public static class MappingDto
    {
        public static IMapper Mapper;

        public static void InitializeMapping()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ContactDto, ContactModel>());

            Mapper = config.CreateMapper();
        }
    }
}
