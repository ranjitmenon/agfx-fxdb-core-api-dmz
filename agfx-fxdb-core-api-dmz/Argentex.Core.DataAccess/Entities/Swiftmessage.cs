using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Swiftmessage
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string SenderReference { get; set; }
        public string FileName { get; set; }
        public string LaufileName { get; set; }
        public string Xmlfile { get; set; }
        public string Laufile { get; set; }
        public string NakErrorCode { get; set; }
        public string ErrorFile { get; set; }
        public string HitErrorCode { get; set; }
        public string GenerationErrorMessages { get; set; }
        public DateTime? NakUpdatedDateTime { get; set; }
        public DateTime? HitUpdatedDateTime { get; set; }

        public Payment Payment { get; set; }
    }
}
