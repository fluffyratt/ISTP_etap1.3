using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Category 
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Назва категорії є обов'язковою!")]
    [MaxLength(50, ErrorMessage = "Введіть коротшу назву категорії")]
    [Display(Name = "Категорія(бізнес/економ)")]
    public string Name { get; set; } = null!;

    [Display(Name = "Авіарейси")]
    public virtual ICollection<CategoriesFlight> Categories { get; } = new List<CategoriesFlight>();
}
