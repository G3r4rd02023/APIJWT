namespace ProyectoApi.Frontend.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public decimal Precio { get; set; }
    }
}