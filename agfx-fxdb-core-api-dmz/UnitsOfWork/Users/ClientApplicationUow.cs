using System.Threading.Tasks;
using Argentex.Core.Identity.DataAccess;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;

namespace Argentex.Core.UnitsOfWork.Users
{
    public class ClientApplicationUow : BaseUow, IClientApplicationUow
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        public ClientApplicationUow(SecurityDbContext context, OpenIddictApplicationManager<OpenIddictApplication> applicationManager) : base(context)
        {
            _applicationManager = applicationManager;
        }

        public async Task<OpenIddictApplication> GetClientCredentialsAsync(string clientId) => await _applicationManager.FindByClientIdAsync(clientId);
        
    }
}