using System;
using Aiwins.Rocket;
using Aiwins.ClientSimulation.Scenarios;

namespace Aiwins.ClientSimulation.Demo.Scenarios
{
    public class DemoScenario : Scenario
    {
        public DemoScenario(IServiceProvider serviceProvider) : 
            base(serviceProvider)
        {
            AddStep(new SleepScenarioStep("Wait1", RandomHelper.GetRandom(1000, 5000)));
            AddStep(new SleepScenarioStep("Wait2", RandomHelper.GetRandom(2000, 6000)));
        }
    }
}