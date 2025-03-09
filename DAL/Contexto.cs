using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using Ronalfy_Jimenez_P2_Ap1.Models;

namespace Ronalfy_Jimenez_P2_Ap1.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

    }
}
