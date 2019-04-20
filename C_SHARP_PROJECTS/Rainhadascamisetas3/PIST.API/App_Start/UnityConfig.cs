using Microsoft.Practices.Unity;
using System;
using System.Web.Http;
using Unity.WebApi;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Practices.Unity.Configuration;

namespace PIST.API
{
	public static class UnityConfig
	{
		public static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
		{
			var container = new UnityContainer();
			return container;
		});

		public static IUnityContainer GetConfiguredContainer()
		{
			return container.Value;
		}

		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			container.LoadConfiguration();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();

			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}