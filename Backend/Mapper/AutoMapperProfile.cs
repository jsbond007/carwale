using AutoMapper;
using Carwale.Domain.Entities;
using Carwale.Objects;
using System.Reflection;

namespace Carwale.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadStandardMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {
            CreateMap<CarCreateRequest, Car>().ReverseMap();
            CreateMap<CarUpdateRequest, Car>().ReverseMap();
        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }
    }
}
