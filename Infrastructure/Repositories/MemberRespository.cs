using Application.Interfaces.Repositories;
using Application.Responses.Member;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MemberRespository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRespository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<MembersResponse> GetByDepartmentIdAsync(int departmentId)
        {
           var members = await PlannerContext.DepartmentMembers.AsNoTracking()
                 .Where(dm => dm.DepartmentId == departmentId)
                 .Select(dm => new MemberItem
                 {
                     IdDepartMember = dm.Id,
                     Name = dm.Member.Name + " " + dm.Member.LastName,
                     NickName = dm.NickName!,
                     Sex = dm.Member.Sexe,
                     Status = dm.Status.ToString(),
                     Postes = dm.DepartmentMemberPosts.Select(p => p.Poste.Name)
                     .ToList()
                 }).ToListAsync(); 

            return new MembersResponse
            {
                Members = members
            };
        }

        /// <summary>
        ///     Récupère le profil complet d'un membre avec ses informations personnelles,
        ///     son compte utilisateur (email, téléphone) et ses départements.
        /// </summary>
        /// <param name="memberId">Identifiant du membre.</param>
        /// <returns>Le profil du membre ou null s'il n'existe pas.</returns>
        public async Task<ProfileResponse?> GetProfileAsync(Guid memberId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(m => m.Id == memberId)
                .Select(m => new ProfileResponse
                {
                    MemberId = m.Id,
                    Name = m.Name,
                    LastName = m.LastName,
                    Sexe = m.Sexe,
                    Email = m.User != null ? m.User.Email : null,
                    PhoneNumber = m.User != null ? m.User.PhoneNumber : null,
                    City = m.City,
                    Quarter = m.Quarter,
                    BirthDate = m.BirthDate.HasValue ? m.BirthDate.Value.ToString("MM-dd") : null,
                    EntryDate = m.EntryDate,
                    Departments = m.DepartmentMembers.Select(dm => new ProfileDepartmentResponse
                    {
                        DepartmentId = dm.DepartmentId,
                        DepartmentName = dm.Department.Name,
                        MinistryName = dm.Department.Ministry != null ? dm.Department.Ministry.Name : null
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Récupère un membre par son identifiant Guid.
        /// </summary>
        public async Task<Member?> GetByGuidAsync(Guid memberId)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Id == memberId);
        }

        /// <summary>
        ///     Récupère les membres dont l'anniversaire tombe dans le mois donné,
        ///     filtrés par les départements spécifiés.
        /// </summary>
        public async Task<List<BirthdayResponse>> GetBirthdaysByMonthAsync(List<int> departmentIds, int month)
        {
            var raw = await PlannerContext.DepartmentMembers
                .AsNoTracking()
                .Where(dm => departmentIds.Contains(dm.DepartmentId)
                    && dm.Member.BirthDate.HasValue
                    && dm.Member.BirthDate.Value.Month == month)
                .Select(dm => new
                {
                    dm.MemberId,
                    DisplayName = dm.Member.Name + " " + (dm.Member.LastName != null ? dm.Member.LastName.Substring(0, 1) + "." : ""),
                    BirthDate = dm.Member.BirthDate!.Value,
                    DepartmentName = dm.Department.Name
                })
                .ToListAsync();

            // Dédupliquer par membre, concaténer les départements
            return raw
                .GroupBy(r => r.MemberId)
                .Select(g => new BirthdayResponse
                {
                    DisplayName = g.First().DisplayName,
                    BirthDate = g.First().BirthDate.ToString("MM-dd"),
                    Day = g.First().BirthDate.Day,
                    DepartmentName = string.Join(", ", g.Select(x => x.DepartmentName).Distinct())
                })
                .OrderBy(b => b.Day)
                .ToList();
        }
    }
}
