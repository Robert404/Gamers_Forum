using Games_Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Data
{
    public interface IReply
    {
        Task Edit(PostReply reply);
        Task Delete(int id);
        PostReply GetById(int id);
    }
}
