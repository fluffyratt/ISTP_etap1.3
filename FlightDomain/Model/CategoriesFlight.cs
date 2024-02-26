using System;
using System.Collections.Generic;

namespace FlightDomain.Model;

public partial class CategoriesFlight
{
    public int CategoryId { get; set; }

    public int FlightId { get; set; }

    public int SeatsNumber { get; set; }

    public int Price { get; set; }

    public int Id { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
