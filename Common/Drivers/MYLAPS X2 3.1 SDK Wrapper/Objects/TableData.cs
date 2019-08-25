using System;
using System.Collections.Generic;
using MylapsSDK.Containers;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using MylapsSDK.Utilities;
using MylapsSDK.Exceptions;

namespace MylapsSDK.Objects
{
    public class TableData: IDisposable
    {
        bool _disposed;
        private readonly IntPtr _nativeHandle;
        private readonly ResultData _resultDataHandleWrapper;

        /* Containers */
        private ResultRowContainer _resultRowContainer;
        private AnnouncementRowContainer _announcementRowContainer;
        private LapRowContainer _lapRowContainer;
//        private LapSectorCellContainer _lapSectorCellContainer;

        internal static TableData Create(ResultData resultDataHandleWrapper)
        {
            var nativeHandle = NativeMethods.mta_tabledata_handle_alloc(resultDataHandleWrapper.NativeHandle, IntPtr.Zero);
            if (nativeHandle == IntPtr.Zero)
            {
                throw new MylapsSDK.Exceptions.MylapsException("Unable to allocate new tabledata handle");
            }

            return new TableData(resultDataHandleWrapper, nativeHandle);
        }

        private TableData(ResultData resultDataHandleWrapper, IntPtr nativeHandle)
        {
            _nativeHandle = nativeHandle;
            _resultDataHandleWrapper = resultDataHandleWrapper;

            _resultRowContainer = new ResultRowContainer(this);
            _announcementRowContainer = new AnnouncementRowContainer(this);
            _lapRowContainer = new LapRowContainer(this);
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        ~TableData()
        {
            Dispose(false);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ClearContainers();
                }

                NativeMethods.mta_tabledata_handle_dealloc(_resultDataHandleWrapper.NativeHandle, _nativeHandle);

                // Note disposing has been done.
                _disposed = true;
            }
        }

        public ResultData ResultData
        {
            get { return _resultDataHandleWrapper; }
        }

        internal IntPtr NativeHandle
        {
            get { return _nativeHandle; }
        }

        public bool SubscribeToResultTableData(MTARESULTTABLEDATA resultTableDataType)
        {
            switch (resultTableDataType)
            {
                case MTARESULTTABLEDATA.mtaResultRow:
                case MTARESULTTABLEDATA.mtaLapRow:
                case MTARESULTTABLEDATA.mtaAnnouncementRow:
                    return NativeMethods.mta_tabledata_subscribe(_nativeHandle, resultTableDataType);

                default:
                    return false;
            }
        }

        public bool BindToResultTable(ResultTable resultTable)
        {
            return NativeMethods.mta_tabledata_bind(_nativeHandle, resultTable.NativePointer);
        }

        public void UnBindFromResultTable()
        {
            NativeMethods.mta_tabledata_unbind(_nativeHandle);
        }

        public ResultRowContainer ResultRowContainer
        {
            get { return _resultRowContainer; }
        }

        public AnnouncementRowContainer AnnouncementRowContainer
        {
            get { return _announcementRowContainer; }
        }

        public LapRowContainer LapRowContainer
        {
            get { return _lapRowContainer; }
        }

        private void ClearContainers()
        {
            _resultRowContainer.Clear();
            _announcementRowContainer.Clear();
            _lapRowContainer.Clear();
        }
    }
}
