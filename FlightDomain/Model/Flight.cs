using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Flight : Entity 
{ 
  
    public int Id { get; set; }

    [Required(ErrorMessage = "Назва є обов'язковою!")]
    [Display(Name = "Авіарейс")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Дата та час є обов'язковими!")]
    [Display(Name = "Дата та час")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Назва категорії є обов'язковою!")]
    [MaxLength(50, ErrorMessage = "Введіть коротший опис")]
    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;

    [Display(Name = "Тривалість")]
    [Required(ErrorMessage = "Тривалість є обов'язковою!")]
    [Range(1, int.MaxValue, ErrorMessage = "Значення повинно бути більше 0")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Тривалість повинна бути цілим додатнім числом")]
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
