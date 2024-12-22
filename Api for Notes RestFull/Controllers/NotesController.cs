using Api_for_Notes_RestFull.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api_for_Notes_RestFull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static List<Note> notes = new List<Note>();

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetNotes()
        {
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetNoteById(Guid id)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public ActionResult<Note> CreateNote([FromBody] Note note)
        {
            note.Id = Guid.NewGuid();
            note.CreatedAt = DateTime.UtcNow;
            notes.Add(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Note updatedNote)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            notes.Remove(note);
            return NoContent();
        }
    }
}
