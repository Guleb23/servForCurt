using ApiForSud.DTOs;

namespace ApiForSud.Services.CurtService
{
    public interface ICurtService
    {
        public Task<bool> CreateCurtService(CurtDTO curtDTO);

        public Task<List<CurtDTO>> GetAllCurts();
    }
}
