using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;

namespace SqStateDaq
{
    class Program
    {
        private static IEnumerable<DeviceTreeNode> GetSupportedDevice()
        {
            var waveformCtrl = new WaveformAiCtrl();
            var index = 0;
            foreach (var supportedDevice in waveformCtrl.SupportedDevices)
            {
                Console.WriteLine("{0}. {1}", index, supportedDevice);
                index++;
            }

            return waveformCtrl.SupportedDevices;
        }

        private static WaveformAiCtrl InitWaveformAiCtrl()
        {
            var waveformCtrl = new WaveformAiCtrl
            {
                SelectedDevice = new DeviceInformation(1),
                Conversion =
                {
                    ChannelStart = 0,
                    ChannelCount = 1,
                    ClockRate = 16384.0,
                },
                Record =
                {
                    SectionCount = 1,
                    SectionLength = 4096,
                },
                Trigger =
                {
                    Action = TriggerAction.DelayToStart,
                    Source = SignalDrop.SigExtAnaTrigger,
                    Edge = ActiveSignal.RisingEdge,
                    DelayCount = 0,
                    Level = 5.0,
                }
            };
            waveformCtrl.Stopped += WaveformAiCtrl_Stopped;
            Console.WriteLine("Selected {0}. {1}", waveformCtrl.SelectedDevice.DeviceNumber, waveformCtrl.SelectedDevice);
            
            return waveformCtrl;
        }

        static void Main(string[] args)
        {
            GetSupportedDevice();
            var waveformCtrl = InitWaveformAiCtrl();

            waveformCtrl.Prepare();
            waveformCtrl.Start();

            Console.ReadLine();
        }

        //  process the acquired data
        private static void WaveformAiCtrl_Stopped(object sender, BfdAiEventArgs e)
        {
            Console.WriteLine("{0}Acquisition has completed, all channel sample count is {1}", "\n", e.Count);

            var waveformAiCtrl = (WaveformAiCtrl)sender;
            var chanCount = waveformAiCtrl.Conversion.ChannelCount;
            var sectionLength = waveformAiCtrl.Record.SectionLength;
            var bufSize = sectionLength * chanCount;

            var remainingCount = e.Count;
            if (e.Count <= 0) return;

            var allChanData = new double[bufSize];
            do
            {
                var getDataCount = Math.Min(bufSize, remainingCount);
                waveformAiCtrl.GetData(getDataCount, allChanData, 0, out var returnedCount);
                remainingCount -= returnedCount;
            } while (remainingCount > 0);

            foreach (var data in allChanData)
            {
                Console.WriteLine("{0}", data);
            }
        }
    }
}
