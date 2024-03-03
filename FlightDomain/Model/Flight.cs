using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Flight : Entity
{ 
  
    public int Id { get; set; }

    [Display(Name = "Авіарейс")]
    [Required(ErrorMessage = "Назва є обов'язковою!")]
    public string Name { get; set; } = null!;

    [Display(Name = "Дата")]
    [Required(ErrorMessage = "Дата та час є обов'язковими!")]
    public DateTime Date { get; set; }

    [Display(Name = "Опис")]
    [MaxLength(50, ErrorMessage = "Введіть коротший опис")]
    public string Description { get; set; } = null!;

    [Display(Name = "Тривалість")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Значення повинно бути більше 0")]
    public int Duration { get; set; }

    [Display(Name = "Аеропорт відбуття")]
    public int DepartureAiroport { get; set; }

    [Display(Name = "Аеропорт прибуття")]
    public int ArrivalAiroport { get; set; }

    [Display(Name = "Аеропорт прибуття")]
    public virtual Airport ArrivalAiroportNavigation { get; set; } = null!;

    public virtual ICollection<CategoriesFlight> CategoriesFlights { get; } = new List<CategoriesFlight>();

    [Display(Name = "Аеропорт відбуття")]
    public virtual Airport DepartureAiroportNavigation { get; set; } = null!;
}
