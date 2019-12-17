using Argentex.Core.Service.Enums;
using System.Text;

namespace Argentex.Core.Service.Helpers
{
    public static class CodeBuilder
    {
        public static string FormatTradeCode(int tradeCount, int clientCompanyId)
        {
            StringBuilder tradeCodeBuilder = new StringBuilder();

            if (clientCompanyId < 10000)
            {
                tradeCodeBuilder.Append("0000" + clientCompanyId);
                tradeCodeBuilder.Remove(0, tradeCodeBuilder.Length - 4);
                tradeCodeBuilder.Insert(0, "AG");
            }
            else
            {
                tradeCodeBuilder.Append("AG" + clientCompanyId);
            }

            if (tradeCount < 10000)
            {
                var tradeCountStr = "0000" + tradeCount;
                var subString = tradeCountStr.Remove(0, tradeCountStr.Length - 4);
                tradeCodeBuilder.Append("-" + subString);
            }
            else
            {
                tradeCodeBuilder.Append("-" + tradeCount);
            }

            return tradeCodeBuilder.ToString();
        }

        public static string FormatSwapTradeCode(string tradeCode, int swapCount, SwapType swapType)
        {
            var sufix = swapType == SwapType.DeliveryLeg ? "DL" : "RL";
            var swapTradeCode = $"{tradeCode}/{sufix}{swapCount}";

            return swapTradeCode;
        }

        public static string FormatSwapTradeCode(int tradeCount, int swapCount, int clientCompanyId, SwapType swapType)
        {
            var tradeCode = FormatTradeCode(tradeCount, clientCompanyId);
            var swapTradeCode = FormatSwapTradeCode(tradeCode, swapCount, swapType);

            return swapTradeCode;
        }
    }
}
