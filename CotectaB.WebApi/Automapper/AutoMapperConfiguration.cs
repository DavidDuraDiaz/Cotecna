using AutoMapper;

namespace CotecnaB.Services.Automapper
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration customMapConfig;
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();
        }
    }
}
