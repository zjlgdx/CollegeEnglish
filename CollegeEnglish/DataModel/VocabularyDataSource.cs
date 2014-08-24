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

        public Course Course { get; private set; }

        public static async Task<Course> GetCourseAsync(string courseId)
        {
            await _vocabularyDataSource.GetDataAsync(courseId);

            return _vocabularyDataSource.Course;
        }

        private async Task GetDataAsync(string courseId)
        {
            //Uri dataUri = new Uri("ms-appx:///DataModel/BookListData.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await StorageDataHelper.GetCourse(courseId);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["NewWords"].GetArray();

            this.Course = new Course { CourseId = jsonObject["CourseId"].GetString(), CourseName = jsonObject["CourseName"].GetString(), NewWords = new List<NewWord>()};

            foreach (JsonValue wordValue in jsonArray)
            {
                JsonObject unitObject = wordValue.GetObject();
                NewWord newWord = new NewWord(unitObject["WordId"].GetString(),unitObject["Word"].GetString(),
                    "D:\\WP.CE\\" + unitObject["WordVoice"].GetString(),
                     unitObject["WordPhrase"].GetString(),
                     unitObject["Sentence"].GetString(),
                    "D:\\WP.CE\\" + unitObject["SentenceVoice"].GetString());

                this.Course.NewWords.Add(newWord);
            }
        }
    }
}
