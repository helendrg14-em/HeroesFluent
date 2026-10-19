using System.ComponentModel.DataAnnotations;

namespace HeroesFluent.Models;

public class Heroe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El alias es obligatorio.")]
    [StringLength(60, ErrorMessage = "El alias no puede superar los 60 caracteres.")]
    public string Alias { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "El nivel de poder debe estar entre 1 y 100.")]
    public int NivelPoder { get; set; }

    [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
    public DateTime FechaRegistro { get; set; }
    public int? EquipoId { get; set; }

    public Equipo? Equipo { get; set; }
    public ICollection<HeroeMision> HeroeMisiones { get; set; } = new List<HeroeMision>();
}
