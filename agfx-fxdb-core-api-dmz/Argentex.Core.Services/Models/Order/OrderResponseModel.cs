namespace Argentex.Core.Service.Models.Order
{
    public class OrderResponseModel
    {
        public int OrderIndex { get; set; }
        public string Code { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
