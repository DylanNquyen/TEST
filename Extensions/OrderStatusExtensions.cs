using THLTW_BT3.Models;

public static class OrderStatusExtensions
{
    public static string ToBadgeClass(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Pending => "bg-warning",
            OrderStatus.Shipped => "bg-info",
            OrderStatus.Delivered => "bg-success",
            _ => "bg-secondary"
        };
    }
}
