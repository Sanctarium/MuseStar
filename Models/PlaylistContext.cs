using Microsoft.EntityFrameworkCore;
using MuseStar.Models;

namespace MuseStar
{
    
    public class PlaylistContext : DbContext
    {
        public PlaylistContext(DbContextOptions<PlaylistContext> options)
            : base(options)
            {
            }

        public DbSet<User> Users{get;set;}
        public DbSet<Song> Songs{get;set;}
    }
}