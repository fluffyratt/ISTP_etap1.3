using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class City
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Назва міста є обов'язковою!")]
    [MaxLength(50, ErrorMessage = "Введіть коротшу назву міста")]
    [Display(Name = "Місто")]
    public string Name { get; set; } = null!;

    [Display(Name = "Країна")]
    [Required(ErrorMessage = "Оберіть країну!")]
    public int CountryId { get; set; }

    public virtual ICollection<Airport> Airports { get; } = new List<Airport>();


    [Display(Name = "Назва Країни")]
    public virtual Country Country { get; set; } = null!;
}
