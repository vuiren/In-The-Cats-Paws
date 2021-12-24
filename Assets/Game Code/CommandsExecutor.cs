using Game_Code.Controllers.CatBotControllers;
using QFSW.QC;
using UnityEngine;
using Zenject;

namespace Game_Code
{
    public class CommandsExecutor : MonoBehaviour
    {
        [Inject]
        private ICatBotExplosionController _catBotExplosionController;

        [Command("cat.catbomb.turnsleftuntilexplosion", MonoTargetType.Single)]
        public int TimeUntilCatBotExplodes()
        {
            return _catBotExplosionController.TurnsUntilExplosion();
        }
    }
}
