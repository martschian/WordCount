using AutoMapper;
using WordGoal.Domain;

namespace WordGoal.API.Profiles
{
    public class LogEntryProfile : Profile
    {
        public LogEntryProfile()
        {
            CreateMap<LogEntry, Models.LogEntryDto>();
        }
    }
}
