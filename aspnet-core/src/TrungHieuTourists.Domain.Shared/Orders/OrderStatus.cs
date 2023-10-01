using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Orders
{
    public enum OrderStatus
    {
        New,
        Confirmed,
        Processing,
        Shipping,
        Finished,
        Canceled
    }
}
