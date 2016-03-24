using System;
using System.Configuration;
using CrmExercise.Domain.Abstract;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace CrmExercise.Domain.Concrete
{
  public class ServiceContextFactory : IServiceContextFactory
  {

    private CrmServiceClient ServiceClient { get; set; }
    private IOrganizationService OrgService { get; set; }
    private ServiceContext SvcContext { get; set; }


    public ServiceContext CreateContext()
    {
      var connectionString = ConfigurationManager.AppSettings["ServiceClientConStr"];

      ServiceClient = new CrmServiceClient(connectionString);
      OrgService = (IOrganizationService)ServiceClient.OrganizationWebProxyClient ??
                   (IOrganizationService)ServiceClient.OrganizationServiceProxy;

      SvcContext = new ServiceContext(OrgService);

      return SvcContext;
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
        if (this.ServiceClient != null) this.ServiceClient = null;
        if (this.OrgService != null) this.OrgService = null;
        this.SvcContext?.Dispose();
      }
      // Release unmanaged resources.

    }


    ~ServiceContextFactory()
    {
      Dispose(false);
    }

    #endregion
  }
}
