using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace controlExtension
{

    public class YaDRV : IDisposable
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StatusReport
        {
            public bool IsStallGuard;
            public ushort MCUtemp;
            public bool IsNewHX711Value;
        }
        public class StatusReportEventArgs : EventArgs
        {
            public StatusReport Status { get; set; }
            public DateTime TimeReached { get; set; }
        }
        public class hx711valueEventArgs : EventArgs
        {
            public uint HX711value { get; set; }
            public DateTime TimeReached { get; set; }
        }
        private enum CTL
        {
            status,
            tmcRegister,
            Speed,
            HX711
        }
        private readonly List<hidDeviceInfo> devs = new List<hidDeviceInfo>();
        private readonly int devIdx = -1;
        private readonly Timer askForStatusTimer;

        public event EventHandler NewStatus;
        public event EventHandler NewHX711value;

        public string Error { get; private set; }
        hidapiw _hidapiw_native;
        YaDRV()
        {
            try
            {
                _hidapiw_native = new hidapiw();
                _hidapiw_native.Enumerate(ref devs, 0, 0);

                foreach (var device in devs)
                {
                    if (device.vendor_id == 1155 && device.product_id == 22352)
                    {

                        _hidapiw_native.Open(ref devIdx, device.vendor_id, device.product_id);
                        if (devIdx == -1)
                        {
                            throw new Exception("Failed to open device");
                        }
                        break;
                    }
                }
                askForStatusTimer = new Timer();
                askForStatusTimer.Elapsed += new ElapsedEventHandler(AskForStatusEvent);
                askForStatusTimer.Interval = 200;
                askForStatusTimer.Start();
            }
            catch (SEHException e)
            {
                if (e.StackTrace is string s)
                {
                    Error = s;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
        public void AskForStatusEvent(object source, ElapsedEventArgs e)
        {
            byte[] data = new byte[5] { (byte)CTL.status, 0, 0, 0, 0 };
            _hidapiw_native.Read(devIdx, ref data);
            StatusReportEventArgs tmp = new StatusReportEventArgs
            {
                //status = status,
                TimeReached = DateTime.Now
            };
            OnNewStatus(this, tmp);
            //byte[] -> StatusReport
            if (tmp.Status.IsNewHX711Value)
            {
                data = new byte[5] { (byte)CTL.HX711, 0, 0, 0, 0 };
                _hidapiw_native.Read(devIdx, ref data);
                //byte[] -> uint
                uint val = 0;
                hx711valueEventArgs _hx711valueEventArgs = new hx711valueEventArgs
                {
                    HX711value = val,
                    TimeReached = DateTime.Now
                };
                OnNewHX711value(this, _hx711valueEventArgs);
            }
        }
        protected virtual void OnNewStatus(object obj, StatusReportEventArgs e)
        {
            NewStatus?.Invoke(this, e);
        }
        protected virtual void OnNewHX711value(object obj, hx711valueEventArgs e)
        {
            NewStatus?.Invoke(this, e);
        }
        public void Dispose()
        {
            _hidapiw_native.Close(devIdx);
            _hidapiw_native.Dispose();
        }
    }
}
