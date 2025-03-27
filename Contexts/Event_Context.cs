using Microsoft.EntityFrameworkCore;
using webapi.event_.Domains;

namespace webapi.event_.Contexts
{
    public class Event_Context : DbContext
    {
        public Event_Context()
        {
        }
        public Event_Context(DbContextOptions<Event_Context> options)
            : base(options)
        {
        }

        public DbSet<TiposUsuarios> TiposUsuarios { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<TiposEventos> TiposEventos { get; set; }

        public DbSet<Eventos> Eventos { get; set; }

        public DbSet<ComentariosEventos> ComentariosEventos { get; set; }

        public DbSet<Instituicoes> Instituicoes { get; set; }

        public DbSet<PresencasEventos> PresencasEventos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server =NOTE12-S28\\SQLEXPRESS; Database = webapi.event+; User Id = sa; Pwd = Senai@134; TrustServerCertificate=true;");
            }
        }
    }
}