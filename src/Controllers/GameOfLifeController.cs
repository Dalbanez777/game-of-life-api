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
            if (!_service.IsValidBoard(board))
                return BadRequest("Invalid board");

            var next = _service.GetNextState(board);
            return Ok(next);
        }

        [HttpGet("status")]
public ActionResult<string> Status()
{
    return Ok("Game of Life API is running");
}

[HttpPost("save")]
public async Task<ActionResult<Guid>> SaveBoard([FromBody] Board board)
{
    var id = await _service.SaveBoardAsync(board);
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
