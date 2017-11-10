using System;

namespace Library
{
	public class Word
	{
		public string OriginalWord { get; private set; }
		public string TranslationOfWord { get; private set; }
		public double PercentOfPopularity { get; private set; }
		int _numberOfRightAnswers;
		
		public Word(string originalWord, string translationOfWord, double percentOfPopularity)
		{
			OriginalWord = originalWord;
			TranslationOfWord = translationOfWord;
			PercentOfPopularity = percentOfPopularity;
			_numberOfRightAnswers = 0;
		}
		
		public void AddRightAnswer()
		{
			_numberOfRightAnswers++;
		}
		
		public bool IsThisWordLearnt()
		{
			if (_numberOfRightAnswers == 3) return true;
			else return false;
		}
	}
}
