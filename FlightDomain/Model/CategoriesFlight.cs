using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class CategoriesFlight 
{
    [Display(Name = "Назва категорії")]
    public int CategoryId { get; set; }

    [Display(Name = "Авіарейс")]
    public int FlightId { get; set; }

    [Display(Name = "Номер місця")]
    public int SeatsNumber { get; set; }

    [Display(Name = "Ціна")]
    [Required(ErrorMessage = "Ціна є обов'язковою!")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Ціна повинна бути додатнім десятковим числом з максимально двома знаками після крапки")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Значення повинно бути більше 0")]
    public int Price { get; set; }

    public int Id { get; set; }

    [Display(Name = "Категорія")]

    public virtual Category Category { get; set; } = null!;

    [Display(Name = "Авіарейс")]
    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
