using ShareInvest.Models;

namespace ShareInvest.EventHandler;

class LocationArgs : EventArgs
{
    internal LocItem Item
    {
        get;
    }

    internal LocationArgs(LocItem item) => Item = item;
}