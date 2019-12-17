using System;
using System.Collections.Generic;
using System.Text;
using Argentex.Core.DataAccess.Entities;

namespace Argentex.Core.UnitsOfWork.Extensions
{
    public static class AppSettingExtensions
    {

        public static T ValueAs<T>(this AppSetting setting) where T : struct, IConvertible => (T)Convert.ChangeType(setting.SettingValue, typeof(T));
    }
}
