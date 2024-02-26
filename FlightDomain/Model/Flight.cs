using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Flight : Entity
{ 
  
    public int Id { get; set; }

    [Display(Name = "Авіарейс")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]

    [Display(Name = "Дата")]
    public byte[] Date { get; set; } = null!;

    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;

    [Display(Name = "Тривалість")]
    public int Duration { get; set; }

    public int DepartureAiroport { get; set; }

    public int ArrivalAiroport { get; set; }

    [Display(Name = "Аеропорт прибуття")]
    public virtual Airport ArrivalAiroportNavigation { get; set; } = null!;

    public virtual ICollection<CategoriesFlight> CategoriesFlights { get; } = new List<CategoriesFlight>();

    [Display(Name = "Аеропорт відбуття")]
    public virtual Airport DepartureAiroportNavigation { get; set; } = null!;
}
