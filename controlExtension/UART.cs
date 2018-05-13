using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlExtension
{
    public partial class MainWindow : MetroWindow
    {
        private void initComPort(int comPortNum)
        {
            mySerialPort = new SerialPort(Vm.comPort.avaibleComPorts[comPortNum]);
            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Vm.comPort.avaibleComPorts = SerialPort.GetPortNames();
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                string str = mySerialPort.ReadExisting();
                Vm.comPort.answer = str;
                try
                {
                    JObject inJSON = JObject.Parse(str);
                    switch (inJSON["Comm"].ToString())
                    {
                        case "STAT?":
                            Vm.MainWin.Status = bool.Parse(inJSON["status"].ToString()) ? "Good" : "Bad";
                            Vm.MainWin.Status = "Receive status command \"STAT?\" = " + Vm.MainWin.Status;
                            break;
                        case "hx711?":
                            Vm.exper.time.Add(DateTime.UtcNow - Vm.exper.tStart);
                            Vm.hx711.Value = inJSON["val"].ToString();
                            Vm.MainWin.Status = "Receive hx711 value command \"hx711?\" = " + Vm.hx711.Value;
                            Vm.exper.rawData.Add(Convert.ToDouble(Vm.hx711.Value));
                            Vm.exper.miu.Add(Convert.ToDouble(Vm.hx711.Value) / Vm.exper.massa);
                            Dispatcher.Invoke(new Action(() =>
                            {
                                RAWdataPlot.graphPane.CurveList[0].AddPoint(Vm.exper.time[Vm.exper.time.Count - 1].TotalMilliseconds, Vm.exper.rawData[Vm.exper.rawData.Count - 1]);
                                miuPlot.graphPane.CurveList[0].AddPoint(Vm.exper.time[Vm.exper.time.Count - 1].TotalMilliseconds, Vm.exper.miu[Vm.exper.miu.Count - 1]);
                                updZed(RAWdataPlot.ZedGraphPlot);
                                updZed(miuPlot.ZedGraphPlot);
                            }));
                            break;

                        case "endStops?":
                            endstopsState = Convert.ToInt32(inJSON["val"]);
                            Vm.MainWin.Status = "Receive endstops State command \"endStops?\" = " + endstopsState;
                            break;
                    }
                }
                catch
                {
                    Vm.MainWin.Status = "Error parsing json";
                }
            }
        }
        private void sendComm(string Comm)
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.WriteLine("{\"Comm\":\"" + Comm + "\"" + "}");
                Vm.MainWin.Status = "Sending" + "{\"Comm\":\"" + Comm + "\"" + "}";
            }
        }

        private void sendComm(string Comm, string[] ParamNames, string[] Params)
        {
            if (mySerialPort.IsOpen)
            {
                string ParamBuf = "";
                for (int i = 0; i < ParamNames.Length; i++)
                {
                    ParamBuf += ",\"" + ParamNames[i] + "\":" + Params[i];
                }
                mySerialPort.WriteLine("{\"Comm\":\"" + Comm + "\"" + ParamBuf + "}");
                Vm.MainWin.Status = "Sending" + "{\"Comm\":\"" + Comm + "\"" + ParamBuf + "}";
            }
        }
    }
}
