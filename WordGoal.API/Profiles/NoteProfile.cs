using AutoMapper;

namespace WordGoal.API.Profiles
{
    public class NoteProfile:Profile
    {
        public NoteProfile()
        {
            CreateMap<Entities.Note, Models.NoteDto>();
        }
    }
}
