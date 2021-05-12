using System;
using System.Collections.Generic;
using Automation.BDaq;

namespace SqStateDaq
{
    class DaqCtrl
    {
        public double[] SqData = new double[4096];
        
        public static IEnumerable<DeviceTreeNode> GetSupportedDevice()
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

        public WaveformAiCtrl InitWaveformAiCtrl()
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
                    Level = 3.0,
                }
            };
            waveformCtrl.Channels[0].SignalType = AiSignalType.SingleEnded;
            waveformCtrl.Channels[0].ValueRange = ValueRange.V_Neg10To10;
            waveformCtrl.Stopped += WaveformAiCtrl_Stopped;
            Console.WriteLine("Selected {0}. {1}", waveformCtrl.SelectedDevice.DeviceNumber, waveformCtrl.SelectedDevice);

            return waveformCtrl;
        }

        //  process the acquired data
        private void WaveformAiCtrl_Stopped(object sender, BfdAiEventArgs e)
        {
            var waveformAiCtrl = (WaveformAiCtrl)sender;

            var remainingCount = e.Count;
            if (e.Count <= 0) return;

            do
            {
                var chanCount = waveformAiCtrl.Conversion.ChannelCount;
                var sectionLength = waveformAiCtrl.Record.SectionLength;
                var bufSize = sectionLength * chanCount;
                var getDataCount = Math.Min(bufSize, remainingCount);
                waveformAiCtrl.GetData(getDataCount, SqData, 0, out var returnedCount);
                remainingCount -= returnedCount;
            } while (remainingCount > 0);

            waveformAiCtrl.Cleanup();
        }
    }
}
