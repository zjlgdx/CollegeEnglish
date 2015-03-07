using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using CollegeEnglish.Common;

namespace CollegeEnglish.DataModel
{
    public sealed class VocabularyDataSource
    {
        private static VocabularyDataSource _vocabularyDataSource = new VocabularyDataSource();

        public Unit Course { get; private set; }

        private string courseId;

        public static async Task<Unit> GetCourseAsync(string courseId)
        {
            await _vocabularyDataSource.GetDataAsync(courseId);

            return _vocabularyDataSource.Course;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId">"1/01p2newword1.htm"</param>
        /// <returns></returns>
        private async Task GetDataAsync(string courseId)
        {
            if (this.courseId == courseId)
            {
                return;
            }
            this.courseId = courseId;

            string jsonText = await StorageDataHelper.GetCourse(courseId);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Vocabularies"].GetArray();

            //this.Course = new Course { CourseId = jsonObject["CourseId"].ToJsonString(), CourseName = jsonObject["CourseName"].ToJsonString(), NewWords = new List<NewWord>() };
            this.Course = new Unit { UnitTitle = jsonObject["UnitTitle"].ToJsonString(), Vocabularies = new List<Vocabulary>() };
            var lessionId = courseId.Substring(0, 1);
            var basePath = Constants.DATA_BASE_PATH + "integrated" + lessionId + "/";
            foreach (JsonValue wordValue in jsonArray)
            {
                JsonObject unitObject = wordValue.GetObject();
                //NewWord newWord = new NewWord(unitObject["WordId"].ToJsonString(),
                //                              unitObject["Word"].ToJsonString(),
                //                              unitObject["WordVoice"].ToJsonString(Constants.DATA_BASE_PATH),
                //                              unitObject["WordPhrase"].ToJsonString(),
                //                              unitObject["Sentence"].ToJsonString(),
                //                              unitObject["SentenceVoice"].ToJsonString(Constants.DATA_BASE_PATH));
                var vocabulary = new Vocabulary(unitObject["Word"].ToJsonString()
                                               , unitObject["Voice"].ToJsonString(basePath)
                                               , unitObject["Definition"].ToJsonString()
                                               , unitObject["Paraphrase"].ToJsonString()
                                               , unitObject["ParaphraseVoice"].ToJsonString(basePath));
                this.Course.Vocabularies.Add(vocabulary);
            }
        }
    }
}
