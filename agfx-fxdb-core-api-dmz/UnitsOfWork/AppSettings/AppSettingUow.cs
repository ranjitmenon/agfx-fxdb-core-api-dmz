using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Argentex.Core.UnitsOfWork.AppSettings
{
    public class AppSettingUow : BaseUow, IAppSettingUow
    {
        private IGenericRepo<AppSetting> _appSettingRepo;
        private readonly IList<AppSetting> _appSettings;

        public AppSettingUow(FXDB1Context context)
            : base(context)
        {
            _appSettings = AppSettingRepo.GetQueryable().AsNoTracking().ToList();
        }

        private IGenericRepo<AppSetting> AppSettingRepo =>
            _appSettingRepo = _appSettingRepo ?? new GenericRepo<AppSetting>(Context);

        public AppSetting GetAppSetting(string key)
        {
            return _appSettings
                .FirstOrDefault(x => x.SettingKey == key);
        }

        public AppSetting GetAppSetting(AppSettingEnum key) => GetAppSetting(Enum.GetName(typeof(AppSettingEnum), key));

    }
}
