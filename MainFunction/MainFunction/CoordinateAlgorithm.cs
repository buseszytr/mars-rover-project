using Contract.LastLocation;

namespace MainFunction.MainFunction
{
    public class CoordinateAlgorithm
    {
        public LastCoordinate Function(char lastPosition, LastCoordinate request)
        {
            var xCoordinate = request.XCoordinate;
            var yCoordinate = request.YCoordinate;

            var response = new LastCoordinate();

            switch (lastPosition)
            {
                case 'W':
                    xCoordinate = xCoordinate - 1;
                    break;
                case 'E':
                    xCoordinate = xCoordinate + 1;
                    break;
                case 'N':
                    yCoordinate = yCoordinate + 1;
                    break;
                case 'S':
                    yCoordinate = yCoordinate - 1;
                    break;
            }

            response.XCoordinate = xCoordinate;
            response.YCoordinate = yCoordinate;

            return response;
        }

    }
}
