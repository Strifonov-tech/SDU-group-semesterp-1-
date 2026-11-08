
public class Program
{

    public static void Main(string[] args)
    {
        RunApp();
    }


    static void RunApp()
    {
        if (!TryLoggingIn())
        {
            Environment.Exit(0);
        }
        showMainMenu();
    }


    static void showHeader(string headerName)
    {
        Console.WriteLine("====================");
        Console.WriteLine(headerName);
        Console.WriteLine("====================");
    }

    /// <summary>
    /// Attempts to do the logging in process. The user is given 3 tries, after which the app gives up..
    /// </summary>
    /// <returns>
    /// True if the user was logged in successfully, false othewise.
    /// </returns>
    static bool TryLoggingIn()
    {
        const uint MAX_TRIES = 3;
        uint currentTries = 0;
        while (currentTries < MAX_TRIES)
        {

            currentTries++;
            if (showLogin())
            {
                Console.WriteLine("====================");
                Console.WriteLine("You are logged in!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return true;
            }
            if (currentTries < MAX_TRIES)
            {
                Console.WriteLine("====================");
                Console.WriteLine("Incorrect username or password");
                Console.WriteLine("Try again!");
                Console.ReadKey();
            }
        }

        Console.WriteLine("====================");
        Console.WriteLine("You have exceeded the number of tries");
        Console.WriteLine("Goodbye");
        return false;

    }

    /// <summary>
    /// Shows the main login page. For now, the username and password are hardcoded
    /// </summary>
    /// <returns>
    /// True if the user logged in successfully, false otherwise
    /// </returns>
    /// <remarks>
    /// The hardcoded values could be adjusted in the future by putting the passwords and usernames
    /// on the actual operator class
    /// </remarks>
    static bool showLogin()
    {

        Console.Clear();
        showHeader("LOGIN");
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        Console.Write("Password: ");
        // password should preferably not be seen but due to the project simplicity this is ignored
        string password = Console.ReadLine() ?? "";

        return username == "admin" && password == "admin";
    }

    static void showMainMenu()
    {

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            showHeader("Welcome to the main menu");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Show History");
            Console.WriteLine("2. Show Active Incidents");
            Console.WriteLine("3. Show Information");
            string input = GetUserInput("Pick an option: ");
            if (!uint.TryParse(input, out uint number))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ReadKey();
                return;
            }

            switch(number)
            {
                case 0:
                    Console.WriteLine("Logging out...");
                    isRunning = false;
                    break;
                case 1:
                    ShowHistoryMenu();
                    break;
                case 2:
                    ShowActiveIncidentMenu();
                    break;
                case 3:
                    ShowInformationMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a valid number");
                    Console.ReadKey();
                    break;

            }
        }
    }

    static string GetUserInput(string question)
    {
        Console.WriteLine("--------------------------");
        Console.Write("Pick an option: ");
        return Console.ReadLine() ?? "";
    }

    static void ShowInformationMenu()
    {
        Console.Clear();
        showHeader("App Information");
        Console.WriteLine("Information");
        Console.WriteLine("More information");
        Console.WriteLine("Enter any key to return to main menu!");
        Console.ReadKey();
    }

    static void ShowActiveIncidentMenu()
    {
        Console.Clear();
        showHeader("Active Incidents");
        Console.WriteLine("1. Incident example here!");
        Console.WriteLine("2. Incident example here!");
        Console.WriteLine("3. Incident example here!");
        Console.WriteLine("4. Incident example here!");
        GetUserInput("Pick an incident number: ");
        // TODO show the information about the incident
    }

    static void ShowHistoryMenu()
    {
        Console.Clear();
        showHeader("History");
        Console.WriteLine("1. Incident example here!");
        Console.WriteLine("2. Incident example here!");
        Console.WriteLine("3. Incident example here!");
        Console.WriteLine("4. Incident example here!");
        GetUserInput("Pick an incident number: ");
        // TODO show the information about the incident
    }
}