using System.ComponentModel;

namespace Brewery.Web.Shared.Enums;

public enum AppRoutingEnum
{
    [Description("Login")]
    Login,
    [Description("Brewery")]
    Brewery,
    [Description("Register")]
    Register,
    [Description("Wholesaler")]
    Wholesaler,
    [Description("User")]
    User,
    [Description("Home")]
    Home
}