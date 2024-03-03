using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class City
{
    public int Id { get; set; }

    [Display(Name = "Назва міста")]
    [Required(ErrorMessage = "Напишіть будь-яке місто!")]
    public string Name { get; set; } = null!;

    [Display(Name = "Назва Країни")]
    [Required(ErrorMessage = "Оберіть країну!")]
    public int CountryId { get; set; }

    public virtual ICollection<Airport> Airports { get; } = new List<Airport>();


    [Display(Name = "Назва Країни")]
    public virtual Country Country { get; set; } = null!;
}
