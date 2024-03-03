using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Country
{
    public int Id { get; set; }

    [Display(Name = "Назва")]
    [Required(ErrorMessage = "Назва країни є обов'язковою!")]
    public string Name { get; set; } = null!;

    [Display(Name = "Міста")]
    public virtual ICollection<City> Cities { get; } = new List<City>();
}
