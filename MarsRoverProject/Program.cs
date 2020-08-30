using System;
using Contract;
using Contract.Plateau;
using MainFunction.ControlSystem;
using MainFunction.MainFunction;

namespace MarsRoverProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var controlSystem = new ControlSystem();
            var plateauCoordinateControlResponse = new PlateauCoordinateResponseModel();
            var calculate = new CalculateToRoboticRoverPosition();

            Console.WriteLine("- Please enter two positive-valued coordinates(x,y) separated by a space for the plateau. \n- There should only be one space  between two coordinate! \n- Eg. Coordinate Input : 5 5\n ");
            var plateauCoordinate = Console.ReadLine();

            plateauCoordinateControlResponse = controlSystem.PlateauCoordinateControl(plateauCoordinate);

            while (!plateauCoordinateControlResponse.IsCoordinateControlSucceed)
            {
                Console.WriteLine("Please enter a valid coordinat value. Eg : 5 5 \n- There should only be one space  between two coordinate!\n ");
                plateauCoordinate = Console.ReadLine();
                plateauCoordinateControlResponse = controlSystem.PlateauCoordinateControl(plateauCoordinate);
            }

            if (plateauCoordinateControlResponse.PlateauCoordinateModel != null && (plateauCoordinateControlResponse.IsCoordinateControlSucceed && plateauCoordinateControlResponse.PlateauCoordinateModel.XCoordinate > 0 && plateauCoordinateControlResponse.PlateauCoordinateModel.YCoordinate > 0))
            {
                Console.WriteLine("- Please enter two positively valued coordinates(x,y) and a position ( N,E,W,S ) for the robotic rover. \n- There should be one spaces between the entered values. \n- The entered position value should be contain one of the values ​​-> Eg : N,E,W,S (North, East, West, South). \n- There should be uppercase that  entered  position values.  \n- Eg. Location Input: 3 5 N\n");
                var roverCoordinate = Console.ReadLine();
                var roverCoordinateResponse = controlSystem.RoverCoordinateControl(plateauCoordinateControlResponse.PlateauCoordinateModel.XCoordinate, plateauCoordinateControlResponse.PlateauCoordinateModel.YCoordinate, roverCoordinate);

                while (!roverCoordinateResponse.IsCoordinateControlSucceed)
                {
                    if (roverCoordinateResponse.ErrorMessageType == ErrorMessageType.InvalidRoverCoordinatByPlateau)
                    {
                        Console.WriteLine("Robotic rover coordinates entered should not exceed plateau coordinates. \n Please enter a valid coordinat value. Eg : 3 5 N \n");
                    }
                    else if (roverCoordinateResponse.ErrorMessageType == ErrorMessageType.InvalidCoordinat)
                    {
                        Console.WriteLine("Please enter a valid coordinat value. Eg : 3 5 N \n ( There should only be one space  between two coordinate!\n ");
                    }
                    else if (roverCoordinateResponse.ErrorMessageType == ErrorMessageType.LowerCaseCharacter)
                    {
                        Console.WriteLine("There should be uppercase that entered position values.\n Please enter a valid position value. Eg : 3 5 N \n");
                    }
                    else if (roverCoordinateResponse.ErrorMessageType == ErrorMessageType.InvalidRoverPosition)
                    {
                        Console.WriteLine("The entered position value should be contain one of the values ​​-> Eg : N,E,W,S (North, East, West, South).\n Please enter a valid position value. Eg : 3 5 N \n");
                    }

                    roverCoordinate = Console.ReadLine();
                    roverCoordinateResponse = controlSystem.RoverCoordinateControl(plateauCoordinateControlResponse.PlateauCoordinateModel.XCoordinate, plateauCoordinateControlResponse.PlateauCoordinateModel.YCoordinate, roverCoordinate);
                }

                if (roverCoordinateResponse.IsCoordinateControlSucceed)
                {
                    Console.WriteLine("- Please enter the commands the robotic rover should be complete. \n- The entered command value should be contain one of the values ​​-> Eg : R,L,M (Right, Left, Middle). \n- R value is turn right command \n- L value is turn left command \n- M value is command to remain still to maintain its current position \n- Command values that ​​entered should be in uppercase letters and there should not be spaces between them \n- Eg. Command LRLRMLRMLRMMR\n");
                    var roverMapCommands = Console.ReadLine();
                    var roverMapCommandsControlResponse = controlSystem.RoverMapCommandControl(roverMapCommands);

                    while (!roverMapCommandsControlResponse.IsCoordinateControlSucceed)
                    {
                        if (roverMapCommandsControlResponse.ErrorMessageType == ErrorMessageType.InvalidCharacter)
                        {
                            Console.WriteLine("- Please enter a valid command value\n- Command values that ​​entered should be in uppercase letters and there should not be spaces between them \n- The entered command value should be contain one of the values ​​-> Eg : R,L,M (Right, Left, Middle)..\n- Eg. Command LRLRMLRMLRMMR\n");
                        }

                        roverMapCommands = Console.ReadLine();
                        roverMapCommandsControlResponse = controlSystem.RoverMapCommandControl(roverMapCommands);
                    }

                    if (roverMapCommandsControlResponse.IsCoordinateControlSucceed)
                    {
                        var response = calculate.CalculatePositionFunction(roverCoordinateResponse, roverMapCommandsControlResponse);
                        Console.WriteLine("ROBOTIC ROVER LAST LOCATION: X COORDINATE, Y COORDINATE , POSITION  : " + response.LastXCoordinate + " " + response.LastYCoordinate + " " + response.LastPosition + "\n\n\n\n");
                    }
                }
            }

            Console.WriteLine("- MARS ROVER PROJECT - You can press enter to finish the project");
            var endOfTheProject = Console.ReadLine();
        }
    }
}
