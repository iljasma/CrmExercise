using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrmExercise.Domain.Abstract;
using CrmExercise.WebUI.Controllers;
using CrmExercise.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmExercise.WebUI.HtmlHelpers;
using Moq;

namespace CrmExercise.UnitTests
{
  [TestClass]
  public class AccountTests
  {

    [TestMethod]
    public void Can_Filter_Active_Accounts()
    {
      // Arrange
      // - create the mock repository
      var mock = new Mock<ICrmAccountRepository>();
      mock.Setup(m => m.Accounts).Returns(new List<Account>
      {
        new Account
        {
          AccountId = new Guid(),
          Name = "Fourth Coffee",
          Address1_Line1 = "Aškerčeva 15",
          Address1_City = "Ljubljana",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "1000",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Fifth Coffee",
          Address1_Line1 = "Devetakova 7",
          Address1_City = "Ljubljana",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "1001",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Sixth Coffee",
          Address1_Line1 = "Ul. Lojzeta Smrdeta 12",
          Address1_City = "Jesenice",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "4270",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Ninth Coffee",
          Address1_Line1 = "Grdega pogleda 1",
          Address1_City = "Postojna",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "6000",
          StateCode = AccountState.Inactive
        }
      });

      // Arrange - create a controller and make the page size 3 items
      var controller = new AccountController(mock.Object);
      controller.PageSize = 4;

      // Action
      var result = ((AccountsListViewModel) controller.List(1).Model).AccountLabels.ToArray();

      // Assert
      Assert.IsTrue(result.Length == 3);
    }

    [TestMethod]
    public void Can_Paginate()
    {
      // Arrange
      var mock = new Mock<ICrmAccountRepository>();
      mock.Setup(m => m.Accounts).Returns(new List<Account>
      {
        new Account
        {
          AccountId = new Guid(),
          Name = "Fourth Coffee",
          Address1_Line1 = "Aškerčeva 15",
          Address1_City = "Ljubljana",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "1000",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Fifth Coffee",
          Address1_Line1 = "Devetakova 7",
          Address1_City = "Ljubljana",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "1001",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Sixth Coffee",
          Address1_Line1 = "Ul. Lojzeta Smrdeta 12",
          Address1_City = "Jesenice",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "4270",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = null,
          Name = "Seventh Coffee",
          Address1_Line1 = "Žlobudretova 3",
          Address1_City = "Kranj",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "4000",
          StateCode = AccountState.Active
        },
        new Account
        {
          AccountId = new Guid(),
          Name = "Eight Coffee",
          Address1_Line1 = "Konjskega odreda 24",
          Address1_City = "Kranj",
          Address1_Country = "Slovenija",
          Address1_PostalCode = "4000",
          StateCode = AccountState.Active
        },
      });
      var controller = new AccountController(mock.Object)
      {
        PageSize = 3
      };
      // Act
      var result = (AccountsListViewModel) controller.List(2).Model;
      // Assert
      var accArray = result.AccountLabels.ToArray();
      Assert.IsTrue(accArray.Length == 2);
      Assert.AreEqual(accArray[0].Name, "Seventh Coffee");
      Assert.AreEqual(accArray[1].Name, "Sixth Coffee");
    }

    [TestMethod]
    public void Can_Generate_Page_Links()
    {
      // Arrange - define an HTML helper - we need to do this
      // in order to apply the extension method
      HtmlHelper myHelper = null;

      // Arrange - create PagingInfo data
      var pagingInfo = new MiPagingInfo
      {
        CurrentPage = 2,
        TotalItems = 28,
        ItemsPerPage = 10
      };

      // Arrange - set up the delegate using a lambda expression
      Func<int, string> pageUrlDelegate = i =>
      {
        return "Page" + i;
      };
      // Act
      var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

      // Assert
      Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                      + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                      + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
        result.ToString());
    }
  }
}
