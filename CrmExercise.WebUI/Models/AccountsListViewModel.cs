using System.Collections.Generic;


namespace CrmExercise.WebUI.Models
{
  public class AccountsListViewModel
  {
    public AccountsListViewModel()
    {
      CurrentCategory = null;
    }

    public IEnumerable<Account> Accounts { get; set; }
    public IEnumerable<AccountLabel> AccountLabels { get; set; }
    public MiPagingInfo MiPagingInfo { get; set; }
    public string CurrentCategory { get; set; }
  }
}