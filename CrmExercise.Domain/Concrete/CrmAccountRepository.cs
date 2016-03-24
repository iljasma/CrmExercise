using System;
using System.Collections.Generic;
using CrmExercise.Domain.Abstract;



namespace CrmExercise.Domain.Concrete
{
  public class CrmAccountRepository : ICrmAccountRepository
  {

    private readonly ServiceContext _svcContext;
    private readonly IServiceContextFactory _serviceContextFactory;

    public CrmAccountRepository(IServiceContextFactory serviceContextFactory)
    {
      this._serviceContextFactory = serviceContextFactory;
      this._svcContext = _serviceContextFactory.CreateContext();
    }


    public IEnumerable<Account> Accounts => _svcContext.AccountSet;


    public IEnumerable<Account> GetAccounts()
    {
      return _svcContext.AccountSet;
    }

    #region IDisposable Members

    /// <summary>
    /// Separate implemention of Dispose pattern 
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        // Release managed resources.
        if (this._serviceContextFactory != null) _serviceContextFactory.Dispose();
        if (this._svcContext != null) _svcContext.Dispose();
      }
      // Release unmanaged resources.

    }


    ~CrmAccountRepository()
    {
      Dispose(false);
    }

    #endregion
  }
}
