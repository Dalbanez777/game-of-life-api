using System;
using GameOfLife.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace GameOfLife.Services
{
    public class GameOfLifeService
    {
        private readonly ILogger<GameOfLifeService> _logger;
        private readonly ConcurrentDictionary<Guid, Board> _boardStorage = new();

        public GameOfLifeService(ILogger<GameOfLifeService> logger)
        {
            _logger = logger;
        }

        public bool IsValidBoard(Board board)
        {
            return board != null && board.Rows > 0 && board.Columns > 0 && board.Cells != null;
        }

        public async Task<Guid> SaveBoardAsync(Board board)
{
    var id = Guid.NewGuid();
    _boardStorage[id] = board;
    await Task.CompletedTask; // Placeholder to simulate async (e.g., future DB/file writes)
    return id;
}

        public Board? GetBoard(Guid id)
{
    return _boardStorage.TryGetValue(id, out var board) ? board : null;
}

        public Board GetNextState(Board board)
        {
            var next = new int[board.Rows][];
            for (int i = 0; i < board.Rows; i++)
            {
                next[i] = new int[board.Columns];
                for (int j = 0; j < board.Columns; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(board.Cells, i, j);
                    if (board.Cells[i][j] == 1)
                        next[i][j] = (liveNeighbors == 2 || liveNeighbors == 3) ? 1 : 0;
                    else
                        next[i][j] = (liveNeighbors == 3) ? 1 : 0;
                }
            }
            return new Board { Rows = board.Rows, Columns = board.Columns, Cells = next };
        }

        private int CountLiveNeighbors(int[][] cells, int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    int nx = x + dx, ny = y + dy;
                    if (nx >= 0 && ny >= 0 && nx < cells.Length && ny < cells[0].Length)
                        count += cells[nx][ny];
                }
            return count;
        }
    }
}
