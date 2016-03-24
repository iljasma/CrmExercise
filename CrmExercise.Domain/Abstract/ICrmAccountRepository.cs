using System;
using System.Collections.Generic;

namespace CrmExercise.Domain.Abstract
{
  public interface ICrmAccountRepository: IDisposable
  {
    IEnumerable<Account> Accounts { get; }

    IEnumerable<Account> GetAccounts();
  }
}
