using Sorting_Hat_Quiz.CommonLayer.Classes;
using Sorting_Hat_Quiz.DataAccess;
using System.Linq;

namespace Sorting_Hat_Quiz;
class Program
{
    static void Main(string[] args)
    {
        // Pull ASCII code for intro
        Console.WriteLine("Hello, and welcome to the Wizarding World!");
        Console.WriteLine("If at any point you would like to stop the Sorting Hat Quiz, say 'Nox'");

        // Create and interface and reference it here
        var Spreadsheet = new SpreadsheetDataAccess();

        // Create a variable (list) that stores the results to calculate later
        var userAnswers = new List<string>();

        var Quiz = Spreadsheet.GetQuestions();
        for(int i = 0; i< Quiz.Questions.Count(); i++) 
        {
            Console.WriteLine(Quiz.Questions[i].Question);

            // Pull all the answers by id and order by alphabetical order
            var Options = Quiz.Answers.Where(A => A.AnswerId == Quiz.Questions[i].QuestionId).OrderBy(A=>A.AnswerText);

            // Assign a number to each answer to give the user as an input
            foreach(var Option in Options)
            {
                // Display answers with number assigned. I.e. 1) Harry Potter
                Console.WriteLine($"{(Options.ToList().IndexOf(Option)+1).ToString()}. {Option.AnswerText}");
            }

            bool ValidAnswer;
            do
            {
                // Read user input and validate it's correct (no other input should be allowed)
                Console.WriteLine("Please select the number corresponding to your preferred answer: ");
                var Input = Console.ReadLine();
                var intinput = 0;
                int.TryParse(Input, out intinput);

                // Compare user input and store in created variable (list)
                switch (intinput)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        ValidAnswer = true;
                        userAnswers.Add(Options.ElementAt(intinput-1).AnswerHouse);
                        break;
                    default:
                        if (Input.ToLower() == "nox")
                        {
                            return;
                        }
                        ValidAnswer = false;
                        Console.WriteLine("Sorry! That isn't valid input! Input must be a number between 1 and 4.");
                        break;
                }
            } while (ValidAnswer == false);
        }

        // Calculate the house the user belongs to based on the variable (list) values stored in it.
        var FinalHouseResult = userAnswers.GroupBy(ua => ua).OrderByDescending(c => c.Count()).FirstOrDefault();
        Console.WriteLine("Welcome to the family! You have been selected for house: " + FinalHouseResult.ToString());
        // If more than 1 match, then either use the first in alphabetical order or select a random one
        // Pull the right ASCII art for the result
        // Allow to try again
    }
}

