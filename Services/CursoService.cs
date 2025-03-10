using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.Models;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using System.Linq.Expressions;

namespace Ronalfy_Jimenez_P2_Ap1.Services
{
    public class CursoService(IDbContextFactory<Contexto> DbFactory)
    {

        private async Task<bool> Insertar(Cursos cursos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Cursos.Add(cursos);

            return await contexto.SaveChangesAsync() > 0;

        }
        private async Task<bool> Modificar(Cursos cursos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            contexto.Update(cursos);

            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            var EliminarCurso = await contexto.Cursos
                .Where(t => t.CursosId == id).ExecuteDeleteAsync();

            return EliminarCurso > 0;
        }
        public async Task<bool> Existe(int cursosId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            return await contexto.Cursos.AnyAsync(t => t.CursosId == cursosId);
        }
        public async Task<bool> Guardar(Cursos cursos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            if (!await Existe(cursos.CursosId))
            {
                return await Insertar(cursos);
            }
            else
            {
                return await Modificar(cursos);
            }
        }
        public async Task<Cursos?> Buscar(int id = 0, string? asignatura = null)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cursos
                .Where(t => t.CursosId == id || t.Asignatura == asignatura)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> ExisteNombreCliente(string asignatura, int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cursos.AnyAsync(c =>
                (c.Asignatura == asignatura) &&
                c.CursosId != id
            );
        }
        public async Task<List<Cursos>> Listar(Expression<Func<Cursos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            return await contexto.Cursos.AsNoTracking().Where(criterio).ToListAsync();

        }
        public async Task<List<Cursos>> ListarCiudades()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cursos.AsNoTracking().ToListAsync();
        }

        public async Task<double> ObtenerTotalMontos()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cursos.SumAsync((Expression<Func<Cursos, double>>)(m => m.Monto));
        }

    }
}
