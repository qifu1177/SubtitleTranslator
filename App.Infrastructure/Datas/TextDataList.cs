using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Datas
{
    public class TextDataList
    {
        private List<TextData> _dataList;
        private StringBuilder _stringBuilder;
        public TextDataList()
        {
            _dataList = new List<TextData>();
            _stringBuilder = new StringBuilder();
        }
        public void AddData(TextData data)
        {
            if (_dataList.Count > 0 && _dataList[_dataList.Count - 1].IsTemp)
            {
                _dataList[_dataList.Count - 1] = data;
            }
            else
            {
                _dataList.Add(data);
            }
        }
        public string Text
        {
            get
            {
                _stringBuilder.Clear();
                foreach (TextData data in _dataList)
                {
                    _stringBuilder.AppendLine(data.Text);
                }
                return _stringBuilder.ToString();
            }
        }
        public void Clear()
        {
            _dataList.Clear();
        }
    }
}
