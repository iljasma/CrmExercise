using System;

namespace CrmExercise.Domain.Abstract
{
  public interface IServiceContextFactory: IDisposable
  {
    ServiceContext CreateContext();
  }
}
