using System;

namespace CrmExercise.WebUI.Models
{
  public class AccountLabel
  {

    public AccountLabel()
    {
      StateCode = 0;
      AccountId = new Guid();
    }

    public Guid? AccountId { get; set; }
    public string Name { get; set; }
    public string Address1_Line1 { get; set; }
    public string Address1_City { get; set; }
    public string Address1_PostalCode { get; set; }
    public string Address1_Country { get; set; }
    public AccountState? StateCode { get; set; }
  }
}
