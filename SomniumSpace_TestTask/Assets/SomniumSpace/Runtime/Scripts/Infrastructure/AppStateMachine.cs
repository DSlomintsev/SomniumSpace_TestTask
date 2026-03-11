using SomniumSpace.Infrastructure.States;
using CloserToTheSky.Infrastructure.States;
using Common.Infrastructure;

namespace SomniumSpace.Infrastructure
{
    public class AppStateMachine: BaseAppStateMachine
    {
        protected override void OnInit()
        {
            base.OnInit();
            
            States.Add(typeof(BootstrapState), new BootstrapState());
            States.Add(typeof(InitState), new InitState());
            States.Add(typeof(MissionStartState), new MissionStartState());
            States.Add(typeof(AppLoopState), new AppLoopState());
            States.Add(typeof(MissionEndState), new MissionEndState());
        }
    }
}