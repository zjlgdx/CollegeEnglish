using CollegeEnglish.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

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

        private string bookId;

        public static async Task<IEnumerable<UnitTitle>> GetUnitTitlesAsync(string bookId)
        {
            await _unitTitleDataSource.GetDataAsync(bookId);

            return _unitTitleDataSource.UnitTitles;
        }

        private async Task GetDataAsync(string bookId)
        {
            if (this.bookId == bookId)
            {
                return;
            }
            this.bookId = bookId;

            this._unitTitles.Clear();
            string jsonText = await StorageDataHelper.GetUnitlist(bookId: bookId);
            JsonArray jsonArray = JsonArray.Parse(jsonText);

            foreach (JsonValue bookValue in jsonArray)
            {
                JsonObject unitObject = bookValue.GetObject();
                UnitTitle unitTitle = new UnitTitle(unitObject["UnitID"].ToJsonString(),
                                                    unitObject["UnitName"].ToJsonString(), 
                                                    unitObject["UnitImage"].ToJsonString(Constants.DATA_BASE_PATH));

                this.UnitTitles.Add(unitTitle);
            }
        }

        
    }
}
