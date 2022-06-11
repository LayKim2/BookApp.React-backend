using Dul.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    /// <summary>
    /// [6] Repository class : ADO.NET or Dapper or Entity Framework Core
    /// </summary>
    public class HaruUserRepository : IHaruUserRepository
    {
        private readonly BookAppDbContext _context;
        private readonly ILogger _logger;

        public HaruUserRepository(BookAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(HaruUserRepository));
        }

        public async Task<HaruUser> AddAsync(HaruUser model)
        {
            try
            {
                _context.HaruUsers.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger?.LogError($"ERROR({nameof(AddAsync)}): {e.Message}");
            }

            return model;
        }
        public async Task<HaruUser> GetHaruUser(string username)
        {
            HaruUser model = await _context.HaruUsers.SingleOrDefaultAsync(m => m.UserID == username);

            return model;
        }

        public Task<HaruUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HaruUser>> GetAllAsync()
        {
            return await _context.HaruUsers.OrderByDescending(m => m.UserID)
                .ToListAsync();
        }

        public Task<ArticleSet<HaruUser, int>> GetArticlesAsync<TParentIdentifier>(int pageIndex, int pageSize, string searchField, string searchQuery, string sortOrder, TParentIdentifier parentIdentifier)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> UpdateAsync(HaruUser model)
        {
            throw new NotImplementedException();
        }

    }
}
