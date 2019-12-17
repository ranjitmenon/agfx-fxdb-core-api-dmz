using System;
using System.Collections.Generic;
using Argentex.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System.Net;
using System.Threading.Tasks;
using Argentex.Core.Api.Controllers.Client;
using Argentex.Core.Api.Models;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.ClientCompany;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Http;
using Argentex.Core.Api.Controllers;

namespace Argentex.Core.Api.Tests.ClientCompanyContact
{
    public class ClientCompanyControllerTests
    {
        [Fact]
        public void Add_Contact_Category_Success()
        {
            // Given
            var model = new ContactCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = true;
            serviceMock.Setup(x => x.AddContactCategory(It.IsAny<ContactCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedType = typeof(ResponseModel);

            
            // When
            var response = controller.AddContactCategory(model);
            var result = response as OkObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Info");
        }
        
        [Fact]
        public void Add_Contact_Category_Failed()
        {
            // Given
            var model = new ContactCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = false;
            serviceMock.Setup(x => x.AddContactCategory(It.IsAny<ContactCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.AddContactCategory(model);
            var result = response as BadRequestObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
        }

        [Fact]
        public void Add_Contact_Category_Failed_ModelState_Invalid()
        {
            // Given
            var model = new ContactCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = true;
            serviceMock.Setup(x => x.AddContactCategory(It.IsAny<ContactCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);
            
            controller.ModelState.AddModelError("Error", "An error has occurred");

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.AddContactCategory(model);
            var result = response as BadRequestObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            var responseModel = (ResponseModel)result.Value;
            responseModel.ResponseMessages.Should().NotBeEmpty();
            responseModel.ResponseMessages.Should().ContainKey("Errors");
            responseModel.ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Contact_Categories_Success()
        {
            // Given
            
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            const string contactCategoryDescription1 = "EUR/USD";
            const string contactCategoryDescription2 = "EUR/GBP";
            const string contactCategoryDescription3 = "GBP/USD";
            const string contactCategoryDescription4 = "GBP/EUR";

            const int contactCategoryId1 = 1;
            const int contactCategoryId2 = 2;
            const int contactCategoryId3 = 3;
            const int contactCategoryId4 = 4;

            const int contactCategorySequence1 = 1;
            const int contactCategorySequence2 = 2;
            const int contactCategorySequence3 = 3;
            const int contactCategorySequence4 = 4;

            IEnumerable<ContactCategoryModel> contactCategoryModels = new List<ContactCategoryModel>()
            {
                new ContactCategoryModel()
                {
                    Id = contactCategoryId1,
                    Description = contactCategoryDescription1,
                    Sequence = contactCategorySequence1
                },
                new ContactCategoryModel()
                {
                    Id = contactCategoryId2,
                    Description = contactCategoryDescription2,
                    Sequence = contactCategorySequence2
                },
                new ContactCategoryModel()
                {
                    Id = contactCategoryId3,
                    Description = contactCategoryDescription3,
                    Sequence = contactCategorySequence3
                },
                new ContactCategoryModel()
                {
                    Id = contactCategoryId4,
                    Description = contactCategoryDescription4,
                    Sequence = contactCategorySequence4
                }
            };

            serviceMock.Setup(x => x.GetContactCategories()).Returns(Task.FromResult(contactCategoryModels));

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedResultValueType = typeof(List<ContactCategoryModel>);

            // When
            var response = controller.GetContactCategories();
            var result = response.Result as OkObjectResult;

            
            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedResultValueType);
            result.Value.Should().BeEquivalentTo(contactCategoryModels);
        }

        [Fact]
        public void Get_Contact_Categories_Failed()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();


            serviceMock.Setup(x => x.GetContactCategories()).Throws(new Exception("An error has occurred"));

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.GetContactCategories();
            var result = response.Result as BadRequestObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Client_Company_Contact_Categories_Success()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int clientCompanyContactId = 44;

            const string contactCategoryDescription1 = "EUR/USD";
            const string contactCategoryDescription2 = "EUR/GBP";
            const string contactCategoryDescription3 = "GBP/USD";
            const string contactCategoryDescription4 = "GBP/EUR";

            const int contactCategoryId1 = 1;
            const int contactCategoryId2 = 2;
            const int contactCategoryId3 = 3;
            const int contactCategoryId4 = 4;
            
            IEnumerable<ClientCompanyContactCategoryModel> contactCategoryModels = new List<ClientCompanyContactCategoryModel>()
            {
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryDescription = contactCategoryDescription1,
                    ContactCategoryId = contactCategoryId1
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryDescription = contactCategoryDescription2,
                    ContactCategoryId = contactCategoryId2
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryDescription = contactCategoryDescription3,
                    ContactCategoryId = contactCategoryId3
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryDescription = contactCategoryDescription4,
                    ContactCategoryId = contactCategoryId4
                },
            };
    

            serviceMock.Setup(x => x.GetClientCompanyContactCategories(It.IsAny<int>())).Returns(Task.FromResult(contactCategoryModels));

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedResultValueType = typeof(List<ClientCompanyContactCategoryModel>);

            // When
            var response = controller.GetClientCompanyContactCategories(clientCompanyContactId);
            var result = response.Result as OkObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedResultValueType);
            result.Value.Should().BeEquivalentTo(contactCategoryModels);
        }

        [Fact]
        public void Get_Client_Company_Contact_Categories_Failed()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int clientCompanyContactId = 44;
            
            serviceMock.Setup(x => x.GetClientCompanyContactCategories(It.IsAny<int>())).Throws(new Exception("An error has occurred"));
            
            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.GetClientCompanyContactCategories(clientCompanyContactId);
            var result = response.Result as BadRequestObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }


        [Fact]
        public void Get_Contact_Category_By_Id_Success()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int contactCategoryId = 44;
            string contactCategoryDescription = "EUR/USD";
            var contactCategorySequence = 1;

            ContactCategory contactCategory = new ContactCategory()
            {
                Id = contactCategoryId,
                Description = contactCategoryDescription,
                Sequence = contactCategorySequence
            };

            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<int>())).Returns(contactCategory);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedResultValueType = typeof(ContactCategory);

