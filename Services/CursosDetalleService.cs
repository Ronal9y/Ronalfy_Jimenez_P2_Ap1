using Ronalfy_Jimenez_P2_Ap1.DAL;
using Ronalfy_Jimenez_P2_Ap1.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Ronalfy_Jimenez_P2_Ap1.Services;

public class CursosDetalleService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }


    public async Task<bool> Eliminar(int cursosId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var detalle = await contexto.Cursos
                .FirstOrDefaultAsync(d => d.CursosId == cursosId);
            return false;
        }

        public async Task AfectarMonto(CursosDetalle detalle, bool Suma)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var encuesta = await contexto.Cursos
                .FirstOrDefaultAsync(e => e.CursosId == detalle.CursosId);

            if (encuesta != null)
            {
                if (Suma)
                {
                    encuesta.Monto -= detalle.Monto;
                }
                else
                {
                    encuesta.Monto += detalle.Monto;
                }

                await contexto.SaveChangesAsync();
            }
        }
}