using System;
using System.Threading.Tasks;
using TruyenAnime.Data; 
using TruyenAnime.Interfaces; 
using TruyenAnime.Models; 

namespace TruyenAnime.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public ContactRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public void Add(ContactMessage message)
        {
            _context.ContactMessages.Add(message);
        }

        public IEnumerable<ContactMessage> GetAll()
        {
            return _context.ContactMessages.OrderByDescending(m => m.SentDate).ToList();
        }

        public ContactMessage GetById(int id)
        {
            return _context.ContactMessages.Find(id);
        }

        public void Delete(int id)
        {
            var message = GetById(id);
            if (message != null)
                _context.ContactMessages.Remove(message);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}