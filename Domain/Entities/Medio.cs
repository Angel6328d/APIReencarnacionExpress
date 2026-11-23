namespace Domain.Entities
{
    public class Medio
    {
        public int Id { get; set; }
        public TipoEntidad Entidad { get; set; }
        public int EntidadId { get; set; }
        public TipoMedio Tipo { get; set; }
        public string Url { get; set; } = "";
        public string Ruta { get; set; } = "";   // ruta dentro del bucket, para poder borrarlo
        public int Orden { get; set; }
    }

    public enum TipoEntidad
    {
        Enemigo,
        Historia,
        Personaje,
        Region,
        Vestigio
    }

    public enum TipoMedio
    {
        Imagen = 0,
        Video = 1,
        Icono = 2   // miniatura / retrato pequeño (mismos formatos que Imagen)
    }
}