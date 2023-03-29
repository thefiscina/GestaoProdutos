using AutoMapper;

namespace GestaoProduto.Service.Map
{
    public static class AutoMapping
    {
        public static IMapper mapper;
        public static void Initializer()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();                
                cfg.AddProfile<ProductProfile>();
            });

            mapper = configuration.CreateMapper();
        }
    }
}
