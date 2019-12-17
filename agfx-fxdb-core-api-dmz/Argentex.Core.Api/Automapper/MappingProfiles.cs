using System.Linq;
using Argentex.Core.Api.ClientAuthentication;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.Models.Identity;
using AutoMapper;
using OpenIddict.Abstractions;

namespace Argentex.Core.Api.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterServiceModel, ApplicationServiceUser>();
            CreateMap<ApplicationServiceUser, ApplicationUser>();
            CreateMap<Models.SecurityModels.LoginModel, LoginServiceModel>();
            CreateMap<ClientConfig, OpenIddictApplicationDescriptor>()
                .ForMember(d => d.ClientSecret, o => o.MapFrom(s => s.Secret))
                .ForMember(d => d.Permissions, o => o.Ignore());
        }
    }
}
