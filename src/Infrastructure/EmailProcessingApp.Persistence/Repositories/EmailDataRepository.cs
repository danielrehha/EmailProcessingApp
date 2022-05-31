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

        public async Task<bool> IsAttributeListUnique(EmailDataDto dto)
        {
            var emailDataList = await _context.EmailData
                .Where(x => x.Email == dto.Email && x.CreationDate.Date == DateTime.Now.Date)
                .ToListAsync();

            foreach(var email in emailDataList)
            {
                if(email.Attributes.Split(",").Any(e => dto.Attributes.Contains(e)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
