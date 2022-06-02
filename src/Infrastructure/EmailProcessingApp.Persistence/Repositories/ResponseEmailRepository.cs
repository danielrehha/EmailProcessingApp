using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProcessingApp.Persistence.Repositories
{
    public class ResponseEmailRepository : BaseRepository<ResponseEmail>, IResponseEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public ResponseEmailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ResponseEmail> FindByEmailAndDateAsync(string email, DateTime date)
        {
            var result = await _context.ResponseEmail
                .FirstOrDefaultAsync(e => e.Email == email && e.CreationDate.Date == date);

            return result;
        }
    }
}
