using Common.Services;
using Common.Utils;
using UnityEngine;

namespace Common.Input
{
    public class InputService : BaseService
    {
        public InputReader InputReader;

        public InputService(InputReader inputReader)
        {
            InputReader = inputReader;
            InputReader.Init();
        }

        protected override void OnInit()
        {
            base.OnInit();
        }
    }
}