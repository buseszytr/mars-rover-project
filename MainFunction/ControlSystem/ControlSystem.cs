using System;
using System.Collections.Generic;
using System.Linq;
using Contract;
using Contract.Plateau;
using Contract.RoboticRover;
using Contract.RoboticRoverMap;

namespace MainFunction.ControlSystem
{
    public class ControlSystem
    {
        public PlateauCoordinateResponseModel PlateauCoordinateControl(string plateauLocation)
        {
            var response = new PlateauCoordinateResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(plateauLocation))
                {
                    var firstItem = plateauLocation.Split(" ")?[0];
                    var secondItem = plateauLocation.Split(" ")?[1];

                    // If plateauPosition coordinate valid values return true and PlateauCoordinateResponseModel
                    if (int.TryParse(firstItem, out var xCoordinate) && int.TryParse(secondItem, out var yCoordinate))
                    {
                        if (xCoordinate > 0 && yCoordinate > 0)
                        {
                            var coordinateItem = new PlateauCoordinateModel()
                            {
                                XCoordinate = xCoordinate,
                                YCoordinate = yCoordinate
                            };
                            response = new PlateauCoordinateResponseModel()
                            {
                                IsCoordinateControlSucceed = true,
                                PlateauCoordinateModel = coordinateItem
                            };
                            return response;
                        }
                        else
                        {
                            response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                        }

                        return response;
                    }
                    else
                    {
                        response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                    }
                }
                else
                {
                    response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                }

                return response;
            }
            catch
            {
                response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                return response;
            }
        }

        public RoverCoordinateResponseModel RoverCoordinateControl(int plateauXCoordinate, int plateauYCoordinate, string roverLocation)
        {
            char[] roverPositionArray = { 'N', 'W', 'S', 'E' };
            var response = new RoverCoordinateResponseModel();
            var coordinateItem = new RoverCoordinateModel();

            try
            {
                if (!string.IsNullOrEmpty(roverLocation))
                {
                    var firstItem = roverLocation.Split(" ")?[0];
                    var secondItem = roverLocation.Split(" ")?[1];
                    var thirdItem = roverLocation.Split(" ")?[2];

                    if (int.TryParse(firstItem, out var xCoordinate) && int.TryParse(secondItem, out var yCoordinate) && char.TryParse(thirdItem, out var position))
                    {
                        if (xCoordinate > 0 && yCoordinate > 0)
                        {
                            if (xCoordinate <= plateauXCoordinate && yCoordinate <= plateauYCoordinate)
                            {
                                coordinateItem.XCoordinate = xCoordinate;
                                coordinateItem.YCoordinate = yCoordinate;
                            }
                            else
                            {
                                response.ErrorMessageType = ErrorMessageType.InvalidRoverCoordinatByPlateau;
                            }
                        }
                        else
                        {
                            response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                            return response;
                        }

                        if (!Char.IsUpper(position))
                        {
                            response.ErrorMessageType = ErrorMessageType.LowerCaseCharacter;
                            return response;
                        }
                        else if (!roverPositionArray.Contains(position))
                        {
                            response.ErrorMessageType = ErrorMessageType.InvalidRoverPosition;
                            return response;
                        }
                        else
                        {
                            coordinateItem.Position = position;
                            response.IsCoordinateControlSucceed = true;
                            response.RoverCoordinateModel = coordinateItem;
                            return response;
                        }
                    }
                    else
                    {
                        response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                    }
                }

                return response;
            }
            catch
            {
                response.ErrorMessageType = ErrorMessageType.InvalidCoordinat;
                return response;
            }
        }

        public RoverMapCommandResponseModel RoverMapCommandControl(string roverCommands)
        {
            char[] mapCommand = { 'L', 'R', 'M' };
            string pattern = "[0-9]+";
            char[] roverMapCommand = roverCommands?.ToCharArray();
            var mapCommandList = new List<MapCommand>();
            var response = new RoverMapCommandResponseModel();

            // If roverMapCommand Array exist digit value return error message
            foreach (var control in roverMapCommand)
            {
                var isDigit = System.Text.RegularExpressions.Regex.IsMatch(control.ToString(), pattern);

                if (isDigit)
                {
                    response.ErrorMessageType = ErrorMessageType.InvalidCharacter;
                    return response;
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(roverCommands) && !roverCommands.Contains(" "))
                {
                    foreach (var alpha in roverMapCommand.Where(c => Char.IsLetter(c)))
                    {
                        if (!Char.IsUpper(alpha))
                        {
                            response.ErrorMessageType = ErrorMessageType.InvalidCharacter;
                            return response;
                        }
                        else if (!mapCommand.Contains(alpha))
                        {
                            response.ErrorMessageType = ErrorMessageType.InvalidCharacter;
                            return response;
                        }
                        else
                        {
                            mapCommandList.Add(new MapCommand()
                            {
                                Command = alpha,
                            });
                        }

                        response.IsCoordinateControlSucceed = true;
                    }

                    response.MapCommandsList = mapCommandList;
                }

                return response;
            }
            catch
            {
                response.ErrorMessageType = ErrorMessageType.InvalidCharacter;
                return response;
            }
        }
    }
}
