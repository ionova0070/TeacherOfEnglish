using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Library;

namespace TestingOfProgram
{
	class Program
	{
		public static void Main(string[] args)
		{
			TeacherOfEnglish DictionaryTest = new TeacherOfEnglish();
			DictionaryTest.LoadWords();
			Console.WriteLine("Введите команду Начать");
			string Input = Console.ReadLine();
			
			if (String.Equals(Input, "Начать"))
			{
				for (int i = 0; ;i++)
				{
					string word = DictionaryTest.GenerateWord();
					Console.WriteLine(word);
					string[] answers = new string[4];
					answers = DictionaryTest.GenerateAnswers(word);
					
					for (int j = 0; j<4; j++)
					{
						Console.WriteLine(answers[j]);
					}
					
					string answer = Console.ReadLine();
					
					if (DictionaryTest.IsRightAnswer(word, answers[Convert.ToInt32(answer)]))
					{
						Console.WriteLine("RIGHT");
					}
					else Console.WriteLine("WRONG");
				}
				
			}
		}
	}
}
