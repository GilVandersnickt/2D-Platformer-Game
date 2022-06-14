using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // IOC
            Container.Bind<IGameService>().To<GameService>().AsSingle();
            Container.Bind<IPlayerService>().To<PlayerService>().AsSingle();
            Container.Bind<IEnemyService>().To<EnemyService>().AsSingle();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
            Container.Bind<IMenuService>().To<MenuService>().AsSingle();

            Container.Bind<Player>().AsSingle();
        }
    }
}
