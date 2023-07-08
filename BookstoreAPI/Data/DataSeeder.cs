//using BookstoreAPI.Data;
//using System.IO;
//using System.Globalization;
//using CsvHelper;
//using System.Formats.Asn1;

//public class DataSeeder
//{
//    private readonly ApplicationDbContext _dbContext;

//    public DataSeeder(ApplicationDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public void SeedData()
//    {
//        using (var reader = new StreamReader("Data/BX-Books.csv"))
//        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//        {
//            try
//            {
//                var records = csv.GetRecords<dynamic>();

//                foreach (var record in records)
//                {
//                    var book = new Book
//                    {
//                        ISBN = record.ISBN,
//                        BookTitle = record.BookTitle,
//                        BookAuthor = record.BookAuthor,
//                        YearOfPublication = Convert.ToInt32(record.YearOfPublication),
//                        Publisher = record.Publisher
//                    };

//                    _dbContext.Books.Add(book);
//                }

//                _dbContext.SaveChanges();
//            }
//            catch (CsvHelperException ex)
//            {
//                // Handle any CsvHelper specific exceptions here
//                Console.WriteLine($"CsvHelper exception: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                // Handle other exceptions that might occur during the data seeding process
//                Console.WriteLine($"Exception occurred: {ex.Message}");
//            }
//        }
//    }
//}

