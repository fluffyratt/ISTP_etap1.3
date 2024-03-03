using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Airport
{
    public int Id { get; set; }


    [Display(Name = "Місто")]
    [Required(ErrorMessage = "Назва міста є обов'язковою!")]
    [MaxLength(50, ErrorMessage = "Введіть коротшу назву")]
    public int CityId { get; set; }

    [Display(Name = "Назва")]
    [Required(ErrorMessage = "Назва є обов'язковою!")]
    [MaxLength(50, ErrorMessage = "Введіть коротшу назву")]
    public string Name { get; set; } = null!;

    [Display(Name = "Місто")]
    [Required(ErrorMessage = "Назва міста є обов'язковою!")]
    public virtual City City { get; set; } = null!;

    [Display(Name = "Рейси прибуття")]
    public virtual ICollection<Flight> FlightArrivalAiroportNavigations { get; } = new List<Flight>();

    [Display(Name = "Рейси відбуття")]
    public virtual ICollection<Flight> FlightDepartureAiroportNavigations { get; } = new List<Flight>();
}
