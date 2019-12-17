using Argentex.Core.Service.Models.Payments;
using Argentex.Core.Service.Models.Settlements;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Settlements
{
    public interface ISettlementService : IDisposable
    {
        PaymentInformationModel GetPaymentInformation(string paymentCode, bool isPaymentOut);
        Task<IList<AssignSettlementModel>> AssignAsync(AssignSettlementRequestModel assignSettlementRequest);
        IList<AssignSettlementModel> GetAssignedSettlements(string tradeCode);
        void DeleteAssignedSettlements(long settlementId);
    }
}
