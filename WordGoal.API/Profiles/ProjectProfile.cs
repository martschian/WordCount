using AutoMapper;

namespace WordGoal.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Entities.Project, Models.ProjectDto>();
        }
    }
}
