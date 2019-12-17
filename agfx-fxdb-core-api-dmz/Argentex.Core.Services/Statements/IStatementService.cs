using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Argentex.Core.Service.Models.Statements;

namespace Argentex.Core.Service.Statements
{
    public interface IStatementService : IDisposable
    {
        IDictionary<string, List<StatementModel>> GetStatements(int clientCompanyId, DateTime startDate, DateTime endDate);
        bool CheckCompany(int clientCompanyId);
    }
}
