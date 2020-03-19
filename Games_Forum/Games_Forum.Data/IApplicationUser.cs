using Games_Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task SetProfileImage(string id, Uri uri);
        Task IncrementRating(string id, Type type);
    }
}
