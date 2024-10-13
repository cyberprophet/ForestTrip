using ShareInvest.Models;

namespace ShareInvest.EventHandler;

class HouseArgs : EventArgs
{
    internal HouseItem Item
    {
        get;
    }

    internal HouseArgs(HouseItem item) => Item = item;
}