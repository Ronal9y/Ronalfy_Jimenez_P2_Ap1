using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.Models;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using System.Linq.Expressions;


namespace Ronalfy_Jimenez_P2_Ap1.Services;

public class CiudadService(IDbContextFactory<Contexto> DbFactory)
{

    private async Task<bool> Insertar(Ciudades ciudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Add(ciudades);

        return await contexto.SaveChangesAsync() > 0;

    }
    private async Task<bool> Modificar(Ciudades ciudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(ciudades);

        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var EliminarCiudad = await contexto.Ciudades
            .Where(t => t.CiudadId == id).ExecuteDeleteAsync();

        return EliminarCiudad > 0;
    }
    public async Task<bool> Existe(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Ciudades.AnyAsync(t => t.CiudadId == ciudadId);
    }
    public async Task<bool> Guardar(Ciudades ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (ciudad.CiudadId == 0)
        {
            contexto.Ciudades.Add(ciudad);
        }
        else
        {
            contexto.Update(ciudad);
        }

        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Ciudades?> Buscar(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .FirstOrDefaultAsync(c => c.CiudadId == ciudadId);
    }
    public async Task<bool> ExisteNombreCliente(string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AnyAsync(c =>
            (c.Nombre == nombre) &&
            c.CiudadId != id
        );
    }
    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Ciudades.AsNoTracking().Where(criterio).ToListAsync();

    }
    public async Task<List<Ciudades>> ListarCiudades()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AsNoTracking().ToListAsync();
    }

    public async Task<double> ObtenerTotalMontos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.SumAsync(m => m.MontoBase);
    }
}