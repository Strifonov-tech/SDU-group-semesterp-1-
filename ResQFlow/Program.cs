using ResQFlow;

public class Program
{

    /*
    ==========================================================================================
        PLACEHOLDERS SINCE NOT ALL THE CODE IS MADE YET. REMOVE LATER
    ==========================================================================================
    */
    // TODO remove this when the actual operator gets done
    public class Operator
    {
        public string Name { get; private set; }
        public string Password { get; private set; }

        public Operator(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
    }

    /*
    ==========================================================================================
        ACTUAL PARTS OF THE MAIN FILE
    ==========================================================================================
    */

    private class LoginCredentials
    {

        public string Username {get;}
        public string Password {get;}

        public LoginCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

    }



    private List<Operator> operators;
    private Operator? loggedUser;
    private DispatchSystem system;
    private const uint PAGE_SIZE = 5; // configures how many incidents are shown on information pages

    public Program(List<Operator> operators)
    {
        this.operators = operators;
        this.system = new DispatchSystem();
    }

    public static void Main(string[] args)
    {
        List<Operator> operators = new List<Operator>
        {
            new Operator("operator1", "test"),
            new Operator("operator2", "something"),
        };
        Program app = new Program(operators);
        app.RunApp();
    }


    /// <summary>
    /// Main function of the application
    /// </summary>
    private void RunApp()
    {
        if (!TryLoggingIn())
        {
            Environment.Exit(0);
        }
        showMainMenu();
    }


    private void ShowHeader(string headerName)
    {
        Console.WriteLine("====================");
        Console.WriteLine(headerName);
        Console.WriteLine("====================");
    }

