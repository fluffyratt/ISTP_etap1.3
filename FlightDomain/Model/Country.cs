using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Country
{
    public int Id { get; set; }

    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; } = new List<City>();
}
