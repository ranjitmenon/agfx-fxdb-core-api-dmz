using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.ClientCompanies;
using Argentex.Core.Service.ClientSiteAction;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts.Model;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Synetec.Data.UnitOfWork.GenericRepo;
using Xunit;


namespace Argentex.Core.Service.Tests.ClientCompanies
{
    public class ClientCompanyServiceTests
    {
        [Fact]
        public void Given_No_Company_Is_Found_For_An_Id_An_Exception_Should_Be_Thrown_When_Getting_The_Accounts()
        {
            // Given
            var clientCompanyId = 42;
            var clientCompanyReposiotyMock = new Mock<IGenericRepo<ClientCompany>>();
            var clientCompanyUowMock = new Mock<IClientCompanyAccountsUoW>();

            var clientSiteActionServiceMock = new Mock<IClientSiteActionService>();

            clientCompanyReposiotyMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns((ClientCompany) null);
            clientCompanyUowMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyReposiotyMock.Object);

            var service = new ClientCompanyAccountsService(clientCompanyUowMock.Object, clientSiteActionServiceMock.Object, null);

            var expectedMessage = $"Client company with id {clientCompanyId} does not exist";

            // When
            var exception =
                Assert.Throws<ClientCompanyNotFoundException>(() => service.GetClientCompanyAccounts(clientCompanyId));

