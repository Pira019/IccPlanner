using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Cette classe permet de faire des operations dans la base des donnes
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly  IccPlannerContext _context;

        public UserRepository(IccPlannerContext context)
        {
            _context = context;
        }


        // Ajouter un utilisateur
        public void insert(User user)
        {  
            _context.Users.Add(user);
            _context.SaveChanges();  
        }
    }
}
