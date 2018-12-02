using AutoMapper;

namespace ContactsManagerWebApi.App_Start
{
    public static class MapperInitializator
    {
        public static void Initializate()
        {
            Mapper.Initialize(cfg => { });
        }
    }
}