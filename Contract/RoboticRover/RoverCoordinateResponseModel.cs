namespace Contract.RoboticRover
{
    public class RoverCoordinateResponseModel
    {
        public bool IsCoordinateControlSucceed { get; set; }
        public RoverCoordinateModel RoverCoordinateModel { get; set; }
        public ErrorMessageType ErrorMessageType { get; set; }
    }
}
