using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.WebPages;
using CrmExercise.Domain.Abstract;
using CrmExercise.Domain.Concrete;
using Moq;
using Ninject;
using Ninject.Web.Common;

namespace CrmExercise.WebUI.Infrastructure
{

  public class NinjectDependencyResolver : IDependencyResolver
  {
    private IKernel _kernel;

    public NinjectDependencyResolver(IKernel kernelParam)
    {
      _kernel = kernelParam;
      AddBindings();
    }

    public object GetService(Type serviceType)
    {
      return _kernel.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      return _kernel.GetAll(serviceType);
    }

    private void AddBindings()
    {
      var isMockBinding = false;
      if (ConfigurationManager.AppSettings["IsMockBinding"].IsBool())
      {
        isMockBinding = Convert.ToBoolean(ConfigurationManager.AppSettings["IsMockBinding"]);
      };

      if (isMockBinding)
      {
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
            AccountId =new Guid(),
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
        _kernel.Bind<ICrmAccountRepository>().ToConstant(mock.Object);

      }
      else
      {
        _kernel.Bind<ICrmAccountRepository>().To<CrmAccountRepository>();
        _kernel.Bind<IServiceContextFactory>().To<ServiceContextFactory>().InSingletonScope();
      }
    }
  }
}
