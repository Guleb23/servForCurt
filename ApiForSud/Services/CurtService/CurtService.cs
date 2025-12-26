using ApiForSud.Data;
using ApiForSud.DTOs;
using ApiForSud.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace ApiForSud.Services.CurtService
{
    public class CurtService : ICurtService
    {
        private readonly ApplicationDBContext _dbContext;

        public CurtService(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> CreateCurtService(CurtDTO curtDTO)
        {
            if (curtDTO == null)
            {
                return false;
            }
            try
            {
                var curt = new Curt()
                {
                    Name = curtDTO.CurtName
                };
                _dbContext.Curts.Add(curt);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<CurtDTO>> GetAllCurts()
        {
            return await _dbContext.Curts.Select(c => new CurtDTO() { CurtName = c.Name, Id = c.Id }).ToListAsync();
        }
    }
}
