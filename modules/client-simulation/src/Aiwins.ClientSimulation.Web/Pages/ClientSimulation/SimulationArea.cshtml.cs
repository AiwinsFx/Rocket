using System.Threading.Tasks;
using Aiwins.ClientSimulation.Snapshot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aiwins.ClientSimulation.Pages.ClientSimulation {
    public class SimulationAreaModel : PageModel {
        public SimulationSnapshot Snapshot { get; private set; }

        protected Simulation Simulation { get; }

        public SimulationAreaModel (Simulation simulation) {
            Simulation = simulation;
        }

        public Task OnGetAsync () {
            Snapshot = Simulation.CreateSnapshot ();
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostStartAsync () {
            await Task.Run (() => Simulation.Start ());
            return new NoContentResult ();
        }

        public async Task<IActionResult> OnPostStopAsync () {
            await Task.Run (() => Simulation.Stop ());
            return new NoContentResult ();
        }
    }
}