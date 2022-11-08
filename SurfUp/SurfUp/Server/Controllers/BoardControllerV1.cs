using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfUp.Server.Data;
using SurfUp.Server.Models;
using SurfUp.Shared;
using System.Security.Claims;

namespace SurfUp.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BoardControllerV1 : Controller
    {
        private readonly ApplicationDbContext _context;
        public BoardControllerV1(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Board>>> GetAllBoard()
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return NotFound();
            return Ok(user.Boards.OrderBy(a => a.Name));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBoard(int id)
        {
            return Ok(_context.Boards.Find(id));
        }
        [HttpPost("Rent/{id}")]
        //public async Task<IActionResult> RentOut(int id, [FromBody] Rent rent)
        //{
        //    if (!BoardExists(id))
        //    {
        //        return NotFound();
        //    }
        //    rent.BoardId = id;
        //    if(User.FindFirstValue(ClaimTypes.NameIdentifier) != null && rent.BoardId != 0)
        //    {
        //        Board board = FindBoard(id);
        //        board.IsRented = true;
        //        //jeg kan ikke ændre noget ApplicationUserId på nogen af dem så man skal have en variabel i klassen.
        //    }
        //}
        private bool BoardExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
        private Board FindBoard(int id)
        {
            Board tmpBoard = new();
            foreach (var board in _context.Boards)
            {
                if (id == board.Id)
                {
                    tmpBoard = board;
                    break; //TODO <- Test 
                }
            }
            return tmpBoard;
        }
    }    
}
