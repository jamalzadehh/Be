using AutoMapper;
using Be.Models;
using Be.ViewModels.TeamVM;

namespace Be.AutoMappers
{
    public class TeamAutoMapper:Profile
    {
        public TeamAutoMapper()
        {
            CreateMap<Team,TeamCreateVM>().ReverseMap();
            CreateMap<Team,TeamUpdateVM>().ForMember(x=>x.ImageUrl,o=>o.Ignore()).ReverseMap();
        }
    }
}
