// See https://aka.ms/new-console-template for more information
using MessageLogger.Data;
using MessageLogger.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new MessageLoggerContext())
{

    Console.WriteLine("Welcome to Message Logger!");
    Console.WriteLine();
    Console.WriteLine("Let's create a user profile for you.");
    Console.Write("What is your name? ");
    string name = Console.ReadLine();
    Console.Write("What is your username? (one word, no spaces!) ");
    string username = Console.ReadLine();
    User user = new User(name, username);

    context.Users.Add(user);
    context.SaveChanges();

    user = context.Users.Single(u => u.Username == username);

    Console.WriteLine();
    Console.WriteLine("To log out of your user profile, enter `log out`.");

    Console.WriteLine();
    Console.Write("Add a message (or `quit` to exit): ");

    string userInput = Console.ReadLine();
    //List<User> users = new List<User>() { user };

    while (userInput.ToLower() != "quit")
    {
        while (userInput.ToLower() != "log out")
        {
            user.Messages.Add(new Message(userInput));
            context.SaveChanges();

            //var allUserMessages = context.Messages.Where(u => u.Id == user.Id).ToList();

            foreach (var messages in user.Messages)
            {
                Console.WriteLine($"{user.Name} {messages.CreatedAt:t}: {messages.Content}");
            }

            Console.Write("Add a message: ");

            userInput = Console.ReadLine();
            Console.WriteLine();

            //Message newMessage = new Message(userInput);
            //context.Messages.Add(newMessage);

            // Create instance of message class and add user input
            // Add messages to the message context and save changes
        }

        Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
        userInput = Console.ReadLine();
        if (userInput.ToLower() == "new")
        {
            Console.Write("What is your name? ");
            name = Console.ReadLine();
            Console.Write("What is your username? (one word, no spaces!) ");
            username = Console.ReadLine();
            user = new User(name, username);
            //users.Add(user);
            Console.Write("Add a message: ");

            userInput = Console.ReadLine();

            //User newUser = new User(name, username);
            context.Users.Add(user);
            context.SaveChanges();
            user = context.Users.Single(u => u.Username == username);

            // Create instance of user class and add the user input to the user
            // Add the room to the room context and save changes

        }
        else if (userInput.ToLower() == "existing")
        {
            Console.Write("What is your username? ");
            username = Console.ReadLine();
            user = null;
            foreach (var existingUser in context.Users.Include(u => u.Messages))
            {
                if (existingUser.Username == username)
                {
                    user = existingUser;
                }
            }

            if (user != null)
            {
                Console.Write("Add a message: ");
                userInput = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("could not find user");
                userInput = "quit";

            }
        }

    }

    Console.WriteLine("Thanks for using Message Logger!");
    foreach (var u in context.Users)
    {
        Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
    }

}