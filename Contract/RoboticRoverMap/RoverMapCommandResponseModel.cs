using System.Collections.Generic;

namespace Contract.RoboticRoverMap
{
    public class RoverMapCommandResponseModel
    {
        public bool IsCoordinateControlSucceed { get; set; }
        public List<MapCommand> MapCommandsList { get; set; }
        public ErrorMessageType ErrorMessageType { get; set; }
    }
}
