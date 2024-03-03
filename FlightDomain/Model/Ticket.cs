using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightDomain.Model;

public partial class Ticket
{
    public int Id { get; set; }

    [Display(Name = "Дата купівлі")]
    public byte[] PurchaseDate { get; set; } = null!;


    [Display(Name = "Користувач")]
    [Required(ErrorMessage = "Це поле не може бути порожнім!")]
    public int UserId { get; set; }


    [Display(Name = "Категорія авіарейсу")]
    [Required(ErrorMessage = "Це поле не може бути порожнім!")]
    public int CategoriesFlightsId { get; set; }

    [Display(Name = "Категорія")]
    [Required(ErrorMessage = "Оберіть принаймні одну категорію!")]
    public virtual CategoriesFlight CategoriesFlights { get; set; } = null!;

    [Display(Name = "Користувач")]
    [Required(ErrorMessage = "Це поле не може бути порожнім!")]
    public virtual User User { get; set; } = null!;

}
