using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class City
{
    public int Id { get; set; }

    [Display(Name = "Назва міста")]
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<Airport> Airports { get; } = new List<Airport>();

    [Display(Name = "Назва Країни")]
    public virtual Country Country { get; set; } = null!;
}
