namespace ProyectoApi.Frontend.Models
{
    public class ResultadoApi
    {
        public string Mensaje { get; set; } = null!;
        public List<Producto>? Lista { get; set; }
        public Producto? Objeto { get; set; }
    }
}