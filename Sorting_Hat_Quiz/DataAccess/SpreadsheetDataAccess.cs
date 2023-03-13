using System;
using System.IO;
using Sorting_Hat_Quiz.CommonLayer.Classes;

namespace Sorting_Hat_Quiz.DataAccess
{
    public class SpreadsheetDataAccess
    {
        public QuestionAnswerMap GetQuestions()
        {
            var Response = new QuestionAnswerMap();
            var ListQuestions = new List<Questions>();
            var ListAnswers = new List<Answers>();

            // Need to find the file in the solution, and that may change depending on the computer the project is loaded
            // so, reading the folder of the project rather than hard coding it should help with that
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var file = Path.Combine(projectFolder, @"Assets/Question-Answer_Template.tsv");

            // I found the StreamReader class and how to use it, but may need to fully understand "using"
            using (StreamReader reader = new StreamReader(file))
            {
                var QuestionId = 0;
                while (!reader.EndOfStream) // Looping until the end of the stream (file)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split('\t');

                    QuestionId++; // Counter to account for each row

                    //read each column into a variable
                    var QuestionText = fields[0];
                    var AnswerGrif = fields[1];
                    var AnswerRav = fields[2];
                    var AnswerHuf = fields[3];
                    var AnswerSly = fields[4];

                    //Create q/a objects with values and id
                    Questions Q1 = new Questions {Question = QuestionText, QuestionId = QuestionId};
                    Answers AG = new Answers { AnswerHouse = "Gryffindor", AnswerId = QuestionId, AnswerText = AnswerGrif};
                    Answers AR = new Answers { AnswerHouse = "Ravenclaw", AnswerId = QuestionId, AnswerText = AnswerRav};
                    Answers AH = new Answers { AnswerHouse = "Hufflepuff", AnswerId = QuestionId, AnswerText = AnswerHuf };
                    Answers AS = new Answers { AnswerHouse = "Slytherin", AnswerId = QuestionId, AnswerText = AnswerSly };

                    // Assign each object (question or answer) to the list to retain data out of the loop
                    ListQuestions.Add(Q1);
                    ListAnswers.Add(AG);
                    ListAnswers.Add(AR);
                    ListAnswers.Add(AH);
                    ListAnswers.Add(AS);
                }
            }

            // Assign the lists of questions and answers to the Response object
            Response.Questions = ListQuestions;
            Response.Answers = ListAnswers;

            return Response;
        }
    }
}

