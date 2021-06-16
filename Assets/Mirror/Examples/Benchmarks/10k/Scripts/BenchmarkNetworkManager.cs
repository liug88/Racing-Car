using System;

namespace Mirror.Examples
{
    public class BenchmarkNetworkManager : manager
    {
        /// <summary>
        /// hook for benchmarking
        /// </summary>
        public Action BeforeLateUpdate;
        /// <summary>
        /// hook for benchmarking
        /// </summary>
        public Action AfterLateUpdate;


        public override void LateUpdate()
        {
            BeforeLateUpdate?.Invoke();
            base.LateUpdate();
            AfterLateUpdate?.Invoke();
        }
    }
}
