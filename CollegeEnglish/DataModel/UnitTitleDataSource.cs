using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using CollegeEnglish.Common;

namespace CollegeEnglish.DataModel
{
    public class UnitTitle
    {
        public UnitTitle(string unitId, string unitName, string unitImage)
        {
            UnitID = unitId;
            UnitName = unitName;
            UnitImage = unitImage;
        }

        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitImage { get; set; }

        public override string ToString()
        {
            return UnitName;
        }
    }

    public sealed class UnitTitleDataSource
    {
        private static UnitTitleDataSource _unitTitleDataSource = new UnitTitleDataSource();

        private ObservableCollection<UnitTitle> _unitTitles = new ObservableCollection<UnitTitle>();
        public ObservableCollection<UnitTitle> UnitTitles
        {
            get { return this._unitTitles; }
        }

        public static async Task<IEnumerable<UnitTitle>> GetUnitTitlesAsync(string bookId)
        {
            await _unitTitleDataSource.GetDataAsync(bookId);

            return _unitTitleDataSource.UnitTitles;
        }

        public static async Task<UnitTitle> GetUnitTitleAsync(string bookId, string unitID)
        {
            await _unitTitleDataSource.GetDataAsync(bookId);
            // Simple linear search is acceptable for small data sets
            var matches = _unitTitleDataSource.UnitTitles.Where((unitTitle) => unitTitle.UnitID.Equals(unitID));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetDataAsync(string bookId)
        {
            if (this._unitTitles.Count != 0)
                return;

            //Uri dataUri = new Uri("ms-appx:///DataModel/BookListData.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await StorageDataHelper.GetUnitlist(bookId: bookId);
            JsonArray jsonArray = JsonArray.Parse(jsonText);
            //JsonArray jsonArray = jsonObject.GetArray();

            foreach (JsonValue bookValue in jsonArray)
            {
                JsonObject unitObject = bookValue.GetObject();
                UnitTitle unitTitle = new UnitTitle(unitObject["UnitID"].GetString(),
                                                            unitObject["UnitName"].GetString(), "D:\\WP.CE\\"+ unitObject["UnitImage"].GetString());

                this.UnitTitles.Add(unitTitle);
            }
        }

        
    }
}
