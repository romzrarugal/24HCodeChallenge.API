using System;
using System.Collections.Generic;

namespace MyProject.Domain.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public string PizzaId { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Pizza Pizza { get; set; } = null!;
}
