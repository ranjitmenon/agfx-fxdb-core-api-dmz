namespace Argentex.Core.Service.Helpers
{
    public class SystemConstant
    {
        public const string ClientSiteAction_Type_OPIPayment = "RequestOPIAssignedToTrades";
        public const string ClientSiteAction_Type_SwapCreation = "RequestSwap";
        public const string ClientSiteAction_Type_NoFIXConfirmation = "RequestTradesNoFIXConfirmation";
        public const string ClientSiteAction_Type_NewOPI = "RequestNewOPI";

        public const string ClientSiteAction_Status_New = "New";
        public const string ClientSiteAction_Status_Requested = "Requested";
        public const string ClientSiteAction_Status_Pending = "Pending";

        public const string Setting_UserChangeDaysRequiresForApproval = "UserChangeDaysRequiresForApproval";

        public const int Setting_UserChangeDaysRequiresForApproval_Default = 10;

    }
}
