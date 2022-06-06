using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProcessingApp.Persistence.Repositories
{
    public class EmailDataRepository : BaseRepository<EmailData>, IEmailDataRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailDataRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EmailData>> GetRangeByEmailAddressAsync(string email, DateTime from, DateTime until)
        {
            var emailDataCollection = _context.EmailData.Where(x => x.Email == email);

            emailDataCollection = emailDataCollection.Where(x => x.CreationDate.Date >= from.Date && x.CreationDate.Date <= until.Date);

            var result = await emailDataCollection.ToListAsync();

            return result;
        }

        public async Task<bool> ValidateAttributeListAsync(string email, List<string> attributes)
        {
            var emailDataList = await _context.EmailData
                .Where(x => x.Email == email && x.CreationDate.Date == DateTime.Now.Date)
                .ToListAsync();

            foreach(var emailData in emailDataList)
            {
                if(emailData.Attributes.Split(",").Any(e => attributes.Contains(e)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
