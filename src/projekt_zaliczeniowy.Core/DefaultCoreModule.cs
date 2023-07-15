using Autofac;
using projekt_zaliczeniowy.Core.Interfaces;
using projekt_zaliczeniowy.Core.Services;

namespace projekt_zaliczeniowy.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<CommentValidationService>().As<ICommentValidationService>().InstancePerLifetimeScope();    
  }
}
