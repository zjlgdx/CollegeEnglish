using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeEnglish.DataModel
{
    public class NewWord
    {
        public NewWord(string wordId, string word, string wordVoice, string wordPhrase, string sentence, string sentenceVoice)
        {
            WordId = wordId;
            Word = word;
            WordVoice = wordVoice;
            WordPhrase = wordPhrase;
            Sentence = sentence;
            SentenceVoice = sentenceVoice;
        }
        public string WordId { get; set; }
        public string Word { get; set; }
        public string WordVoice { get; set; }
        public string WordPhrase { get; set; }
        public string Sentence { get; set; }
        public string SentenceVoice { get; set; }
    }
}
