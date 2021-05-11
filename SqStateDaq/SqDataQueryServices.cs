using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;
using System.Threading;

namespace SqStateDaq
{
    class SqDataQueryServices: IDataQuery
    {
        public SqData GetSqData()
        {
            var daqCtrl = new DaqCtrl();
            var waveformCtrl = daqCtrl.InitWaveformAiCtrl();
            waveformCtrl.Prepare();
            waveformCtrl.Start();

            while (waveformCtrl.State == ControlState.Running)
            {
                Thread.Sleep(1);
            }

            return new SqData {Data = daqCtrl.SqData};
        }
    }
}
