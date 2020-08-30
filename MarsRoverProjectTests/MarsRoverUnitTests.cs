using System.Collections.Generic;
using Contract.RoboticRover;
using Contract.RoboticRoverMap;
using MainFunction.ControlSystem;
using MainFunction.MainFunction;
using Xunit;

namespace MarsRoverProjectTests
{
    public class MarsRoverUnitTests
    {
        [Fact]
        public void Should_Plateau_Coordinate_ValidValue()
        {
            bool expectedResult = true;
            var platueCoordinate = "5 5";
            var controlSystem = new ControlSystem();

            var response = controlSystem.PlateauCoordinateControl(platueCoordinate);

            Assert.Equal(expectedResult, response.IsCoordinateControlSucceed);
        }

        [Fact]
        public void Should_Rover_Coordinate_ValidValue()
        {
            bool expectedResult = true;

            int platueXCoordinate = 5;
            int platueYCoordinate = 5;
            string roverLocation = "1 2 N";

            var controlSystem = new ControlSystem();

            var response = controlSystem.RoverCoordinateControl(platueXCoordinate, platueYCoordinate, roverLocation);

            Assert.Equal(expectedResult, response.IsCoordinateControlSucceed);
        }

        [Fact]
        public void Should_Map_Command_ValidValue()
        {
            bool expectedResult = true;

            string roverCommands = "LMLMLMLMMLM";

            var controlSystem = new ControlSystem();

            var response = controlSystem.RoverMapCommandControl(roverCommands);

            Assert.Equal(expectedResult, response.IsCoordinateControlSucceed);
        }

        [Fact]
        public void LastPositon_ShouldCorrect_WhenCalculateMethodcall()
        {
            char expectedLastPosition = 'N';
            int expectedXCoordinate = 1;
            int expectedYCoordinate = 3;

            var roverCoordinate = new RoverCoordinateResponseModel
            {
                RoverCoordinateModel = new RoverCoordinateModel()
                {
                    Position = 'N',
                    XCoordinate = 1,
                    YCoordinate = 2
                }
            };

            var commandsList = new List<MapCommand>();

            commandsList.Add(new MapCommand { Command = 'L' });
            commandsList.Add(new MapCommand { Command = 'M' });
            commandsList.Add(new MapCommand { Command = 'L' });
            commandsList.Add(new MapCommand { Command = 'M' });
            commandsList.Add(new MapCommand { Command = 'L' });
            commandsList.Add(new MapCommand { Command = 'M' });
            commandsList.Add(new MapCommand { Command = 'L' });
            commandsList.Add(new MapCommand { Command = 'M' });
            commandsList.Add(new MapCommand { Command = 'M' });

            var roverMapCommand = new RoverMapCommandResponseModel()
            {
                MapCommandsList = commandsList,
            };

            var calculate = new CalculateToRoboticRoverPosition();
            var response = calculate.CalculatePositionFunction(roverCoordinate, roverMapCommand);

            Assert.Equal(expectedLastPosition, response.LastPosition);
            Assert.Equal(expectedXCoordinate, response.LastXCoordinate);
            Assert.Equal(expectedYCoordinate, response.LastYCoordinate);
        }
    }
}
