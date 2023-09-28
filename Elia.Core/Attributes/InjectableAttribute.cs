namespace Elia.Core.Attributes;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class InjectableAttribute : Attribute
{
    /// <summary>
    /// Property to determine lifetime of injected type
    /// </summary>
    public ServiceLifetime ServiceLifetime { get; }

    /// <summary>
    /// Assign explicit interface to implement
    /// </summary>
    public Type ExplicitInterface { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="explicitInterface"></param>
    /// <param name="serviceLifetime"></param>
    /// <exception cref="ArgumentException"></exception>
    public InjectableAttribute(Type explicitInterface = null, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        ServiceLifetime = serviceLifetime;

        if (explicitInterface != null && !explicitInterface.IsInterface)
            throw new ArgumentException(nameof(explicitInterface));

        ExplicitInterface = explicitInterface;
    }
}