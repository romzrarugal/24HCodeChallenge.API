using System;
using System.Collections.Generic;

namespace MyProject.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
