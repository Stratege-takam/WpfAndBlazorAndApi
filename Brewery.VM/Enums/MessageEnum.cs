using System.ComponentModel;

namespace Brewery.VM.Enums;

public enum MessageEnum
{
    MsgLogout,
    MsgSwitchLang,
    MsgNavigationPage,
    MsgDisplayBrewery,
    MsgOpenCreateBeer,
    [Description("MSG_OPEN_TAB_DETAIL_BEER")]
    MsgOpenDetailBeer
}