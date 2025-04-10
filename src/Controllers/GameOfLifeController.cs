using GameOfLife.Models;
using GameOfLife.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameOfLifeController : ControllerBase
    {
        private readonly GameOfLifeService _service;

        public GameOfLifeController(GameOfLifeService service)
        {
            _service = service;
        }

        [HttpPost("next")]
public ActionResult<Board> GetNextState([FromBody] Board board)
{
    if (board == null)
        return BadRequest("Board is required.");

    if (board.Rows <= 0 || board.Columns <= 0)
        return BadRequest("Board dimensions must be greater than 0.");

    if (board.Cells == null || board.Cells.Length != board.Rows || board.Cells.Any(r => r.Length != board.Columns))
        return BadRequest("Invalid board structure.");

    var next = _service.GetNextState(board);
    return Ok(next);
}

        [HttpGet("status")]
public ActionResult<string> Status()
{
    return Ok("Game of Life API is running");
}

[HttpPost("save")]
public ActionResult<Guid> SaveBoard([FromBody] Board board)
{
    var id = _service.SaveBoard(board);
    return Ok(id);
}

[HttpGet("{id}")]
public ActionResult<Board> GetBoardById(string id)
{
    if (!Guid.TryParse(id, out var guid))
        return BadRequest("Invalid ID format.");

    var board = _service.GetBoard(guid);

    if (board == null)
        return NotFound("Board not found.");

    return Ok(board);
}


    }
}
