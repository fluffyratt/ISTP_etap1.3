using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Airport
{
    public int Id { get; set; }

    [Display(Name = "Код міста")]
    public int CityId { get; set; }

    [Display(Name = "Назва аеропорту")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]

    public virtual City City { get; set; } = null!;

    [Display(Name = "Рейси прибуття")]
    public virtual ICollection<Flight> FlightArrivalAiroportNavigations { get; } = new List<Flight>();

    [Display(Name = "Рейси відбуття")]
    public virtual ICollection<Flight> FlightDepartureAiroportNavigations { get; } = new List<Flight>();
}
