namespace Argentex.Core.Service.Email.EmailSender
{
    public enum EmailType
    {
        NewUser,
        ResetPassword,
        PasswordChanged,

        TradeNote,
        BrokerTradeNote,
        FailedFIXTrades,

        OrderNote,
        DealerOrderNote,
        CancelOrder,
        SettlementAssigned,

        InwardPayment,
        OutwardPayment,

        UserChangeRequestAlert,
        MobileChangeEmailAlert
    }
}
