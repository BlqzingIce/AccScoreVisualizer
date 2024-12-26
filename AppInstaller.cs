using Zenject;

namespace AccScoreVisualizer
{
    internal class AppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FlyingScoreEffectPatch>().AsSingle();
        }
    }
}
