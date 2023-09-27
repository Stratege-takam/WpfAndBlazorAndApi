using Elia.Core.Attributes;

namespace Elia.Core.Containers;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;


    /// <summary>
    /// 
    /// </summary>
  public static class RegisterServicesFromAssembly
    {
        public static IList<string> getWPFControl() { 
            return new List<string>() {
                "UserControl",
                "Window",
                "TabControl",
                "DialogWindowBase",
                "WindowBase",
                "UserControlBase",
                "DataGrid"
            }; 
        }

        // public static void AddAllAssembly<T>(this IServiceCollection services,  Assembly assembly)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AutoInject(this IServiceCollection services,  IList<Assembly> assemblies)
        {

            var injectableTypes =  assemblies.SelectMany(assembly => assembly.DefinedTypes 
                .Where(x => x.GetCustomAttributes(typeof(InjectableAttribute), 
                    false).FirstOrDefault() != null && x.IsClass)).ToList();
            
        
       
            foreach (var injectableType in injectableTypes)
            {
                var injectAttributeData = injectableType
                    .GetCustomAttributes(typeof(InjectableAttribute), false).First() as InjectableAttribute;

                if (injectAttributeData.ExplicitInterface != null)
                {
                    services.Add(new ServiceDescriptor(
                        injectAttributeData.ExplicitInterface,
                        injectableType,
                        injectAttributeData.ServiceLifetime));
                }
                else if (
                getWPFControl().All(e =>  injectableType.BaseType?.Name != e) && injectableType.BaseType?.Name.StartsWith("Metro") != true && 
                injectableType.ImplementedInterfaces.Any()
                         && (injectableType.ImplementedInterfaces.Count() > 1 || injectableType.ImplementedInterfaces.Count() == 1 
                             && !injectableType.ImplementedInterfaces.Any(i => i.Name.Contains("IBaseRepository") || i.Name.Contains("IBaseBl"))) 
                         )
                {
                    foreach (var implementedInterface in injectableType.ImplementedInterfaces)
                    {
                        services.Add(new ServiceDescriptor(
                            implementedInterface,
                            injectableType,
                            injectAttributeData.ServiceLifetime));
                    }
                }
                else
                {

                    services.Add(new ServiceDescriptor(
                        injectableType,
                        injectableType,
                        injectAttributeData.ServiceLifetime)); 
                }
            }
        }

    }