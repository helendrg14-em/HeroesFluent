using System.ComponentModel.DataAnnotations;

namespace HeroesFluent.Models;

public class Equipo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, ErrorMessage = "El nombre no puede superar los 80 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de fundación es obligatoria.")]
    public DateTime FechaFundacion { get; set; }

    [Range(typeof(decimal), "0", "9999999999.99", ErrorMessage = "El presupuesto debe ser igual o mayor que cero.")]
    public decimal Presupuesto { get; set; }

    public ICollection<Heroe> Heroes { get; set; } = new List<Heroe>();
}
