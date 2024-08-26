using ProyectoApi.Frontend.Models;

namespace ProyectoApi.Frontend.Services
{
    public interface IServicioApi
    {
        Task<List<Producto>> Lista();

        Task<Producto> Obtener(int idProducto);

        Task<bool> Guardar(Producto objeto);

        Task<bool> Editar(Producto objeto);

        Task<bool> Eliminar(int idProducto);
    }
}