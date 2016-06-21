using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCulture
{
    /// <summary>
    /// Simplify culture implementation
    /// </summary>
    public class Culture: IDisposable
    {
        private CultureInfo originalCulture = null;
        private CultureInfo prevCulture = null;

        /// <summary>
        /// Culture constructor
        /// </summary>
        /// <param name="cultureName">Culture name</param>
        public Culture(string cultureName = "en-US")
        {
            CultureInfo newCulture = new CultureInfo(cultureName);
            if (newCulture != Get)
            {
                SaveCurrentCulture();
                SetCulture(newCulture);
            }
        }
        /// <summary>
        /// Set culture info
        /// </summary>
        /// <param name="cultureName">Culture name</param>
        public void Set(string cultureName = "en-US")
        {
            SetCulture(new CultureInfo(cultureName));
        }
        /// <summary>
        /// Get culture info
        /// </summary>
        public CultureInfo Get
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        #region PRIVATE METHODS
        private void SaveCurrentCulture()
        {
            prevCulture = Thread.CurrentThread.CurrentCulture;
            if (originalCulture==null)
            {
                originalCulture = Thread.CurrentThread.CurrentCulture;
            }
        }
        private void SetCulture(CultureInfo newCulture)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
        private void ReturnPreviousCulture()
        {
            SetCulture(prevCulture);
        }
        private void ReturnOriginalCulture()
        {

            SetCulture(originalCulture);
        }
        #endregion


        #region DISPOSE
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                ReturnOriginalCulture();
            }

            disposed = true;
        }
        #endregion
    }
}
