using Prism.Events;
using System;

namespace Events
{
    public struct usbParcel
    {
        public usbReports_t report;
        public int value;
    }
    public class ConnectEvent : PubSubEvent<bool> { }
    public class RDChangedEvent : PubSubEvent<int> { }
    public class NewHX711valueEvent : PubSubEvent<int> { }
    public class WriteToDeviceEvent : PubSubEvent<usbParcel> { }
    public class ReadFromDeviceEvent : PubSubEvent<usbReports_t> { }
    public class SaveToFlashEvent : PubSubEvent { }
    public class GetHX711RawDataEvent : PubSubEvent { }
    public class ResponseFromDeviceEvent : PubSubEvent<int> { }
}
