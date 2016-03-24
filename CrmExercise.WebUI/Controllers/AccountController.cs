using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using CrmExercise.Domain.Abstract;
using CrmExercise.WebUI.Models;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace CrmExercise.WebUI.Controllers
{
  public class AccountController : Controller
  {
    private ICrmAccountRepository _repository;
    public int PageSize = 4;

    public AccountController(ICrmAccountRepository accountRepository)
    {
      if (accountRepository == null) throw new ArgumentNullException("accountRepository");

      this._repository = accountRepository;
      if (ConfigurationManager.AppSettings["AccountListPageSize"].IsInt())
      {
        PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["AccountListPageSize"]);
      } ;
    }

    [HandleError(ExceptionType = typeof (System.Exception), View = "ErrorPage", Master = "_Layout")]
    // GET: Account
    public ViewResult List(int page = 1)
    {
 
      var accountLabels = _repository.Accounts
        .Where(a => a.StateCode == AccountState.Active)
        .OrderBy(a => a.Name)
        .Skip((page - 1)*PageSize)
        .Take(PageSize)
        .Select(a => new AccountLabel
        {
          Name = a.Name,
          Address1_Line1 = a.Address1_Line1,
          Address1_City = a.Address1_City,
          Address1_PostalCode = a.Address1_PostalCode,
          Address1_Country = a.Address1_Country,
          AccountId = a.AccountId,
          StateCode = a.StateCode
        });

      var model = new AccountsListViewModel
      {
        AccountLabels = accountLabels,
        MiPagingInfo = new MiPagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = PageSize,
          TotalItems = _repository.Accounts.Count()
        }
      };

      return View(model);
    }

    #region IDisposable Members

    protected override void Dispose(bool disposing) {
      if (disposing)
      {
        // Release managed resources.
        this._repository?.Dispose();
        if (this._repository != null) this._repository = null;
      }
      // Release unmanaged resources.

    }

    #endregion
  }
}
