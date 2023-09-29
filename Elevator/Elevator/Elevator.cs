using System;

public class Elevator
{
    public int numberOfElevators = 1;
    public int[] elevatorFloor;
    private bool[] floorReady;
    public int timeTaken = 10;
    public int myFloor = 0;
    public string currentFloor;
    private int topfloor;
    public ElevatorStatus Status = ElevatorStatus.STOPPED;

    public Elevator(int NumberOfFloors)
    {
        floorReady = new bool[NumberOfFloors + 1];
        topfloor = NumberOfFloors;
    }

    private void Stop(int floor)
    {
        Status = ElevatorStatus.STOPPED;
        myFloor = floor;
        floorReady[floor] = false;
    }

    private void Descend(int floor)
    {
        for (int i = myFloor; i >= 1; i--)
        {
            if (floorReady[i])
                Stop(floor);
            else
                continue;
        }
        Status = ElevatorStatus.STOPPED;
    }

    private void Ascend(int floor)
    {
        for (int i = myFloor; i <= topfloor; i++)
        {
            if (floorReady[i])
                Stop(floor);
            else
                continue;
        }
        Status = ElevatorStatus.STOPPED;
    }

    public void StayPut()
    {
        Console.WriteLine("That's our current floor");
    }

    public int NearestElevator(int myFloor)
    {
        int i = 0;

        if (numberOfElevators == 1)
        {
            i = elevatorFloor[0];
        }
        else
        {
            int[] findLift = new int[numberOfElevators];

            foreach (int lift in elevatorFloor)
            {
                findLift[i] = Math.Abs(lift - myFloor);
                i++;
            }

            for (int j = 0; j < findLift.Length - 1; j++)
            {
                if (findLift[j] < findLift[j + 1])
                {
                    i = j;
                }
                else
                {
                    i = j + 1;
                }
            }
            i = elevatorFloor[i];
        }

        Console.WriteLine("\nThe nearest lift to you is at floor {0}", i);

        return i;
    }

    public void FloorPress(int destFloor, int myFloor, int numberOfElevators)
    {
        this.numberOfElevators = numberOfElevators;
        elevatorFloor = new int[numberOfElevators];
        int nearestElevator = 0;

        if (destFloor > topfloor)
        {
            Console.WriteLine("We only have {0} floors", topfloor);
            return;
        }

        floorReady[destFloor] = true;

        for (int i = 0; i < numberOfElevators; i++)
        {
            Random random = new Random();
            elevatorFloor[i] = random.Next(1, topfloor);
            floorReady[elevatorFloor[i]] = true;
        }

        for(int i = 0; i < numberOfElevators; i++)
            Console.WriteLine("\nElevator {0} is at floor {1}", i, elevatorFloor[i]);

        nearestElevator = NearestElevator(myFloor);

        timeTaken = 10;

        if (nearestElevator > myFloor)
            timeTaken += Math.Abs(nearestElevator - myFloor) * 2;
        else if (nearestElevator < myFloor)
            timeTaken += Math.Abs(myFloor - nearestElevator) * 2;

        switch (Status)
        {
            case ElevatorStatus.DOWN:
                timeTaken += Math.Abs(myFloor - destFloor) * 2;
                Descend(destFloor);
                Console.WriteLine("\nDestination Reached! Total time taken to reach your destination: {0} \n\n", timeTaken);
                break;

            case ElevatorStatus.STOPPED:
                if (myFloor < destFloor)
                {
                    timeTaken += Math.Abs(destFloor - myFloor) * 2;
                    Ascend(destFloor);
                    Console.WriteLine("\nDestination Reached! Total time taken to reach your destination: {0} \n\n", timeTaken);
                }
                else if (myFloor == destFloor)
                {
                    StayPut();
                    Console.WriteLine("\nDestination Reached! Total time taken to reach your destination: {0} \n\n", timeTaken);
                }
                else
                {
                    timeTaken += Math.Abs(myFloor - destFloor) * 2;
                    Descend(destFloor);
                    Console.WriteLine("\nDestination Reached! Total time taken to reach your destination: {0} \n\n", timeTaken);
                }
                break;

            case ElevatorStatus.UP:
                timeTaken += Math.Abs(destFloor - myFloor) * 2;
                Ascend(destFloor);
                Console.WriteLine("\nDestination Reached! Total time taken to reach your destination: {0} \n\n", timeTaken);
                break;

            default:
                break;
        }
    }

    public enum ElevatorStatus
    {
        UP,
        STOPPED,
        DOWN
    }
}