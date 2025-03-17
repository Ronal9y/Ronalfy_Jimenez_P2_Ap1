using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.Models;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using System.Linq.Expressions;

namespace Ronalfy_Jimenez_P2_Ap1.Services
{
    public class CursoService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public CursoService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        private async Task<bool> Insertar(Cursos curso)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Cursos.Add(curso);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cursos curso)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Cursos.Update(curso);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            var curso = await contexto.Cursos
                .Include(c => c.CursosDetalle)
                .FirstOrDefaultAsync(c => c.CursosId == id);

            if (curso == null) return false;

            foreach (var detalle in curso.CursosDetalle)
            {
                var ciudad = await contexto.Ciudades
                    .FirstOrDefaultAsync(c => c.CiudadId == detalle.CiudadId);

                if (ciudad != null)
                {
                    ciudad.MontoBase -= detalle.Monto;
                    contexto.Update(ciudad);
                }
            }

            contexto.Cursos.Remove(curso);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int cursoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Cursos.AnyAsync(c => c.CursosId == cursoId);
        }

        public async Task<bool> Guardar(Cursos curso)
        {
            if (!await Existe(curso.CursosId))
                return await Insertar(curso);
            else
                return await Modificar(curso);
        }

        public async Task<Cursos?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Cursos
                .Include(c => c.CursosDetalle)
                .ThenInclude(d => d.Ciudad)
                .FirstOrDefaultAsync(c => c.CursosId == id);
        }

        public async Task<List<Cursos>> Listar(Expression<Func<Cursos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Cursos
                .Include(c => c.CursosDetalle) // Incluir detalles
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

    }
}
