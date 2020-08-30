namespace MainFunction.MainFunction
{
    public class PositionAlgorithm
    {
        public char Function(char roverPosition, char command)
        {
            char lastPosition = roverPosition;
            switch (command)
            {
                case 'L':
                    {
                        switch (roverPosition)
                        {
                            case 'N':
                                lastPosition = 'W';
                                break;
                            case 'W':
                                lastPosition = 'S';
                                break;
                            case 'S':
                                lastPosition = 'E';
                                break;
                            case 'E':
                                lastPosition = 'N';
                                break;
                        }
                        break;
                    }

                case 'R':
                    {
                        switch (roverPosition)
                        {
                            case 'N':
                                lastPosition = 'E';
                                break;
                            case 'W':
                                lastPosition = 'N';
                                break;
                            case 'S':
                                lastPosition = 'W';
                                break;
                            case 'E':
                                lastPosition = 'S';
                                break;
                        }
                        break;
                    }

            }

            return lastPosition;
        }
    }
}
