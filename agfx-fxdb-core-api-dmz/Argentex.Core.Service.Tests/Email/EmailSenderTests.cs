using Argentex.Core.Service.Email.EmailSender;
using EQService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Argentex.Core.Service.Tests.Email
{
    public class EmailSenderTests
    {
        [Fact]
        public void CreateBody_NewUser()
        {
            var eqsService = new Mock<IServiceEmail>();

            var service = new EmailSender(eqsService.Object, null);

            var result = service.CreateBody(EmailType.NewUser);

            Assert.Contains("Create Password", result);
        }

        [Fact]
        public void CreateBode_ResetPassword()
        {
            var eqsService = new Mock<IServiceEmail>();

            var service = new EmailSender(eqsService.Object, null);

            var result = service.CreateBody(EmailType.ResetPassword);

            Assert.Contains("Forgot Password", result);
        }
    }
}
