using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.BusinessLogic.Configurations.Mapper.Explora
{
    public class ExploraMapperProfile : MapperProfileBase
    {
        public ExploraMapperProfile()
        {
            CreateMapperConfig();
        }

        /// <summary>
        /// Maps entities <---> dtos
        /// </summary>
        internal void CreateMapperConfig()
        {
            CreateMap<ExploraFile, FileDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ExploraCollection, CollectionDto>().ReverseMap();
            CreateMap<ExploraTotem, TotemDto>().ReverseMap();
        }
    }
}
