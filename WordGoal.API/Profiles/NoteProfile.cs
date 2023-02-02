using AutoMapper;
using WordGoal.API.Models;
using WordGoal.Domain;

namespace WordGoal.API.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, Models.NoteDto>();

            CreateMap<NoteForCreationDto, Note>();
        }
    }
}
