using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library
{
	public class Dictionary
	{
		List<Word> _dictionaryOfWords;
		
		public Dictionary()
		{
			_dictionaryOfWords = new List<Word>();
		}
		
		public void AddWord(string englishWord, string translationOfWord, double percentOfPopularity)
		{
			_dictionaryOfWords.Add(new Word(englishWord, translationOfWord, percentOfPopularity));
		}
		
		public bool IsRightAnswer(string englishWord, string answer)
		{
			foreach (Word WordForReading in _dictionaryOfWords)
			{
				if (String.Equals(WordForReading.OriginalWord, englishWord) && String.Equals(WordForReading.TranslationOfWord, answer))
				{
					WordForReading.AddRightAnswer();
					
					if (WordForReading.IsThisWordLearnt())
					{
						_dictionaryOfWords.Remove(WordForReading);
					}
					return true;
				}
			}
			return false;
		}
		
		public void FilterDictionary()
		{
			List<Word> FilteredDictionary = new List<Word>();
			foreach (Word word in _dictionaryOfWords)
			{
				if (!String.Equals(word.TranslationOfWord, "WRONG"))
				{
					FilteredDictionary.Add(word);
			    }
			}
			
			_dictionaryOfWords = FilteredDictionary;
		}
		
		public void SortDictionary()
		{
			_dictionaryOfWords.Sort(delegate(Word word1, Word word2) { return word2.PercentOfPopularity.CompareTo(word1.PercentOfPopularity); });
		}
		
		public void ShowDictionary()
		{
			foreach (Word word in _dictionaryOfWords)
			{
				Console.WriteLine(word.OriginalWord+" "+word.TranslationOfWord+" "+word.PercentOfPopularity);
			}
		}
		
		public string GenerateWord()
		{
			if (_dictionaryOfWords.Count!=0)
			{
				return _dictionaryOfWords[new Random().Next(0, 10)].OriginalWord;
			}
			else
			{
				throw new Exception("Все слова изучены!");
			}
		}
		
		public string[] GenerateAnswers(string originalWord)
		{
			string[] Answers = new string[4];
			List<Word> PotentialAnswers = new List<Word>();
			for (int counter = 0; counter<10; counter++)
			{
				PotentialAnswers.Add(_dictionaryOfWords[counter]);
			}
						
			foreach (Word word in PotentialAnswers)
			{
				if (String.Equals(word.OriginalWord, originalWord))
				{
					Answers[0] = word.TranslationOfWord;
				}
				PotentialAnswers.Remove(word);
				break;
			}
			
			for (int counter = 0; counter<3; counter++)
			{
				int Index = new Random().Next(0, 9-counter);
				Answers[counter+1] = PotentialAnswers[Index].TranslationOfWord;
				PotentialAnswers.RemoveAt(Index);
			}
			
			Answers.OrderBy(x => x);
			return Answers;
		}
	}
}
