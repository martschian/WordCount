using AutoMapper;
using WordGoal.API.Models;
using WordGoal.Domain;

namespace WordGoal.API.Profiles
{
    public class LogEntryProfile : Profile
    {
        public LogEntryProfile()
        {
            CreateMap<LogEntry, LogEntryDto>();
            CreateMap<LogEntryForCreationDto, LogEntry>();
        }
    }
}
