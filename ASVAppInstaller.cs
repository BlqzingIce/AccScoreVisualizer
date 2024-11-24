using Zenject;

namespace AccScoreVisualizer
{
    internal class ASVAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FlyingScoreEffectPatch>().AsSingle();
        }
    }
}
