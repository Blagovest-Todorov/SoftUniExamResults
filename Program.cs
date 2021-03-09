using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftUniExamResults
{
    class Program
    {
        static void Main(string[] args)
        {                                //key->user, value -> languages>
            //Dictionary<string, List<string>> langesByUser = new Dictionary<string, List<string>>();
            Dictionary<string, int> pointsByUser = new Dictionary<string, int>();
            Dictionary<string, int> submsByLanguage = new Dictionary<string, int>();

            //int submCount = 0;

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "exam finished")
                {
                    break;
                }

                string[] parts = line.Split('-');

                string userName = parts[0];
                string language = parts[1];   //on the place of language can be "banned" ;then we break;
                int points;

                if (language == "banned" && pointsByUser.ContainsKey(userName))
                {
                    pointsByUser.Remove(userName); 
                    break;                    
                }              

                if (pointsByUser.ContainsKey(userName) == false)
                {
                    points = int.Parse(parts[2]);
                    pointsByUser.Add(userName, points);

                    if (submsByLanguage.ContainsKey(language))
                    {
                        submsByLanguage[language]++;
                    }
                    else
                    {
                        submsByLanguage.Add(language, 1);
                    }
                }
                else  //exists username
                {
                    points = int.Parse(parts[2]);

                    if (pointsByUser[userName] < points)
                    {
                        

                        if (submsByLanguage.ContainsKey(language))
                        {
                            submsByLanguage[language]++;
                            pointsByUser[userName] = points;
                        }
                        else  //another Language add it 
                        {
                            submsByLanguage.Add(language, 1);
                            pointsByUser[userName] = points;
                        }
                    }
                    else  //(pointsByUser[userName] >= points)
                    {
                        if (submsByLanguage.ContainsKey(language))
                        {
                            submsByLanguage[language]++;
                        }
                    }
                }
            }

            Dictionary<string, int> sortedPoitnsByUser = pointsByUser
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Results:");

            foreach (var kvp in sortedPoitnsByUser)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }

            Dictionary<string, int> sortedSubmsByLanguage = submsByLanguage
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Submissions:");
            foreach (var kvp in sortedSubmsByLanguage)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }
    }
}

