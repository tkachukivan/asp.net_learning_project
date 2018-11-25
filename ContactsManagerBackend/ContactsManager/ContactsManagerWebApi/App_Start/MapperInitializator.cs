using ContactsManagerBL;

namespace ContactsManagerWebApi.App_Start
{
    public static class MapperInitializator
    {
        public static void Initializate()
        {
            MappingDto.InitializeMapping();
            MappingModel.InitializeMapping();
        }
    }
}