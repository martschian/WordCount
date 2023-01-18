using AutoMapper;

namespace WordGoal.API.Profiles
{
    public class LogEntryProfile : Profile
    {
        public LogEntryProfile()
        {
            CreateMap<Entities.LogEntry, Models.LogEntryDto>();
        }
    }
}
