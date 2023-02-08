using AutoMapper;
using WordGoal.API.Models;
using WordGoal.Domain;

namespace WordGoal.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectForCreationDto, Project>();
        }
    }
}
