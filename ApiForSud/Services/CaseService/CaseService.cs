using ApiForSud.Data;
using ApiForSud.DTOs;
using ApiForSud.Migrations;
using ApiForSud.Models.DatabaseModels;
using ApiForSud.Services.CurtInstanceService;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ApiForSud.Services.CaseService
{
    public class CaseService : ICaseService
    {
        private readonly ICurtInstanceService _curtInstanceService;
        private readonly ApplicationDBContext _dbContext;
        public CaseService(ApplicationDBContext dBContext, ICurtInstanceService curtInstanceService)
        {
            _dbContext = dBContext;
            _curtInstanceService = curtInstanceService;
        }

        public async Task<CaseDTO> CreateCase(CaseDTO caseDTO, Guid userId)
        {
            var userExists = await _dbContext.Users
                .AnyAsync(u => u.Id == userId);

            if (!userExists)
                throw new Exception("Пользователь не найден");

            if (string.IsNullOrWhiteSpace(caseDTO.NameOfCurt))
                throw new Exception("Название суда не передано");

            var curt = await _dbContext.Curts
                .FirstOrDefaultAsync(c => c.Name == caseDTO.NameOfCurt);

            if (curt == null)
                throw new Exception($"Суд '{caseDTO.NameOfCurt}' не найден");

            var newCase = new Case
            {
                Id = Guid.NewGuid(),
                NomerOfCase = caseDTO.NomerOfCase,
                Applicant = caseDTO.Applicant,
                Defendant = caseDTO.Defendant,
                Reason = caseDTO.Reason,
                DateOfCurt = caseDTO.DateOfCurt,
                ResultOfCurt = caseDTO.ResultOfCurt,
                DateOfResult = caseDTO.DateOfResult,
                Third = caseDTO.Third,
                Notes = caseDTO.Notes,
                UserId = userId,
                CurtId = curt.Id
            };

            _dbContext.Cases.Add(newCase);
            await _dbContext.SaveChangesAsync();

            var caseDto = new CaseDTO()
            {
                Id = newCase.Id,
                NomerOfCase = newCase.NomerOfCase,
                Applicant = newCase.Applicant,
                Defendant = newCase.Defendant,
                Reason = newCase.Reason,
                DateOfCurt = newCase.DateOfCurt,
                ResultOfCurt = newCase.ResultOfCurt,
                DateOfResult = newCase.DateOfResult,
            };

            return caseDto;
        }

        public async Task<List<CaseUserDto>> GetAllCases()
        {
            return await _dbContext.Cases.Where(c => c.IsArhcived == false).Include(c => c.User).Select(c => new CaseUserDto
            {
                Id = c.Id,
                NomerOfCase = c.NomerOfCase,
                Curt = c.Curt,
                Applicant = c.Applicant,
                Defendant = c.Defendant,
                Reason = c.Reason,
                DateOfCurt = c.DateOfCurt,
                CreatedDate = c.CreatedDate,
                ResultOfCurt= c.ResultOfCurt,
                DateOfResult= c.DateOfResult,
                IsMarkeredByAdmin = c.IsMarkeredByAdmin,
                IsUnMarkeredByAdmin = c.IsUnMarkeredByAdmin,
                IsNotificated = c.IsNotificated,
                IsArhcived = c.IsArhcived,
                ArchivedDate = c.ArchivedDate,
                UserFio = c.User.FIO,
                Notes = c.Notes,
                Third = c.Third

            }).ToListAsync();
        }

        public async Task<CaseResponseDTO?> GetDetailCasesById( Guid userId, Guid caseId)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (currentUser.RoleId == 2)
            {
                return await _dbContext.Cases
                .Where(c => c.Id == caseId)
                .Select(c => new CaseResponseDTO
                {
                    Id = c.Id,
                    UserFIO = c.User.FIO,
                    NomerOfCase = c.NomerOfCase,
                    Notes = c.Notes,
                    Third = c.Third,
                    NameOfCurt = c.Curt.Name,
                    Applicant = c.Applicant,
                    Defendant = c.Defendant,
                    Reason = c.Reason,
                    DateOfCurt = c.DateOfCurt,
                    ResultOfCurt = c.ResultOfCurt,
                    DateOfResult = c.DateOfResult,
                    
                    CurtInstances = c.CurtInstances.OrderBy(i => i.DateOfResult).Select(ci => new CurtInstanceResponseDTO
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                        NameOfCurt = ci.NameOfCurt,
                        DateOfSession = ci.DateOfSession,
                        Link = ci.Link,
                        Employee = ci.Employee,
                        ResultOfIstance = ci.ResultOfIstance,
                        DateOfResult = ci.DateOfResult,
                        Report = ci.Report,
                        CreatedDate = ci.CreatedDate
                        
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            }

            else
            {
                return await _dbContext.Cases
                .Where(c => c.Id == caseId && c.UserId == userId)
                .Include(c => c.CurtInstances)
                .Select(c => new CaseResponseDTO
                {
                    Id = c.Id,
                    UserFIO = c.User.FIO,
                    NomerOfCase = c.NomerOfCase,
                    NameOfCurt = c.Curt.Name,
                    Applicant = c.Applicant,
                    Defendant = c.Defendant,
                    Notes = c.Notes,
                    Third = c.Third,
                    Reason = c.Reason,
                    DateOfCurt = c.DateOfCurt,
                    ResultOfCurt = c.ResultOfCurt,
                    DateOfResult = c.DateOfResult,
                    CurtInstances = c.CurtInstances.OrderBy(i => i.DateOfResult).Select(ci => new CurtInstanceResponseDTO
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                        NameOfCurt = ci.NameOfCurt,
                        DateOfSession = ci.DateOfSession,
                        Link = ci.Link,
                        Employee = ci.Employee,
                        ResultOfIstance = ci.ResultOfIstance,
                        DateOfResult = ci.DateOfResult,
                        Report = ci.Report,
                        CreatedDate = ci.CreatedDate
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            }
            
        }

        public async Task<List<CaseWtihCurt>> GetCasesById(Guid userId)
        {
            return await _dbContext.Cases.Where(c => c.UserId == userId && c.IsArhcived == false).Select(c => new CaseWtihCurt()
            {
                Id = c.Id,
                UserFIO = c.User.FIO,
                Applicant = c.Applicant,
                Defendant = c.Defendant,
                DateOfCurt = c.DateOfCurt,
                ArchivedDate = c.ArchivedDate,
                CreatedDate = c.CreatedDate,
                IsMarkeredByAdmin = c.IsMarkeredByAdmin,
                IsNotificated = c.IsNotificated,
                IsUnMarkeredByAdmin = c.IsUnMarkeredByAdmin,
                UserId = userId,
                Reason = c.Reason,
                ThirdParties = c.Third,
                Notes = c.Notes,
                Curt = new CurtDTO()
                {
                    Id = c.Curt.Id,
                    CurtName = c.Curt.Name,
                },
                NomerOfCase = c.NomerOfCase,
                DateOfResult = c.DateOfResult,
                ResultOfCurt = c.ResultOfCurt,
                IsArhcived = c.IsArhcived,
                Instances = c.CurtInstances.OrderBy(i => i.DateOfResult)
                .Select(i => new ShortInstance()
                {
                    DateOfResult = i.DateOfResult,
                    ResultOfIstance = i.ResultOfIstance,
                    Report = i.Report
                }).ToList(),
            }).ToListAsync();
        }

        public async Task<bool> DeleteCase(Guid caseId, Guid userId)
        {
            var caseToDelete = await _dbContext.Cases
                .FirstOrDefaultAsync(c => c.Id == caseId && c.UserId == userId);

            if (caseToDelete == null)
            {
                return false;
            }

            _dbContext.Cases.Remove(caseToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CaseResponseDTO?> UpdateCase(CaseDTO caseDTO, Guid userId, Guid caseId, List<CurtInstaceDTO> curtInstaces)
        {
            var caseToUpdate = await _dbContext.Cases
                .Include(c => c.CurtInstances)
                .FirstOrDefaultAsync(c => c.Id == caseId && c.UserId == userId);

            if (caseToUpdate == null)
                return null;

            var curt = await _dbContext.Curts
                .FirstOrDefaultAsync(c => c.Name.ToLower() == caseDTO.NameOfCurt.ToLower());

            caseToUpdate.NomerOfCase = caseDTO.NomerOfCase;
            caseToUpdate.Curt = curt;
            caseToUpdate.Applicant = caseDTO.Applicant;
            caseToUpdate.Defendant = caseDTO.Defendant;
            caseToUpdate.Reason = caseDTO.Reason;
            caseToUpdate.DateOfCurt = caseDTO.DateOfCurt;
            caseToUpdate.ResultOfCurt = caseDTO.ResultOfCurt;
            caseToUpdate.DateOfResult = caseDTO.DateOfResult;
            caseToUpdate.Third = caseDTO.Third;
            caseToUpdate.Notes = caseDTO.Notes;

            if (caseToUpdate.CurtInstances.Any())
            {
                _dbContext.CurtInstances.RemoveRange(caseToUpdate.CurtInstances);
            }

            if (curtInstaces != null && curtInstaces.Count > 0)
            {
                var newInstances = curtInstaces.Select(instanceDto => new CurtInstance
                {
                    Id = Guid.NewGuid(),
                    Name = instanceDto.Name,
                    NameOfCurt = instanceDto.NameOfCurt,
                    DateOfSession = instanceDto.DateOfSession,
                    Link = instanceDto.Link,
                    Employee = instanceDto.Employee,
                    ResultOfIstance = instanceDto.ResultOfIstance,
                    DateOfResult = instanceDto.DateOfResult,
                    Report = instanceDto.Report,
                    CaseId = caseId
                }).ToList();

                _dbContext.CurtInstances.AddRange(newInstances);
            }

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cases
                .Where(c => c.Id == caseId)
                .Select(c => new CaseResponseDTO
                {
                    Id = c.Id,
                    NomerOfCase = c.NomerOfCase,
                    NameOfCurt = c.Curt.Name,
                    Applicant = c.Applicant,
                    Defendant = c.Defendant,
                    Reason = c.Reason,
                    DateOfCurt = c.DateOfCurt,
                    ResultOfCurt = c.ResultOfCurt,
                    DateOfResult = c.DateOfResult,
                    CurtInstances = c.CurtInstances.Select(ci => new CurtInstanceResponseDTO
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                        NameOfCurt = ci.NameOfCurt,
                        DateOfSession = ci.DateOfSession,
                        Link = ci.Link,
                        Employee = ci.Employee,
                        ResultOfIstance = ci.ResultOfIstance,
                        DateOfResult = ci.DateOfResult,
                    
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MarkerByAdmin(Guid caseId)
        {
            var currentCase = await _dbContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);

            if (currentCase == null) 
            {
                return false;
            }
            else
            {
                currentCase.IsMarkeredByAdmin = true;
                currentCase.IsUnMarkeredByAdmin = false;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> UnMarkerByAdmin(Guid caseId)
        {
            var currentCase = await _dbContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);

            if (currentCase == null)
            {
                return false;
            }
            else
            {
                currentCase.IsMarkeredByAdmin = false;
                currentCase.IsUnMarkeredByAdmin = true;
                await _dbContext.SaveChangesAsync();

                return true;
            }

        }

        public async Task<List<Case>> GatArchiveCases()
        {
            return await _dbContext.Cases.Where(c => c.IsArhcived == true).ToListAsync();
        }

        public async Task<List<Case>> GatArchiveCasesById(Guid userId)
        {
            return await _dbContext.Cases.Where(c => c.UserId == userId && c.IsArhcived == true).ToListAsync();
        }

        public async Task<bool> Archive(Guid caseId)
        {
            var currentCase = await _dbContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);

            if (currentCase == null)
            {
                return false;
            }
            else
            {
                currentCase.IsArhcived = true;
                currentCase.ArchivedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> UnArchive(Guid caseId)
        {
            var currentCase = await _dbContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);

            if (currentCase == null)
            {
                return false;
            }
            else
            {
                currentCase.IsArhcived = false;
                currentCase.ArchivedDate = null;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }
    }
}
