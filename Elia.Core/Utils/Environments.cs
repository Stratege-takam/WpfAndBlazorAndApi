
using Elia.Core.Enums;

namespace Elia.Core.Utils;

/// <summary>
/// the environements helper to check in which environment the app is.
/// </summary>
public static partial class Environments
{
    /// <summary>
    /// Checks whether the current environment corresponds to the list of EnvironmentEnum parameters.
    /// </summary>
    /// <param name="envEnums">The list of EnvironmentEnum parameters</param>
    /// <returns></returns>
    public static bool IsEnvironment(params EnvironmentEnum[] envEnums)
        => envEnums.Any(e => e == Current);

    /// <summary>
    /// Gets the environment name given the conditional flags defined in the project builds.
    /// </summary>
    /// <remarks>
    /// Currently due to a problem in Visual Studio, changing the Debug configuration does not display(ungrey) the needed string but the build is OK.
    /// </remarks>
    /// <returns>The environement name.</returns>
    public static EnvironmentEnum Current 
        =>

    #if DEBUG
        EnvironmentEnum.Debug;
    
    #elif TEST
    EnvironmentEnum.Test;

    #elif PROD
        EnvironmentEnum.Prod;
    #endif
}