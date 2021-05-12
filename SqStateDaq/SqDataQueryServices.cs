using Automation.BDaq;
using System.Threading;

namespace SqStateDaq
{
    class SqDataQueryServices: IDataQuery
    {
        private readonly DaqCtrl _daqCtrl = new DaqCtrl();
        private readonly SqData _sqData = new SqData {Data = new double[0]};

        public SqData GetSqData()
        {
            var waveformCtrl = _daqCtrl.InitWaveformAiCtrl();
            waveformCtrl.Prepare();
            waveformCtrl.Start();

            while (waveformCtrl.State == ControlState.Running)
            {
                Thread.Sleep(1);
            }

            _sqData.Data = _daqCtrl.SqData;

            return _sqData;
        }
    }
}
