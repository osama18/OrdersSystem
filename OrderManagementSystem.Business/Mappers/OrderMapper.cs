using AutoMapper;
using OrderManagementSystem.Business.Mappers;
using OrderManagementSystem.Common.Logging;

namespace OrderManagementSystem.Business.Mappers
{
    public class OrderMapper : ObjectMapper, IOrderMapper
    {
        public OrderMapper()
        {
            base.Configure();
        }
        protected override void CreateMappings(AutoMapper.IMapperConfiguration cfg)
        {
            //cfg.CreateMap<Student, StudentDto>();
            //cfg.CreateMap<StudentDto, Student> ();
        }

    }
}