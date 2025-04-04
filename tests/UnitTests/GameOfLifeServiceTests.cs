using GameOfLife.Models;
using GameOfLife.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;

namespace UnitTests
{
    public class GameOfLifeServiceTests
    {
        private readonly GameOfLifeService _service;

        public GameOfLifeServiceTests()
        {
            var loggerMock = new Mock<ILogger<GameOfLifeService>>();
            _service = new GameOfLifeService(loggerMock.Object);
        }

        [Fact]
        public void IsValidBoard_ShouldReturnFalse_IfRowsAndColsAreZero()
        {
            var board = new Board { Rows = 0, Columns = 0, Cells = new int[0][] };
            Assert.False(_service.IsValidBoard(board));
        }

        [Fact]
        public void SaveBoard_ShouldReturnNonEmptyGuid()
        {
            var board = new Board { Rows = 1, Columns = 1, Cells = new[] { new[] { 0 } } };
            var id = _service.SaveBoard(board);
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public void GetNextState_ShouldApplyGameRulesCorrectly()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Cells = new[]
                {
                    new[] { 0, 1, 0 },
                    new[] { 0, 1, 0 },
                    new[] { 0, 1, 0 }
                }
            };

            var next = _service.GetNextState(board);

            Assert.Equal(1, next.Cells[1][0]);
            Assert.Equal(1, next.Cells[1][1]);
            Assert.Equal(1, next.Cells[1][2]);
        }
    }
}
