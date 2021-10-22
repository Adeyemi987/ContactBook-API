using ContactBook.Model;
using System.Threading.Tasks;

namespace ContactBook.BL
{
    public interface ITokenGenerator
    {
       Task <string> GenerateToken(User user);
    }
}