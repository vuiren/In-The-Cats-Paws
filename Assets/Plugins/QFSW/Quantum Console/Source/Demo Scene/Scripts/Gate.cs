using QFSW.QC;
using UnityEngine;

namespace Plugins.QFSW.Quantum_Console.Source.Demo_Scene.Scripts
{
    [CommandPrefix("demo.gate.")]
    public class Gate : MonoBehaviour
    {
        [Command("opened")]
        private bool IsOpened
        {
            get { return GetComponent<Animator>().GetBool("opened"); }
            set { GetComponent<Animator>().SetBool("opened", value); }
        }
    }
}
