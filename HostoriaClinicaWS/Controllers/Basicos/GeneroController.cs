﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HostoriaClinicaWS.Models;

namespace HostoriaClinicaWS.Controllers.Basicos
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly HistoriaClinicaContext _context;

        public GeneroController(HistoriaClinicaContext context)
        {
            _context = context;
        }

        // GET: api/Genero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGenero()
        {
            return await _context.Genero.ToListAsync();
        }

        // GET: api/Genero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(string id)
        {
            var genero = await _context.Genero.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // PUT: api/Genero/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(string id, Genero genero)
        {
            if (id != genero.GenCodigo)
            {
                return BadRequest();
            }

            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Genero
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            _context.Genero.Add(genero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GeneroExists(genero.GenCodigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGenero", new { id = genero.GenCodigo }, genero);
        }

        // DELETE: api/Genero/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genero>> DeleteGenero(string id)
        {
            var genero = await _context.Genero.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Genero.Remove(genero);
            await _context.SaveChangesAsync();

            return genero;
        }

        private bool GeneroExists(string id)
        {
            return _context.Genero.Any(e => e.GenCodigo == id);
        }
    }
}
