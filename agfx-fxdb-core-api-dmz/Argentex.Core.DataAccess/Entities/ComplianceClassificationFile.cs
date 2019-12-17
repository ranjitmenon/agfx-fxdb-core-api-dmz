using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceClassificationFile
    {
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int ComplianceClassificationId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string DocumentId { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public int UploadedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }

        public ClientCompanyCompliance ClientCompanyCompliance { get; set; }
        public ComplianceClassification ComplianceClassification { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public AuthUser UploadedByAuthUser { get; set; }
    }
}
