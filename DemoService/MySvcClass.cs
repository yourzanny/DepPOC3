using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DemoService
{
    public class MySvcClass
    {
        readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "ServiceLog.txt";
        readonly Timer _timer;
        public MySvcClass()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => File.AppendAllText(filePath, Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
