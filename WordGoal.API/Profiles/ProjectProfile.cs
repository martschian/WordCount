using AutoMapper;
using WordGoal.Domain;

namespace WordGoal.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, Models.ProjectDto>();
        }
    }
}
