using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTOs
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]  //Que sea oblogatorio.
        [MaxLength(30)] //maximo de 30 caracteres.
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
