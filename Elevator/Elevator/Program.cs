using System;
using System.Threading;

public class Program
{
    private const string QUIT = "N";
    private const string NEWBUILDING = "Y";

    public static void Main(string[] args)
    {
        Console.WriteLine("=== WORKING OF A SINGLE ELEVATOR ===");
        Console.WriteLine("A single lift in the building which would take you to your desired floor. " +
            "It will calculate the time taken for the lift to arrive, open-close, and reach your destination. " +
            "\nEnjoy!");
        Console.WriteLine("====================================");
        Console.WriteLine("");

    Start:
        int floor = 0;
        string floorInput = "";
        Elevator elevator;

        Console.WriteLine("Enter the number of floors in the building: ");
        floorInput = Console.ReadLine();

        Console.WriteLine("Enter number of elevators in the building: ");
        string numberOfElevators = Console.ReadLine();

        if (Int32.TryParse(floorInput, out floor))
            elevator = new Elevator(floor);

        else
        {
            Console.WriteLine("Wrong Input! Kindly enter a number!");
            Console.Beep();
            Thread.Sleep(2000);
            Console.Clear();
            goto Start;
        }

        string input = string.Empty;

        while (input != QUIT)
        {
            Console.WriteLine("Enter the floor you are at: ");
            elevator.currentFloor = Console.ReadLine();
            Console.WriteLine("Enter the floor you wish to go to: ");
            input = Console.ReadLine();

            if (Int32.TryParse(input, out floor) && Int32.TryParse(elevator.currentFloor, out elevator.myFloor) && Int32.TryParse(numberOfElevators, out elevator.numberOfElevators))
                elevator.FloorPress(floor, elevator.myFloor, elevator.numberOfElevators);

            else if (input == QUIT)
                Console.WriteLine("GoodBye!");

            else if (input == NEWBUILDING)
            {
                Console.WriteLine("\nSTARTING AGAIN...\n");
                goto Start;
            }

            else
                Console.WriteLine("You have pressed an incorrect floor. Please try again!");

            Console.WriteLine("Do you want to continue? (Y/N): ");
            input = Console.ReadLine();
        }
    }
}