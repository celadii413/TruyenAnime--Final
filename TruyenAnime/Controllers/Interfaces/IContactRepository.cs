using System.Threading.Tasks;
using TruyenAnime.Models; 

namespace TruyenAnime.Interfaces
{
    public interface IContactRepository
    {
        void Add(ContactMessage message);
        IEnumerable<ContactMessage> GetAll();
        void Delete(int id);
        ContactMessage GetById(int id);
        void Save();
    }
}