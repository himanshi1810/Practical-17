using AutoMapper;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Models.Student, ViewModels.StudentViewModel>().ReverseMap();
            CreateMap<Models.Student, ViewModels.StudentDetailsViewModel>().ReverseMap();
            CreateMap<Models.Student, ViewModels.CreateStudentViewModel>().ReverseMap();
            CreateMap<EditStudentViewModel, Student>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
