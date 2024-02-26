using System;
using System.Collections.Generic;

namespace FlightDomain.Model;

public partial class Ticket
{
    public int Id { get; set; }

    public byte[] PurchaseDate { get; set; } = null!;

    public int UserId { get; set; }

    public int CategoriesFlightsId { get; set; }

    public virtual CategoriesFlight CategoriesFlights { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
