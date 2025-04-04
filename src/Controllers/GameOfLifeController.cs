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
public ActionResult<Guid> SaveBoard([FromBody] Board board)
{
    var id = _service.SaveBoard(board);
    return Ok(id);
}


    }
}
