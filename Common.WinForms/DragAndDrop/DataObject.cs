using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.Common.WinForms.DragAndDrop
{
    public class DataObject : IDataObject
    {
        System.Collections.Hashtable _Data = new System.Collections.Hashtable();

        public DataObject() { }

        public DataObject(object data)
        {
            SetData(data);
        }

        public DataObject(string format, object data)
        {
            SetData(format, data);
        }

        #region IDataObject Members

        public object GetData(string format)
        {
            return _Data[format];
        }

        public object GetData(Type format)
        {
            return _Data[format.FullName];
        }

        public object GetData(string format, bool somethingThatIllIgnore)
        {
            return GetData(format);
        }

        public bool GetDataPresent(string format)
        {
            return _Data.ContainsKey(format);
        }

        public bool GetDataPresent(string format, bool somethingThatIllIgnore)
        {
            return GetDataPresent(format);
        }

        public bool GetDataPresent(Type format)
        {
            return _Data.ContainsKey(format.FullName);
        }

        public string[] GetFormats()
        {
            string[] strArray = new string[_Data.Keys.Count];
            _Data.Keys.CopyTo(strArray, 0);
            return strArray;
        }

        public string[] GetFormats(bool autoConvert)
        {
            return GetFormats();
        }

        public void SetData(string format, object data)
        {
            object obj = new DataContainer(data);

            if (string.IsNullOrEmpty(format))
            {
                // Create a dummy DataObject object to retrieve all possible formats. 
                // Ex.: For a System.String type, GetFormats returns 3 formats: 
                // "System.String", "UnicodeText" and "Text" 
                System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject(data);
                foreach (string fmt in dataObject.GetFormats())
                {
                    _Data[fmt] = obj;
                }
            }
            else
            {
                _Data[format] = obj;
            }
        }

        public void SetData(string format, bool somethingIllIgnore, object data)
        {
            SetData(format, data);
        }

        public void SetData(Type type, object data)
        {
            SetData(type.FullName, data);
        }

        public void SetData(object data)
        {
            SetData((string)null, data);
        }

        #endregion
    }
}
