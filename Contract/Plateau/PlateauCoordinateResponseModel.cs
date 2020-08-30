namespace Contract.Plateau
{
    public class PlateauCoordinateResponseModel
    {
        public bool IsCoordinateControlSucceed { get; set; }
        public PlateauCoordinateModel PlateauCoordinateModel { get; set; }
        public ErrorMessageType ErrorMessageType { get; set; }
    }
}
