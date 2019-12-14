using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Starbound.Common.WinForms.DragAndDrop
{
    [Serializable]
    public class DataContainer : ISerializable
    {
        public object Data { get; set; }

        public DataContainer(object data)
        {
            Data = data;
        }

        // Deserialization constructor 
        protected DataContainer(SerializationInfo info, StreamingContext context)
        {
            IntPtr address = (IntPtr)info.GetValue("dataAddress", typeof(IntPtr));
            GCHandle handle = GCHandle.FromIntPtr(address);
            Data = handle.Target;
            handle.Free();
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GCHandle handle = GCHandle.Alloc(Data);
            IntPtr address = GCHandle.ToIntPtr(handle);
            info.AddValue("dataAddress", address);
        }

        #endregion
    }

}
