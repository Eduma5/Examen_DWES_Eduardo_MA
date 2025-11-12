using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoCRUD2.Models.Entity
{
    [Table("venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("producto")]
        [Required]
        [StringLength(50)]
        public string? Producto { get; set; }

        [Column("cantidad")]
        [Required]
        public int Cantidad { get; set; }

        [Column("precio")]
        [Required]
        public decimal Precio { get; set; }

        [NotMapped]
        public decimal Subtotal => Cantidad * Precio;
    }
}
