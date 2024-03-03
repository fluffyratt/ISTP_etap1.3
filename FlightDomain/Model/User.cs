using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class User
{
    public int Id { get; set; }

    [Display(Name = "Ім'я")]
    [MaxLength(50, ErrorMessage = "Введіть коротше ім'я")]
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public string Name { get; set; } = null!;

    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public string Surname { get; set; } = null!;

    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "E-mail є обов'язковим!")]
    [EmailAddress(ErrorMessage = "Невірний формат E-mail")]
    public string Email { get; set; } = null!;


    [Display(Name = "Номер телефону")]
    [Required(ErrorMessage = "Номер телефону є обов'язковим!")]
    [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Телефон має починатися з +, мати код країни та від 6 до 14 цифр.")]
    public string PhoneNumber { get; set; } = null!;
    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
