using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.UnitsOfWork.AppSettings
{
    public interface IAppSettingUow : IBaseUow
    {
        AppSetting GetAppSetting(string key);
        AppSetting GetAppSetting(AppSettingEnum key);
    }
}
