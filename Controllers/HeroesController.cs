using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HeroesFluent.Models;
using HeroesFluent.Data;

public class HeroesController : Controller
{
    private readonly HeroesDbContext _context;

    public HeroesController(HeroesDbContext context)
    {
        _context = context;
    }

    // GET: HEROES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Heroes.Include(h => h.Equipo).ToListAsync());
    }

    // GET: HEROES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var heroe = await _context.Heroes.Include(h => h.Equipo)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (heroe == null)
        {
            return NotFound();
        }

        return View(heroe);
    }

    // GET: HEROES/Create
    public IActionResult Create()
    {
        CargarEquipos();
        return View(new Heroe { FechaRegistro = DateTime.Now });
    }

    // POST: HEROES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Alias,NivelPoder,FechaRegistro,EquipoId")] Heroe heroe)
    {
        if (heroe.EquipoId.HasValue && !await _context.Equipos.AnyAsync(e => e.Id == heroe.EquipoId.Value))
        {
            ModelState.AddModelError(nameof(Heroe.EquipoId), "El equipo seleccionado no existe.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(heroe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    // GET: HEROES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var heroe = await _context.Heroes.FindAsync(id);
        if (heroe == null)
        {
            return NotFound();
        }

        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    // POST: HEROES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Alias,NivelPoder,FechaRegistro,EquipoId")] Heroe heroe)
    {
        if (id != heroe.Id)
        {
            return NotFound();
        }

        if (heroe.EquipoId.HasValue && !await _context.Equipos.AnyAsync(e => e.Id == heroe.EquipoId.Value))
        {
            ModelState.AddModelError(nameof(Heroe.EquipoId), "El equipo seleccionado no existe.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(heroe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroeExists(heroe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    // GET: HEROES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var heroe = await _context.Heroes.Include(h => h.Equipo)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (heroe == null)
        {
            return NotFound();
        }

        return View(heroe);
    }

    // POST: HEROES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var heroe = await _context.Heroes.FindAsync(id);
        if (heroe != null)
        {
            _context.Heroes.Remove(heroe);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HeroeExists(int id)
    {
        return _context.Heroes.Any(e => e.Id == id);
    }

    private void CargarEquipos(int? equipoSeleccionado = null)
    {
        ViewBag.EquipoId = new SelectList(_context.Equipos.OrderBy(e => e.Nombre), "Id", "Nombre", equipoSeleccionado);
    }
}
