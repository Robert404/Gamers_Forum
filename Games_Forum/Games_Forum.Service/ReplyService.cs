using Games_Forum.Data;
using Games_Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Forum.Service
{
    public class ReplyService : IReply
    {
        private readonly ApplicationDbContext _context;
        public ReplyService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var reply = GetById(id);
            _context.Remove(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(PostReply reply)
        {
            _context.Update(reply);
            await _context.SaveChangesAsync();
        }

        public PostReply GetById(int id)
        {
            return _context.PostReplies.Where(r => r.Id == id)
                .Include(r => r.User).FirstOrDefault();
        }

    }
}
