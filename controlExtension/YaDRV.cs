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
        public StatusReport Status { get; private set; }
        public List<uint> HX711values { get; private set; }
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
                HX711values = new List<uint>();
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
            //byte[] -> StatusReport
            if (Status.IsNewHX711Value)
            {
                data = new byte[5] { (byte)CTL.HX711, 0, 0, 0, 0 };
                _hidapiw_native.Read(devIdx, ref data);
                //byte[] -> uint
                uint val = 0;
                HX711values.Add(val);
            }
        }
        public void Dispose()
        {
            _hidapiw_native.Close(devIdx);
            _hidapiw_native.Dispose();
        }
    }
}
