using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace Library
{
	public class TeacherOfEnglish
	{
		Dictionary _dictionary;
		string[,] _wordsWithoutTranslation;
		
		public TeacherOfEnglish()
		{
			_dictionary = new Dictionary();
			_wordsWithoutTranslation = new string[3000,2];
		}
		public void LoadWords()
		{
			Console.WriteLine("И д е т   з а г р у з к а . . .");
			HttpWebRequest WRequest = (HttpWebRequest) WebRequest.Create("https://studyenglishwords.com/top-english-words");
			HttpWebResponse WResponse = (HttpWebResponse) WRequest.GetResponse();
			Stream BaseStream = WResponse.GetResponseStream();
			StreamReader StreamForReading = new StreamReader(BaseStream);
			
			bool IsBlockOfWord = false;
			string LineForReading;
			int Counter = 0;
			
			while ((LineForReading = StreamForReading.ReadLine()) != null)
			{
				string EnglishWord;
				
				if (LineForReading.Contains("wordBox"))
				{
					IsBlockOfWord = true;
					EnglishWord = LineForReading.Replace("<td class=\"wordBox\"><a href=\"/words/", String.Empty);
					EnglishWord = EnglishWord.Remove(EnglishWord.IndexOf('"'));
					EnglishWord = EnglishWord.Replace(" ", String.Empty);
					_wordsWithoutTranslation[Counter, 0] = EnglishWord;
				}
				
				if (IsBlockOfWord && LineForReading.Contains("\"percent\""))
				{
					string LineWithPercent = LineForReading;
					
					LineWithPercent = LineWithPercent.Replace("<td class=\"percent\">", String.Empty);
					LineWithPercent = LineWithPercent.Remove(LineWithPercent.IndexOf('%'));
					LineWithPercent = LineWithPercent.Replace(" ", String.Empty);
					LineWithPercent = LineWithPercent.Replace(".", ",");
					IsBlockOfWord = false;
					_wordsWithoutTranslation[Counter, 1] = LineWithPercent;
					Counter++;
				}		
			}
			
			StreamForReading.Close();
			BaseStream.Close();
			
			FilterDictionary();
			SortDictionary();
		}
		
		public string GenerateWord()
		{
			return _dictionary.GenerateWord();
		}
		
		public string[] GenerateAnswers(string originalWord)
		{
			return _dictionary.GenerateAnswers(originalWord);
		}
		
		public bool IsRightAnswer(string word, string answer)
		{
			return _dictionary.IsRightAnswer(word, answer);
		}
		
		string GetDefinition(string englishWord)
		{
			string PatternOfUrlForTranslation = "https://dictionary.cambridge.org/ru/словарь/англо-русский/";
			HttpWebRequest WRequest = (HttpWebRequest) WebRequest.Create(PatternOfUrlForTranslation + englishWord);
			HttpWebResponse WResponse = (HttpWebResponse) WRequest.GetResponse();
			Stream BaseStream = WResponse.GetResponseStream();
			StreamReader StreamForReading = new StreamReader(BaseStream);
				
			string LineForReadingTranslation;
			string TranslationOfWord = "WRONG";
			int Counter = 0;
				
			while ((LineForReadingTranslation = StreamForReading.ReadLine()) != null)
			{
				if (Counter != 0) Counter++;
				
				if (LineForReadingTranslation.Contains("<span class=\"trans\" lang=\"ru\">"))
				{
					Counter++;
				}
								
				if (Counter == 3)
				{
					TranslationOfWord = LineForReadingTranslation.Remove(LineForReadingTranslation.IndexOf('<'));
					TranslationOfWord = TranslationOfWord.TrimStart(' ');
					break;
				}				
			}
			StreamForReading.Close();
			BaseStream.Close();
			
			return TranslationOfWord;
		}
		
		void FilterDictionary()
		{
			_dictionary.FilterDictionary();
		}
		
		void SortDictionary()
		{
			_dictionary.SortDictionary();
		}
	}
}