            // When
            var response = controller.GetContactCategory(contactCategoryId);
            var result = response as OkObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedResultValueType);
            result.Value.Should().BeEquivalentTo(contactCategory);
        }

        [Fact]
        public void Get_Contact_Category_By_Id_Failed()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int contactCategoryId = 44;

            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<int>())).Throws(new Exception("An error has occurred"));

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.GetContactCategory(contactCategoryId);
            var result = response as BadRequestObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Contact_Category_By_Id_Not_Found()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int contactCategoryId = 44;

            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<int>())).Returns((ContactCategory) null);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);

            // When
            var response = controller.GetContactCategory(contactCategoryId);
            var result = response as OkObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeNull();
        }

        [Fact]
        public void Get_Contact_Category_By_Description_Success()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            int contactCategoryId = 44;
            string contactCategoryDescription = "EUR/USD";
            var contactCategorySequence = 1;

            ContactCategory contactCategory = new ContactCategory()
            {
                Id = contactCategoryId,
                Description = contactCategoryDescription,
                Sequence = contactCategorySequence
            };

            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<string>())).Returns(contactCategory);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedResultValueType = typeof(ContactCategory);

            // When
            var response = controller.GetContactCategory(contactCategoryDescription);
            var result = response as OkObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedResultValueType);
            result.Value.Should().BeEquivalentTo(contactCategory);
        }

        [Fact]
        public void Get_Contact_Category_By_Description_Failed()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            string contactCategoryDescription = "EUR/USD";

            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<string>())).Throws(new Exception("An error has occurred"));

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.GetContactCategory(contactCategoryDescription);
            var result = response as BadRequestObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Contact_Category_By_Description_NotFound()
        {
            // Given

            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            string contactCategoryDescription = "EUR/USD";
            
            serviceMock.Setup(x => x.GetContactCategory(It.IsAny<string>())).Returns((ContactCategory)null);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);

            // When
            var response = controller.GetContactCategory(contactCategoryDescription);
            var result = response as OkObjectResult;


            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeNull();
        }

        [Fact]
        public void Process_Client_Company_Contact_Categories_Success()
        {
            // Given
            var model = new ClientCompanyContactBulkCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = true;
            serviceMock.Setup(x => x.ProcessClientCompanyContactCategories(It.IsAny<ClientCompanyContactBulkCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedType = typeof(ResponseModel);


            // When
            var response = controller.ProcessClientCompanyContactCategories(model);
            var result = response as OkObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Info");
        }

        [Fact]
        public void Process_Client_Company_Contact_Categories_Failed()
        {
            // Given
            var model = new ClientCompanyContactBulkCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = false;
            serviceMock.Setup(x => x.ProcessClientCompanyContactCategories(It.IsAny<ClientCompanyContactBulkCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.ProcessClientCompanyContactCategories(model);
            var result = response as BadRequestObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
        }

        [Fact]
        public void Process_Client_Company_Contact_Categories_Failed_ModelState_Invalid()
        {
            // Given
            var model = new ClientCompanyContactBulkCategoryModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            var returnValue = true;
            serviceMock.Setup(x => x.ProcessClientCompanyContactCategories(It.IsAny<ClientCompanyContactBulkCategoryModel>())).Returns(returnValue);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            var errorMessage = "An error has occurred";
            controller.ModelState.AddModelError("Error", errorMessage);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            // When
            var response = controller.ProcessClientCompanyContactCategories(model);
            var result = response as BadRequestObjectResult;

            // Then
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void GetClientCompanyContact_Success_With_Valid_ClientCompanyContactId_Input()
        {
            //Arrange
            var responseModel = new ClientCompanyContactResponseModel()
            {
                CompanyContactModel = new ClientCompanyContactModel()
                {
                    ID = 1,
                    ContactForename = "Test",
                    ContactSurname = "Tester"
                }
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 1,
                AuthUsertId = 0
            };
            var serviceMock = new Mock<IClientCompanyService>();

            serviceMock.Setup(x => x.GetClientCompanyContact(It.IsAny<ClientCompanyContactSearchContext>()))
                .Returns(responseModel);

            var controller = new ClientCompanyContactController(serviceMock.Object, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var response = controller.GetClientCompanyContact(clientCompanyContactSearchContext);
            var expectedResultType = typeof(OkObjectResult);
            var result = response as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>().And.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);

            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.Should().NotBeNull();
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ID.Should().Be(1);
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ContactForename.Should().Be("Test");
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ContactSurname.Should().Be("Tester");
        }

        [Fact]
        public void GetClientCompanyContact_Success_With_Valid_AuthUsertId_Input()
        {
            //Arrange
            var responseModel = new ClientCompanyContactResponseModel()
            {
                CompanyContactModel = new ClientCompanyContactModel()
                {
                    ID = 1,
                    ContactForename = "Test",
                    ContactSurname = "Tester"
                }
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 0,
                AuthUsertId = 1
            };
            var serviceMock = new Mock<IClientCompanyService>();

            serviceMock.Setup(x => x.GetClientCompanyContact(It.IsAny<ClientCompanyContactSearchContext>()))
                .Returns(responseModel);

            var controller = new ClientCompanyContactController(serviceMock.Object, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var response = controller.GetClientCompanyContact(clientCompanyContactSearchContext);
            var expectedResultType = typeof(OkObjectResult);
            var result = response as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>().And.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);

            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.Should().NotBeNull();
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ID.Should().Be(1);
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ContactForename.Should().Be("Test");
            ((ClientCompanyContactResponseModel)result.Value).CompanyContactModel.ContactSurname.Should().Be("Tester");
        }

        [Fact]
        public void GetClientCompanyContact_Not_Found()
        {
            //Arrange
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();
            var errorMessage = new ClientCompanyContactResponseModel
            {
                ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] {$"Client Company Contact with ID 1 could not be retrieved."}
                        }
                    }
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 0,
                AuthUsertId = 0
            };

            serviceMock.Setup(x => x.GetClientCompanyContact(It.IsAny<ClientCompanyContactSearchContext>()))
                .Returns(new ClientCompanyContactResponseModel());

            serviceMock.Setup(x => x.GetErrorMessages(It.IsAny<HttpStatusCode>(), It.IsAny<Exception>(), It.IsAny<ClientCompanyContactSearchContext>()))
                .Returns(errorMessage);

            var controller = new ClientCompanyContactController(serviceMock.Object, loggerMock.Object);

            //Act
            var response = controller.GetClientCompanyContact(clientCompanyContactSearchContext);
            var expectedResultType = typeof(NotFoundObjectResult);
            var result = response as NotFoundObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);

            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void GetClientCompanyContact_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();
            var controller = new ClientCompanyContactController(service.Object, loggerMock.Object);
            var errorMessage = new ClientCompanyContactResponseModel
            {
                ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] {$"Client Company Contact with ID 1 could not be retrieved."}
                        }
                    }
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 0,
                AuthUsertId = 0
            };

            service.Setup(x => x.GetClientCompanyContact(It.IsAny<ClientCompanyContactSearchContext>()))
                .Throws(new Exception("An error has occurred"));

            service.Setup(x => x.GetErrorMessages(It.IsAny<HttpStatusCode>(), It.IsAny<Exception>(), It.IsAny<ClientCompanyContactSearchContext>()))
                .Returns(errorMessage);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ClientCompanyContactResponseModel);

            //Act
            var response = controller.GetClientCompanyContact(clientCompanyContactSearchContext);
            var result = response as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ClientCompanyContactResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void GetCompanyContactList_Success_With_Valid_ClientCompanyContactId_Input()
        {
            //Arrange
            var responseModel = new ClientCompanyContactListResponseModel()
            {
                CompanyContactListModel = new List<ClientCompanyContactList>()
                {
                    new ClientCompanyContactList()
                    {
                    ID = 1,
                    ContactForename = "Test",
                    ContactSurname = "Tester"
                    }
                },
                Succeeded = true
            };
            var applicationServiceUserList = new ClientCompanyContactListResponseModel();

            var serviceMock = new Mock<IClientCompanyService>();

            serviceMock.Setup(x => x.GetCompanyContactList(It.IsAny<int>()))
                .Returns(responseModel);

            serviceMock.Setup(x => x.GetCompanyContactList(It.IsAny<int>()))
                .Returns(responseModel);

            var controller = new ClientCompanyController(serviceMock.Object, null);

            //Act
            var response = controller.GetCompanyContactList(1);
            var expectedResultType = typeof(OkObjectResult);
            var result = response as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);

            ((ClientCompanyContactListResponseModel)result.Value).CompanyContactListModel.Should().NotBeNull();
            ((ClientCompanyContactListResponseModel)result.Value).CompanyContactListModel.Count.Should().Be(1);
            ((ClientCompanyContactListResponseModel)result.Value).Succeeded.Should().Be(true);
        }

        [Fact]
        public void GetCompanyContactList_Returns_Not_Found()
        {
            //Arrange
            var applicationServiceUserList = new ClientCompanyContactListResponseModel();
            var serviceMock = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();

            serviceMock.Setup(x => x.GetCompanyContactList(It.IsAny<int>()))
                .Returns(applicationServiceUserList);

            var controller = new ClientCompanyController(serviceMock.Object, loggerMock.Object);

            //Act
            var result = controller.GetCompanyContactList(1);
            var expectedResultType = typeof(OkObjectResult);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
        }

        [Fact]
        public void GetCompanyContactList_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IClientCompanyService>();
            var loggerMock = new Mock<ILogWrapper>();
            var controller = new ClientCompanyController(service.Object, loggerMock.Object);
            var errorMessage = new ClientCompanyContactListResponseModel
            {
                ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] {$"Client Company Contact with ID 1 could not be retrieved."}
                        }
                    }
            };
            
            service.Setup(x => x.GetCompanyContactList(It.IsAny<int>()))
                .Throws(new Exception("An error has occurred"));

            service.Setup(x => x.GetErrorMessagesForContactList(It.IsAny<HttpStatusCode>(), It.IsAny<Exception>(), It.IsAny<int>()))
                .Returns(errorMessage);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ClientCompanyContactListResponseModel);

            //Act
            var response = controller.GetCompanyContactList(1);
            var result = response as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ClientCompanyContactListResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ClientCompanyContactListResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
            ((ClientCompanyContactListResponseModel)result.Value).ResponseMessages.Should().HaveCount(1);
        }
    }
}