            // Then
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Given_There_Are_Not_Accounts_Found_An_Empty_List_Should_Be_Returned()
        {
            // Given
            var clientCompany = new ClientCompany
            {
                Id = 42
            };
            var clientCompanyReposiotyMock = new Mock<IGenericRepo<ClientCompany>>();
            var clientCompanyOpiRepositoryMock = new Mock<IGenericRepo<ClientCompanyOpi>>();
            var clientCompanyUowMock = new Mock<IClientCompanyAccountsUoW>();
            
            var clientSiteActionServiceMock = new Mock<IClientSiteActionService>();

            clientCompanyReposiotyMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns(clientCompany);
            clientCompanyOpiRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<ClientCompanyOpi, bool>>>(),
                        It.IsAny<Func<IQueryable<ClientCompanyOpi>,
                            IOrderedQueryable<ClientCompanyOpi>>>(), ""))
                .Returns(new List<ClientCompanyOpi>());
            clientCompanyUowMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyReposiotyMock.Object);
            clientCompanyUowMock.Setup(x => x.ClientCompanyOpiRepository).Returns(clientCompanyOpiRepositoryMock.Object);

            var service = new ClientCompanyAccountsService(clientCompanyUowMock.Object, clientSiteActionServiceMock.Object, null);

            var expectedType = typeof(List<ClientCompanyAccountModel>);

            // When
            var result = service.GetClientCompanyAccounts(clientCompany.Id);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedType, result.GetType());
            Assert.False(result.Any());
        }

        [Fact]
        public void Given_There_Are_Accounts_Found_A_List_With_Of_Mapped_Accounts_Should_Be_Returned()
        {
            // Given
            var clientCompany = new ClientCompany
            {
                Id = 42
            };
            var currency = new Currency
            {
                Id = 24,
                Code = "GBP"
            };
            var clientCompanyAccounts = new List<ClientCompanyOpi>
            {
                new ClientCompanyOpi
                {
                    ClientCompanyId = clientCompany.Id,
                    AccountName = "Bank Account",
                    AccountNumber = "123456",
                    Currency = currency,
                    CurrencyId = currency.Id
                }
            };
            var clientCompanyReposiotyMock = new Mock<IGenericRepo<ClientCompany>>();
            var clientCompanyOpiRepositoryMock = new Mock<IGenericRepo<ClientCompanyOpi>>();
            var currencyRepositoryMock = new Mock<IGenericRepo<Currency>>();
            var clientCompanyUowMock = new Mock<IClientCompanyAccountsUoW>();
            
            var clientSiteActionServiceMock = new Mock<IClientSiteActionService>();

            clientCompanyReposiotyMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns(clientCompany);
            clientCompanyOpiRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<ClientCompanyOpi, bool>>>(),
                        It.IsAny<Func<IQueryable<ClientCompanyOpi>,
                            IOrderedQueryable<ClientCompanyOpi>>>(), ""))
                .Returns(clientCompanyAccounts);
            currencyRepositoryMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns(currency);

            clientCompanyUowMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyReposiotyMock.Object);
            clientCompanyUowMock.Setup(x => x.ClientCompanyOpiRepository).Returns(clientCompanyOpiRepositoryMock.Object);
            clientCompanyUowMock.Setup(x => x.CurrencyRepository).Returns(currencyRepositoryMock.Object);

            var service = new ClientCompanyAccountsService(clientCompanyUowMock.Object, clientSiteActionServiceMock.Object, null);
            
            var expectedId = 42;
            var expectedAccountName = "Bank Account";
            var expectedAccountNumber = "123456";
            var expectedCurrencyCode = "GBP";

            // When
            var result = service.GetClientCompanyAccounts(clientCompany.Id);

            // Then
            Assert.NotNull(result);
            Assert.True(result.Any());
            var firstAccount = result.First();
            Assert.Equal(expectedId, firstAccount.ClientCompanyId);
            Assert.Equal(expectedAccountName, firstAccount.AccountName);
            Assert.Equal(expectedAccountNumber, firstAccount.AccountNumber);
            Assert.Equal(expectedCurrencyCode, firstAccount.Currency);
        }

        [Fact]
        public void GetNumberOfAssociatedTrades_Should_Return_3_If_AssociatedTradesCount_Equals_3()
        {
            //Arrange
            int clientCompanyOpiId = 44;
            int associatedTradesCount = 3;

            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var clientCompanyAccountsUowMock = new Mock<IClientCompanyAccountsUoW>();

            clientCompanyAccountsUowMock.Setup(x => x.GetAssociatedTradesCount(It.IsAny<int>(), It.IsAny<int>())).Returns(associatedTradesCount);

            var service = new ClientCompanyAccountsService(clientCompanyAccountsUowMock.Object, null, null);

            //Act
            var expectedResult = 3;
            var result = service.GetNumberOfAssociatedTrades(clientCompanyOpiId);

            //Assert
            result.Should().NotBe(0).And.Be(expectedResult);
        }

        [Fact]
        public void GetNumberOfAssociatedTrades_Should_Return_0_If_AssociatedTradesCount_Equals_0()
        {
            //Arrange
            int clientCompanyOpiId = 44;
            int associatedTradesCount = 0;

            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var clientCompanyAccountsUowMock = new Mock<IClientCompanyAccountsUoW>();

            clientCompanyAccountsUowMock.Setup(x => x.GetAssociatedTradesCount(It.IsAny<int>(), It.IsAny<int>())).Returns(associatedTradesCount);

            var service = new ClientCompanyAccountsService(clientCompanyAccountsUowMock.Object, null, null);

            //Act
            var expectedResult = 0;
            var result = service.GetNumberOfAssociatedTrades(clientCompanyOpiId);

            //Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Get_ClientCompanyContact_Categories_For_ClientContact()
        {
            //Arrange
            int clientCompanyContactId = 44;

            DataAccess.Entities.ClientCompanyContact clientCompanyContact =
                new DataAccess.Entities.ClientCompanyContact() {Id = clientCompanyContactId};

            const string contactCategoryDescription1 = "EUR/USD";
            const string contactCategoryDescription2 = "EUR/GBP";
            const string contactCategoryDescription3 = "GBP/USD";
            const string contactCategoryDescription4 = "GBP/EUR";

            const int contactCategoryId1 = 1;
            const int contactCategoryId2 = 2;
            const int contactCategoryId3 = 3;
            const int contactCategoryId4 = 4;

            List<ClientCompanyContactCategory> contactCategoryList = new List<ClientCompanyContactCategory>()
            {
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryId = contactCategoryId1,

                    ClientCompanyContact = clientCompanyContact,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId1, Description = contactCategoryDescription1}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryId = contactCategoryId2,

                    ClientCompanyContact = clientCompanyContact,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId2, Description = contactCategoryDescription2}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryId = contactCategoryId3,

                    ClientCompanyContact = clientCompanyContact,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId3, Description = contactCategoryDescription3}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId,
                    ContactCategoryId = contactCategoryId4,

                    ClientCompanyContact = clientCompanyContact,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId4, Description = contactCategoryDescription4}
                }
            };

            IList<ClientCompanyContactCategoryModel> expectedResult = new List<ClientCompanyContactCategoryModel>()
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

            var clientCompanyUow = new Mock<IClientCompanyUow>();
            
            clientCompanyUow.Setup(x => x.GetClientCompanyContactCategories(It.IsAny<int>())).Returns(contactCategoryList.AsQueryable().BuildMock().Object);

            var service = new ClientCompanyService(clientCompanyUow.Object,null,null,null,null);

            //Act
            IEnumerable<ClientCompanyContactCategoryModel> result = service.GetClientCompanyContactCategories(clientCompanyContactId).Result;

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Get_All_ClientCompanyContact_Categories()
        {
            //Arrange
            int clientCompanyContactId1 = 44;
            int clientCompanyContactId2 = 50;
            int clientCompanyContactId3 = 100;

            DataAccess.Entities.ClientCompanyContact clientCompanyContact1 =
                new DataAccess.Entities.ClientCompanyContact() { Id = clientCompanyContactId1 };

            DataAccess.Entities.ClientCompanyContact clientCompanyContact2 =
                new DataAccess.Entities.ClientCompanyContact() { Id = clientCompanyContactId2 };

            DataAccess.Entities.ClientCompanyContact clientCompanyContact3 =
                new DataAccess.Entities.ClientCompanyContact() { Id = clientCompanyContactId3 };

            const string contactCategoryDescription1 = "EUR/USD";
            const string contactCategoryDescription2 = "EUR/GBP";
            const string contactCategoryDescription3 = "GBP/USD";
            const string contactCategoryDescription4 = "GBP/EUR LHS";
            const string contactCategoryDescription5 = "GBP/EUR RHS";
            const string contactCategoryDescription6 = "GBP/AUD LHS";
            const string contactCategoryDescription7 = "GBP/EUR RHS";

            const int contactCategoryId1 = 1;
            const int contactCategoryId2 = 2;
            const int contactCategoryId3 = 3;
            const int contactCategoryId4 = 4;
            const int contactCategoryId5 = 5;
            const int contactCategoryId6 = 6;
            const int contactCategoryId7 = 7;

            List<ClientCompanyContactCategory> contactCategoryList = new List<ClientCompanyContactCategory>()
            {
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryId = contactCategoryId1,

                    ClientCompanyContact = clientCompanyContact1,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId1, Description = contactCategoryDescription1}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryId = contactCategoryId2,

                    ClientCompanyContact = clientCompanyContact1,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId2, Description = contactCategoryDescription2}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryId = contactCategoryId3,

                    ClientCompanyContact = clientCompanyContact1,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId3, Description = contactCategoryDescription3}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryId = contactCategoryId4,

                    ClientCompanyContact = clientCompanyContact1,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId4, Description = contactCategoryDescription4}
                },

                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId2,
                    ContactCategoryId = contactCategoryId5,

                    ClientCompanyContact = clientCompanyContact2,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId5, Description = contactCategoryDescription5}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId2,
                    ContactCategoryId = contactCategoryId6,

                    ClientCompanyContact = clientCompanyContact2,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId6, Description = contactCategoryDescription6}
                },
                new ClientCompanyContactCategory()
                {
                    ClientCompanyContactId = clientCompanyContactId3,
                    ContactCategoryId = contactCategoryId7,

                    ClientCompanyContact = clientCompanyContact3,
                    ContactCategory = new ContactCategory()
                        {Id = contactCategoryId7, Description = contactCategoryDescription7}
                }
            };

            IList<ClientCompanyContactCategoryModel> expectedResult = new List<ClientCompanyContactCategoryModel>()
            {
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryDescription = contactCategoryDescription1,
                    ContactCategoryId = contactCategoryId1
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryDescription = contactCategoryDescription2,
                    ContactCategoryId = contactCategoryId2
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryDescription = contactCategoryDescription3,
                    ContactCategoryId = contactCategoryId3
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId1,
                    ContactCategoryDescription = contactCategoryDescription4,
                    ContactCategoryId = contactCategoryId4
                },

                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId2,
                    ContactCategoryDescription = contactCategoryDescription5,
                    ContactCategoryId = contactCategoryId5
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId2,
                    ContactCategoryDescription = contactCategoryDescription6,
                    ContactCategoryId = contactCategoryId6
                },
                new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = clientCompanyContactId3,
                    ContactCategoryDescription = contactCategoryDescription7,
                    ContactCategoryId = contactCategoryId7
                },
            };

            var clientCompanyUow = new Mock<IClientCompanyUow>();

            clientCompanyUow.Setup(x => x.GetClientCompanyContactCategories(It.IsAny<int>())).Returns(contactCategoryList.AsQueryable().BuildMock().Object);

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            IEnumerable<ClientCompanyContactCategoryModel> result = service.GetClientCompanyContactCategories(clientCompanyContactId1).Result;

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
        
        [Fact]
        public void Add_ContactCategory_Successful()
        {
            var model = new ContactCategoryModel(){Description = "New Contact Category"};
            var clientCompanyUow = new Mock<IClientCompanyUow>();

            var contactCategories = new List<ContactCategory>();

            var contactCategory = contactCategories.AsQueryable();
            clientCompanyUow.Setup(x => x.GetContactCategory(It.IsAny<string>())).Returns(contactCategory);
            clientCompanyUow.Setup(x => x.AddContactCategory(It.IsAny<ContactCategory>()));

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.AddContactCategory(model);

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public void Add_ContactCategory_Not_Successful()
        {
            string description = "New Contact Category";
            var model = new ContactCategoryModel() { Description = description };
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            
            var contactCategories = new List<ContactCategory>() { new ContactCategory(){Description = description}};

            var contactCategory = contactCategories.AsQueryable();
            clientCompanyUow.Setup(x => x.GetContactCategory(It.IsAny<string>())).Returns(contactCategory);
            clientCompanyUow.Setup(x => x.AddContactCategory(It.IsAny<ContactCategory>()));

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.AddContactCategory(model);

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void Get_Contact_Categories()
        {
            //Arrange
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

            List<ContactCategory> contactCategoryList = new List<ContactCategory>() 
            {
                new ContactCategory()
                {
                    Id = contactCategoryId1,
                    Description = contactCategoryDescription1,
                    Sequence = contactCategorySequence1
                },
                new ContactCategory()
                {
                    Id = contactCategoryId2,
                    Description = contactCategoryDescription2,
                    Sequence = contactCategorySequence2
                },
                new ContactCategory()
                {
                    Id = contactCategoryId3,
                    Description = contactCategoryDescription3,
                    Sequence = contactCategorySequence3
                },
                new ContactCategory()
                {
                    Id = contactCategoryId4,
                    Description = contactCategoryDescription4,
                    Sequence = contactCategorySequence4
                },
            };

            IList<ContactCategoryModel> expectedResult = new List<ContactCategoryModel>()
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
                },
            };

            var clientCompanyUow = new Mock<IClientCompanyUow>();

            clientCompanyUow.Setup(x => x.GetContactCategories()).Returns(contactCategoryList.AsQueryable().BuildMock().Object);

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            IEnumerable<ContactCategoryModel> result = service.GetContactCategories().Result;

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetClientCompanyContact_Success_With_Valid_clientCompanyContactID()
        {
            //Arrange
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyContact = new DataAccess.Entities.ClientCompanyContact
            {
                Id = 1,
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                PrimaryContact = false,
                Birthday = DateTime.Now,
                AuthUser = new AuthUser(),
                ClientCompany = new ClientCompany()
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 1,
                AuthUsertId = 0
            };

            clientCompanyUow.Setup(x => x.GetCurrentClientCompanyContact(It.IsAny<ClientCompanyContactSearchModel>()))
                .Returns(clientCompanyContact);

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.GetClientCompanyContact(clientCompanyContactSearchContext);

            //Assert
            result.Should().BeOfType<ClientCompanyContactResponseModel>().And.NotBeNull();
            result.CompanyContactModel.ID.Should().Be(1);
            result.CompanyContactModel.ContactEmail.Should().Be("testaccount@synetec.co.uk");
        }

        [Fact]
        public void GetClientCompanyContact_Success_With_Valid_AuthUsertId()
        {
            //Arrange
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyContact = new DataAccess.Entities.ClientCompanyContact
            {
                Id = 1,
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                PrimaryContact = false,
                Birthday = DateTime.Now,
                AuthUser = new AuthUser(),
                ClientCompany = new ClientCompany()
            };
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 0,
                AuthUsertId = 1
            };

            clientCompanyUow.Setup(x => x.GetCurrentClientCompanyContact(It.IsAny<ClientCompanyContactSearchModel>()))
                .Returns(clientCompanyContact);

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.GetClientCompanyContact(clientCompanyContactSearchContext);

            //Assert
            result.Should().BeOfType<ClientCompanyContactResponseModel>().And.NotBeNull();
            result.CompanyContactModel.ID.Should().Be(1);
            result.CompanyContactModel.ContactEmail.Should().Be("testaccount@synetec.co.uk");
        }

        [Fact]
        public void GetClientCompanyContact_Returns_Empty_Model_When_clientCompanyContactID_And_AuthUsertId_Equal_Zero()
        {
            //Arrange
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyContactSearchContext = new ClientCompanyContactSearchContext()
            {
                ClientCompanyContactId = 0,
                AuthUsertId = 0
            };

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.GetClientCompanyContact(clientCompanyContactSearchContext);

            //Assert
            result.Should().NotBeNull().And.BeOfType(typeof(ClientCompanyContactResponseModel));
            result.CompanyContactModel.Should().BeNull();
        }

        [Fact]
        public void GetCompanyContactList_Success_With_Valid_clientCompanyContactID()
        {
            //Arrange
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            int clientCompanytID = 1;

            var user = new DataAccess.Entities.ClientCompanyContact
            {
                Id = 1,
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                PrimaryContact = false,
                Birthday = DateTime.Now
            };
            var userList = new List<DataAccess.Entities.ClientCompanyContact> { user }.AsQueryable();

            clientCompanyUow.Setup(x => x.GetClientCompanyContactList(It.IsAny<int>()))
                .Returns(userList);

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.GetCompanyContactList(clientCompanytID);

            //Assert
            result.Should().NotBeNull();
            result.CompanyContactListModel.Count.Should().Be(1);
            result.CompanyContactListModel.FirstOrDefault().ID.Should().Be(1);
            result.CompanyContactListModel.FirstOrDefault().ContactEmail.Should().Be("testaccount@synetec.co.uk");
        }

        [Fact]
        public void GetCompanyContactList_Returns_Empty_Object_When_clientCompanyContactID_Equals_Zero()
        {
            //Arrange
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            int clientCompanyID = 0;

            clientCompanyUow.Setup(x => x.GetClientCompanyContactList(It.IsAny<int>()));

            var service = new ClientCompanyService(clientCompanyUow.Object, null, null, null, null);

            //Act
            var result = service.GetCompanyContactList(clientCompanyID);

            //Assert
            result.Should().BeAssignableTo<ClientCompanyContactListResponseModel>();
            result.CompanyContactListModel.Should().BeEmpty();
        }

        /*
            Task<IEnumerable<ClientCompanyContactCategoryModel>> GetClientCompanyContactCategories(int clientCompanyContactId);
            Task<IEnumerable<ContactCategoryModel>> GetContactCategories();
            
            bool AddContactCategory(ContactCategoryModel model);
            ContactCategory GetContactCategory(int contactCategoryId);
            ContactCategory GetContactCategory(string contactCategoryDescription);
            bool ProcessClientCompanyContactCategories(ClientCompanyContactBulkCategoryModel model);
         
         */
    }


}
