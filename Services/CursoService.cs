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
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            if (curso.CursosId == 0)
            {
                contexto.Cursos.Add(curso);
            }
            else
            {
                var existente = await contexto.Cursos
                    .Include(c => c.CursosDetalle)
                    .FirstOrDefaultAsync(c => c.CursosId == curso.CursosId);

                if (existente != null)
                {
                    foreach (var detalleExistente in existente.CursosDetalle.ToList())
                    {
                        var nuevoDetalle = curso.CursosDetalle
                            .FirstOrDefault(d => d.DetalleId == detalleExistente.DetalleId);

                        if (nuevoDetalle != null)
                        {
                            if (detalleExistente.Monto != nuevoDetalle.Monto)
                            {
                                var ciudad = await contexto.Ciudades
                                    .FindAsync(detalleExistente.CiudadId);

                                if (ciudad != null)
                                {
                                    ciudad.MontoBase -= detalleExistente.Monto;
                                    ciudad.MontoBase += nuevoDetalle.Monto;
                                    contexto.Update(ciudad);
                                }
                            }
                        }
                        else
                        {
                            var ciudad = await contexto.Ciudades
                                .FindAsync(detalleExistente.CiudadId);

                            if (ciudad != null)
                            {
                                ciudad.MontoBase -= detalleExistente.Monto;
                                contexto.Update(ciudad);
                            }
                            contexto.Remove(detalleExistente);
                        }
                    }

                    foreach (var nuevoDetalle in curso.CursosDetalle
                        .Where(d => d.DetalleId == 0))
                    {
                        existente.CursosDetalle.Add(nuevoDetalle);
                        var ciudad = await contexto.Ciudades
                            .FindAsync(nuevoDetalle.CiudadId);

                        if (ciudad != null)
                        {
                            ciudad.MontoBase += nuevoDetalle.Monto;
                            contexto.Update(ciudad);
                        }
                    }

                    existente.Fecha = curso.Fecha;
                    existente.Asignatura = curso.Asignatura;
                    existente.Monto = curso.CursosDetalle.Sum(d => d.Monto);
                }
            }

            return await contexto.SaveChangesAsync() > 0;
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
                .Include(c => c.CursosDetalle)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

    }
}
