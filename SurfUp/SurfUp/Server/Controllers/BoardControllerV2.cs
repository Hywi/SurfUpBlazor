using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SurfUp.Server.Data;
using SurfUp.Shared;

namespace SurfUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class BoardControllerV2 : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BoardControllerV2(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Board>> GetAllBoards()
        {
            return _context.Boards.OrderBy(a => a.Name).ToList().Where(b => b.Premium == false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBoard(int id)
        {
            var boards = await _context.Boards.FindAsync(id);

            if (!BoardExists(id))
            {
                return NotFound();
            }

            else if (boards.Premium == true)
            {
                return Unauthorized();
            }

            else
            {
                return Ok(_context.Boards.Find(id));
            }
        }

        [HttpPost("Rent/{id}")]
        public async Task<IActionResult> RentOut(int id, [FromBody] Rent rent)
        {
            if (!BoardExists(id))
            {
                return NotFound();
            }
            int tmpID = id; // IDK what it does

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //userID = claims.Value;
            rent.BoardId = id;
            //rent.UserID = userID;

            if (rent.StartRent > rent.EndRent)
            {
                ModelState.AddModelError("StartRent", "Start date must be before end date");
            }
            else
            {
                if (rent.UserID != null && rent.BoardId != 0) //TODO vi vil gerne have modelstate.isvalid, men vi kan ikke få det til at fungere
                {
                    try
                    {
                        Board board = FindBoard(id);
                        board.IsRented = true;
                        board.UserID = rent.UserID;

                        _context.Update(board);
                        await _context.SaveChangesAsync();

                        _context.Add(rent);
                        await _context.SaveChangesAsync();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError(string.Empty, "Board is already rented");
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError(string.Empty, "Someone was faster than you");
                    }
                }
            }
            return Ok(rent);
        }

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