    /// <summary>
    /// Controls the overall flow of logging in, such as the amount of retries, messages
    /// </summary>
    /// <returns>
    /// True if the user was logged in successfully, false othewise.
    /// </returns>
    private bool TryLoggingIn()
    {
        const uint MAX_TRIES = 3;
        uint currentTries = 0;
        while (currentTries < MAX_TRIES)
        {
            currentTries++;
            LoginCredentials credentials = ShowLogin();
            Operator? foundOperator = AttemptLogIn(credentials);

            if (foundOperator != null)
            {
                Console.WriteLine("====================");
                Console.WriteLine("You are logged in!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                this.loggedUser = foundOperator;
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
    /// Tries to find a valid operator given the credentials
    /// </summary>
    /// <param name="credentials">Credentials from the login</param>
    /// <returns>Found operator or null</returns>
    private Operator? AttemptLogIn(LoginCredentials credentials)
    {
        return this.operators.Find(op => op.Name == credentials.Username && op.Password == credentials.Password);
    }

    /// <summary>
    /// Shows the main login page. Allows the user to write his credentials.
    /// </summary>
    /// <returns>
    /// The login credentials obtained from the user
    /// </returns>
    private LoginCredentials ShowLogin()
    {

        Console.Clear();
        ShowHeader("LOGIN");
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        Console.Write("Password: ");
        // password should preferably not be seen but due to the project simplicity this is ignored
        string password = Console.ReadLine() ?? "";
        return new LoginCredentials(username, password);
    }


    /// <summary>
    /// Returns the currently logged in users name.
    /// </summary>
    /// <remarks>Due to the logged in user being possibly null, this simplifies allows us to get
    /// the name without checking for null each time </remarks>
    private string GetLoggedUsername()
    {
        ArgumentNullException.ThrowIfNull(this.loggedUser);
        return this.loggedUser.Name;
    }

    /// <summary>
    /// Shows the main menu. The main entry point of the app where they can navigate to any
    /// other part or exit
    /// </summary>
    private void showMainMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            ShowHeader($"Welcome to the main menu---Logged in as: {GetLoggedUsername()}");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Show History");
            Console.WriteLine("2. Show Active Incidents");
            Console.WriteLine("3. Show Information");
            Console.WriteLine("4. Log Out");

            string input = GetUserInput("Pick an option: ");

            if (!uint.TryParse(input, out uint number))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ReadKey();
                continue;
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
                case 4:
                    this.loggedUser = null;
                    isRunning = TryLoggingIn();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a valid number");
                    Console.ReadKey();
                    break;

            }
        }
    }


    /// <summary>
    /// Simplifies the process of asking for user input so it looks the same each time.
    /// </summary>
    /// <param name="question">What to ask the user?</param>
    /// <returns>Result from the user or empty string.</returns>
    private string GetUserInput(string question)
    {
        Console.WriteLine("--------------------------");
        Console.Write(question);
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Shows the menu that contains information about the application. Maybe credits?
    /// </summary>
    private void ShowInformationMenu()
    {
        Console.Clear();
        ShowHeader("App Information");
        Console.WriteLine("ResQFlow project for Semester 1 Software Engineering");
        Console.WriteLine("Credits: Group 1");
        // TODO add full names here
        Console.WriteLine("Richard Svajdlenka, Rares, Camilla, Svetoslav, Fabio, Xenia");
        Console.WriteLine("Enter any key to return to main menu!");
        Console.ReadKey();
    }

    /// <summary>
    /// Shows all the incidents that are currently active. Allows the user to pick a specific incident
    /// to handle what to send to it
    /// </summary>
    private void ShowActiveIncidentMenu()
    {
        Console.Clear();
        ShowHeader($"Active Incident---Logged in as: {GetLoggedUsername()}");
        Console.WriteLine("1. Incident example here!");
        Console.WriteLine("2. Incident example here!");
        Console.WriteLine("3. Incident example here!");
        Console.WriteLine("4. Incident example here!");
        GetUserInput("Pick an incident number: ");
        // TODO show the information about the incident
        // TODO show the menu for dispatching stuff?
    }

    private void ShowIncidentInformation(Incident incident)
    {
        Console.Clear();
        ShowHeader($"Incident {incident.incidentId}---Logged in as: {GetLoggedUsername()}");
        Console.WriteLine($"ID: {incident.incidentId}");
        Console.WriteLine($"Location: {incident.location}");
        Console.WriteLine($"Priority: {incident.priority}");
        Console.WriteLine($"Status: {incident.status}");
        Console.WriteLine($"Occured at: {incident.timeOfOccurrence}");
        Console.WriteLine($"Number of people in emergency: {incident.numOfPeople}");
    }

    /// <summary>
    /// Shows all the incidents that have been handled. Allows the user to pick a specific incident
    /// to show more information about it.
    /// </summary>
    private void ShowHistoryMenu()
    {
        uint currentPage = 1;
        Incident[] finishedIncidents = this.system.GetFinishedIncidents();
        uint pageCount = (uint) Math.Ceiling(finishedIncidents.Length / (double) PAGE_SIZE);
        // TODO extract the paging and showing of incidents into its own function
        while (true)
        {
            Console.Clear();
            ShowHeader($"History---Logged in as: {GetLoggedUsername()}");
            if (finishedIncidents.Length == 0)
            {
                Console.WriteLine("No finished incidents yet!");
                Console.ReadKey();
                return;
            }

            currentPage = Math.Clamp(currentPage, 1, pageCount);

            int startIndex = (int)((currentPage - 1) * PAGE_SIZE);
            long endIndex = Math.Min(startIndex + PAGE_SIZE, finishedIncidents.Length);

            Console.WriteLine("0: Exit to Main menu");

            for (int i = startIndex; i < endIndex; i++)
            {
                Console.WriteLine($"{i - startIndex + 1}: {finishedIncidents[i].incidentId}");
            }

            if (currentPage > 1)
            {
                Console.WriteLine($"{PAGE_SIZE + 1}. Previous Page");
            }

            if (currentPage < pageCount)
            {
                Console.WriteLine($"{PAGE_SIZE + 2}. Next Page");
            }

            string input = GetUserInput("Pick an option: ");

            if (!uint.TryParse(input, out uint choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ReadKey();
                continue;
            }

            switch (choice)
            {
                case 0:
                    return;
                case PAGE_SIZE + 1:
                    currentPage--;
                    break;
                case PAGE_SIZE + 2:
                    currentPage++;
                    break;
                default:
                    if (choice <= endIndex - startIndex)
                    {
                        Incident selectedIncident = finishedIncidents[startIndex + choice - 1];
                        ShowIncidentInformation(selectedIncident);
                        Console.WriteLine("Enter any key to return to the previous menu...");
                        Console.ReadKey();
                    } else
                    {
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                    }
                    break;
            }
        }
    }
}