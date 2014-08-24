using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeEnglish.DataModel
{
    public class NewWord: INotifyPropertyChanged
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

        private bool _wordMeaningVisible;
        public bool WordMeaningVisible {
            get { return _wordMeaningVisible; }
            set { this.SetProperty(ref this._wordMeaningVisible, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage,value))
            {
                return false;
            }
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
