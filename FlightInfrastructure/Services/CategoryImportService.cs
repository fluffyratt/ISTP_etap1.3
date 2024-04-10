using ClosedXML.Excel;
using FlightDomain.Model;
using Microsoft.EntityFrameworkCore;


/*namespace FlightInfrastructure.Services
{
    public class CategoryImportService : IImportService<Category>
    {
        private readonly DbflightsContext _context;

        public CategoryImportService(DbflightsContext context)
        {
            _context = context;
        }

        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanRead)
            {
                throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
            }

            using (XLWorkbook workbook = new XLWorkbook(stream))
            {
                foreach (IXLWorksheet worksheet in workbook.Worksheets)
                {
                    var categoryName = worksheet.Name;

                    // Перевіряємо наявність категорії
                    var category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Name == categoryName, cancellationToken);

                    if (category == null)
                    {
                        // Створюємо категорію, якщо її немає
                        category = new Category { Name = categoryName };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    foreach (var row in worksheet.RowsUsed().Skip(1)) // Пропускаємо заголовок
                    {
                        var ticketDate = GetDateTimeFromCell(row, 1);
                        var userName = GetStringFromCell(row, 2);
                        var userSurname = GetStringFromCell(row, 3);
                        var flightName = GetStringFromCell(row, 4);
                        var seatsNumber = GetStringFromCell(row, 5);
                        var price = GetPriceFromCell(row, 6);

                        // Перевіряємо наявність користувача
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName && u.Surname == userSurname, cancellationToken);

                        if (user == null)
                        {
                            // Викидаємо помилку, якщо користувача з таким ім'ям та прізвищем немає в базі даних
                            throw new ArgumentException($"Користувача з ім'ям {userName} та прізвищем {userSurname} не знайдено в базі даних.");
                        }

                        // Перевіряємо наявність політу
                        var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Name == flightName, cancellationToken);

                        if (flight == null)
                        {
                            throw new ArgumentException($"Політ з назвою {flightName} не знайдено в базі даних.");
                        }

                        // Створюємо об'єкт категорій польоту для квитка
                        var categoriesFlight = new CategoriesFlight 
                        { 
                            Category = category, 
                            Flight = flight,
                        };

                        // Створюємо квиток і додаємо його до бази даних
                        var ticket = new Ticket
                        {
                            PurchaseDate = ticketDate,
                            UserId = user.Id,
                            CategoriesFlights = categoriesFlight
                        };
                        _context.Tickets.Add(ticket);
                    }
                }

                // Зберігаємо всі зміни в базі даних
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private decimal GetPriceFromCell(IXLRow row, int columnIndex)
        {
            string priceString = row.Cell(columnIndex).Value.ToString();
            if (decimal.TryParse(priceString, out decimal price))
            {
                return price;
            }
            else
            {
                // Обробляємо невірний формат ціни, наразі просто повертаємо 0
                return 0;
            }
        }

        private DateTime GetDateTimeFromCell(IXLRow row, int columnIndex)
        {
            string dateString = row.Cell(columnIndex).Value.ToString();
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date;
            }
            else
            {
                // Обробляємо невірний формат дати, наразі просто повертаємо DateTime.MinValue
                return DateTime.MinValue;
            }
        }

        private string GetStringFromCell(IXLRow row, int columnIndex)
        {
            return row.Cell(columnIndex).Value.ToString();
        }

        private TimeSpan GetDurationFromCell(IXLRow row, int columnIndex)
        {
            string durationString = row.Cell(columnIndex).Value.ToString();
            if (double.TryParse(durationString, out double durationMinutes))
            {
                return TimeSpan.FromMinutes(durationMinutes);
            }
            else
            {
                // Обробляємо невірний формат тривалості, наразі просто повертаємо TimeSpan.Zero
                return TimeSpan.Zero;
            }
        }

    }
}
*/


