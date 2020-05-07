using OpenLibrary.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckAuthorsAsync();
            await CheckGendersAsync();
            await CheckTypeOfDocumentAsync();
            await CheckDocumentLanguageAsync();
            await CheckDocumentsAsync();
        }
        private async Task CheckAuthorsAsync()
        {
            if (!_context.Authors.Any())
            {
                AddAuthor("Gabriel Garcia Marquez");
                AddAuthor("Charles Dickens");
                AddAuthor("Arthur Conan Doyle");
                AddAuthor("Franz Kafka");
                AddAuthor("Julio Verne");
                AddAuthor("Oscar Wilde");

                await _context.SaveChangesAsync();
            }
        }

        private void AddAuthor(string name)
        {
            _context.Authors.Add(new AuthorEntity { Name = name });
        }

        private async Task CheckGendersAsync()
        {
            if (!_context.Genders.Any())
            {
                AddGender("Science fiction");
                AddGender("Biography");
                AddGender("Fantasy");
                AddGender("Legend");
                AddGender("Journalism");
                AddGender("British literature");
                AddGender("Gothic");

                await _context.SaveChangesAsync();
            }
        }

        private void AddGender(string name)
        {
            _context.Genders.Add(new GenderEntity { Name = name });
        }

        private async Task CheckTypeOfDocumentAsync()
        {
            if (!_context.TypeOfDocuments.Any())
            {
                AddTypeOfDocument("Thesis");
                AddTypeOfDocument("Report");
                AddTypeOfDocument("Assay");
                AddTypeOfDocument("Novel");
                AddTypeOfDocument("Encyclopedia");
                AddTypeOfDocument("Fable");

                await _context.SaveChangesAsync();
            }
        }

        private void AddTypeOfDocument(string name)
        {
            _context.TypeOfDocuments.Add(new TypeOfDocumentEntity { Name = name });
        }

        private async Task CheckDocumentLanguageAsync()
        {
            if (!_context.DocumentLanguages.Any())
            {
                AddDocumentLanguages("Spanish");
                AddDocumentLanguages("English");
                AddDocumentLanguages("German");
                AddDocumentLanguages("Japanese");
                AddDocumentLanguages("Chinese");
                AddDocumentLanguages("Italian");

                await _context.SaveChangesAsync();
            }
        }

        private void AddDocumentLanguages(string name)
        {
            _context.DocumentLanguages.Add(new DocumentLanguageEntity { Name = name });
        }

        private async Task CheckDocumentsAsync()
        {
            if (!_context.Documents.Any())
            {
                DateTime Date = DateTime.Today.AddMonths(2).ToUniversalTime();
                _context.Documents.Add(new DocumentEntity
                {
                    Date = Date,
                    Title = "100 years of solitude",
                    DocumentPath = $"~/Documents/OPI72-taller6.pdf",
                    Summary = "Plot Overview. One Hundred Years of Solitude is the history of the isolated town of Macondo and of the family who founds it, the Buendías. For years, the town has no contact with the outside world, except for gypsies who occasionally visit, peddling technologies like ice and telescopes",
                    PagesNumber = 422,
                    Gender = _context.Genders.FirstOrDefault(t => t.Name == "Fantasy"),
                    Author = _context.Authors.FirstOrDefault(t => t.Name == "Gabriel Garcia Marquez"),
                    DocumentLanguage = _context.DocumentLanguages.FirstOrDefault(t => t.Name == "Spanish"),
                    TypeOfDocument = _context.TypeOfDocuments.FirstOrDefault(t => t.Name == "Novel")
                });
                Date = DateTime.Today.AddMonths(1).ToUniversalTime();
                _context.Documents.Add(new DocumentEntity
                {
                    Date = Date,
                    Title = "The Picture of Dorian Gray",
                    DocumentPath = $"~/Documents/OPI72-taller7.pdf",
                    Summary = "Summaries. A corrupt young man somehow keeps his youthful beauty, but a special painting gradually reveals his inner ugliness to all. In 1886, in Victorian London, the corrupt Lord Henry Wotton meets the pure Dorian Gray (Hurd Hatfield) posing for talented painter Basil Hallward (Lowell Gilmore)",
                    PagesNumber = 224,
                    Gender = _context.Genders.FirstOrDefault(t => t.Name == "Gothic"),
                    Author = _context.Authors.FirstOrDefault(t => t.Name == "Oscar Wilde"),
                    DocumentLanguage = _context.DocumentLanguages.FirstOrDefault(t => t.Name == "English"),
                    TypeOfDocument = _context.TypeOfDocuments.FirstOrDefault(t => t.Name == "Novel")
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
