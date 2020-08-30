using System.Linq;
using Contract.LastLocation;
using Contract.RoboticRover;
using Contract.RoboticRoverMap;

namespace MainFunction.MainFunction
{
    public class CalculateToRoboticRoverPosition
    {
        public LastLocation CalculatePositionFunction(RoverCoordinateResponseModel roverCoordinate, RoverMapCommandResponseModel roverMapCommand)
        {
            var lastLocationResponse = new LastLocation();

            if (roverCoordinate.RoverCoordinateModel != null && roverMapCommand.MapCommandsList.Any())
            {
                var positionAlgorithm = new PositionAlgorithm();
                var coordinateAlgorithm = new CoordinateAlgorithm();

                var coordinate = new LastCoordinate()
                {
                    XCoordinate = roverCoordinate.RoverCoordinateModel.XCoordinate,
                    YCoordinate = roverCoordinate.RoverCoordinateModel.YCoordinate
                };

                var lastPosition = roverCoordinate.RoverCoordinateModel.Position;

                foreach (var commandItem in roverMapCommand.MapCommandsList)
                {
                    lastPosition = positionAlgorithm.Function(lastPosition, commandItem.Command);
                    coordinate = coordinateAlgorithm.Function(lastPosition, coordinate);
                }

                lastLocationResponse.LastPosition = lastPosition;
                lastLocationResponse.LastXCoordinate = coordinate.XCoordinate;
                lastLocationResponse.LastYCoordinate = coordinate.YCoordinate;

                return lastLocationResponse;
            }

            return lastLocationResponse;
        }
    }
}


