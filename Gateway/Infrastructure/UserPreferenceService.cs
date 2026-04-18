using Gateway.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Infrastructure;

public class UserPreferenceService : IUserPreferenceService
{
    private readonly ApplicationDbContext _db;

    public UserPreferenceService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UserPreference?> GetByUserIdAsync(string userId)
    {
        return await _db.UserPreferences
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }

}