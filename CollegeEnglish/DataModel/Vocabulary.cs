
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace CollegeEnglish.DataModel
{
    public class Vocabulary : INotifyPropertyChanged
    {
        public Vocabulary(string word, string voice, string definition, string paraphrase, string paraphraseVoice)
        {
            this.Word = word;
            this.Voice = voice;
            this.Definition = definition;
            this.Paraphrase = paraphrase;
            this.ParaphraseVoice = paraphraseVoice;

        }
        public string Word { get; set; }
        public string Voice { get; set; }
        public string Definition { get; set; }
        public string Paraphrase { get; set; }
        public string ParaphraseVoice { get; set; }

        private bool _wordMeaningVisible;
        public bool WordMeaningVisible
        {
            get { return _wordMeaningVisible; }
            set { this.SetProperty(ref this._wordMeaningVisible, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value))
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
