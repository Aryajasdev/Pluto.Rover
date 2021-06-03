using System.Collections.Generic;
using NUnit.Framework;
using Rover.Pluto.Commands;
using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Test
{
    public class CommandHandlerTests
    {
        private Core.Impl.Rover rover;
        private Planet pluto;

        [SetUp]
        public void Setup()
        {
            var initialPosition = new Position(new Coordinate(0, 0), Direction.North);
            pluto = new Planet(100, 100, new List<Coordinate>());
            rover = new Core.Impl.Rover(initialPosition, pluto);
        }

        [TestCase(new[] { Command.F }, ExpectedResult = "0, 1, North")]
        [TestCase(new[]
        {
            Command.F,
            Command.F,
            Command.F,
            Command.F,
            Command.F

        }, ExpectedResult = "0, 5, North")]
        [TestCase(new[]
        {
            Command.F,
            Command.F,
            Command.F,
            Command.B,
            Command.B

        }, ExpectedResult = "0, 1, North")]
        public string MovementQueueHanlder_Moves_In_One_Axis_Successfully(Command[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }

        [TestCase(new[] { Command.L }, ExpectedResult = "0, 0, West")]
        [TestCase(new[] { Command.R }, ExpectedResult = "0, 0, East")]
        public string MovementQueueHanlder_Rotates_Successfully(Command[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }

        [TestCase(new[]
        {
            Command.L,
        }, 0, 0, Direction.East, ExpectedResult = "0, 0, North")]
        [TestCase(new[]
        {
            Command.R,
        }, 100, 100, Direction.West, ExpectedResult = "100, 100, North")]
        public string MovementQueueHandler_Rotation_Tests_With_Initial_Position(Command[] queue,
            int initialLatitude, int initialLongitude, Direction direction)
        {
            return MovementQueueHandler_With_InitialPosition_Test_Runner(queue, initialLatitude, initialLongitude, direction);
        }

        [TestCase(new[] {
            Command.F,
            Command.F,
            Command.L,
            Command.F
        }, ExpectedResult = "1, 2, West")]
        [TestCase(new[] {
            Command.F,
            Command.F,
            Command.L,
            Command.L,
            Command.F,
            Command.R,
            Command.F
        }, ExpectedResult = "1, 1, West")]
        [TestCase(new[] {
            Command.F,
            Command.F,
            Command.B,
            Command.L,
            Command.F,
            Command.F,
            Command.R,
            Command.F,
            Command.F,
            Command.R,
            Command.B,
        }, ExpectedResult = "3, 3, East")]
        public string MovementQueueHandler_Queue_With_Moves_And_Rotations(Command[] queue)
        {
            return MovementQueueHandler_Test_Runner(queue);
        }

        #region GridWrappingTests

        [TestCase(new[]
        {
            Command.F
        }, 100, 100, Direction.North, ExpectedResult = "100, 0, North")]
        [TestCase(new[]
        {
            Command.L,
            Command.F
        }, 100, 100, Direction.North, ExpectedResult = "0, 100, West")]
        [TestCase(new[]
        {
            Command.B
        }, 0, 0, Direction.North, ExpectedResult = "0, 100, North")]
        [TestCase(new[]
        {
            Command.L,
            Command.B,
        }, 0, 0, Direction.North, ExpectedResult = "100, 0, West")]
        public string MovementQueueHandler_Wraps_Works_Appropriately(Command[] queue,
            int initialLatitude, int initialLongitude, Direction direction)
        {
            return MovementQueueHandler_With_InitialPosition_Test_Runner(queue, initialLatitude, initialLongitude, direction);
        }


        [Test]
        public void MovementQueueHandler_Obstacle_Encountered_When_Moving_Forward_Rover_Stays_Put()
        {
            var initialPosition = new Position(new Coordinate(0, 0), Direction.North);
            var pluto = new Planet(100, 100, new List<Coordinate>() { new Coordinate(0,1) });
            var rover = new Core.Impl.Rover(initialPosition, pluto);

            var commandQueue = new MovementRequest()
            {
                Vehicle = rover,
                CommandQueue = new Queue<Command>(new[]
                {
                    Command.F
                })
            };

            var result = new MovementCommandHandler().Handle(commandQueue, default).Result;

            Assert.AreEqual(result.Position.ToString(),"0, 1, North");
            Assert.AreEqual(result.ReasonOfFailure, ReasonOfFailure.ObstacleDectected);
        }

        [Test]
        public void MovementQueueHandler_Obstacle_Encountered_When_Moving_Backward_Rover_Stays_Put()
        {
            var initialPosition = new Position(new Coordinate(1, 1), Direction.North);
            var pluto = new Planet(100, 100, new List<Coordinate>() { new Coordinate(1,0) });
            var rover = new Core.Impl.Rover(initialPosition, pluto);

            var commandQueue = new MovementRequest()
            {
                Vehicle = rover,
                CommandQueue = new Queue<Command>(new[]
                {
                    Command.B
                })
            };

            var result = new MovementCommandHandler().Handle(commandQueue, default).Result;

            Assert.AreEqual(result.Position.ToString(), "1, 0, North");
            Assert.AreEqual(result.ReasonOfFailure, ReasonOfFailure.ObstacleDectected);
        }

        #endregion

        #region Private implementation
        private string MovementQueueHandler_Test_Runner(Command[] queue)
        {
            var commandQueue = new MovementRequest()
            {
                Vehicle = rover,
                CommandQueue = new Queue<Command>(queue)
            };

            var newPosition = new MovementCommandHandler().Handle(commandQueue, default).Result.Position;

            return newPosition.ToString();
        }

        private string MovementQueueHandler_With_InitialPosition_Test_Runner(Command[] queue, int initialLatitude,
            int initialLongitude, Direction direction)
        {
            var initialPosition = new Position(new Coordinate(initialLatitude, initialLongitude), direction);
            var rover = new Core.Impl.Rover(initialPosition, pluto);

            var commandQueue = new MovementRequest()
            {
                Vehicle = rover,
                CommandQueue = new Queue<Command>(queue)
            };

            var newPosition = new MovementCommandHandler().Handle(commandQueue, default).Result.Position;

            return newPosition.ToString();
        }

        #endregion
    }
}