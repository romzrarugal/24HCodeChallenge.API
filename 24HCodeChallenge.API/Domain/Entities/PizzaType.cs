using System;
using System.Collections.Generic;

namespace MyProject.Domain.Entities;

public partial class PizzaType
{
    public string PizzaTypeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public string? Ingredients { get; set; }

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
