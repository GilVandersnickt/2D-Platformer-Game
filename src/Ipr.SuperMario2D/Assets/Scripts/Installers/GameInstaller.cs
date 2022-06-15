using Assets.Scripts.Controllers.Player;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public PlayerController[] playerPrefabs;

        public override void InstallBindings()
        {
            // IOC
            Container.Bind<IGameService>().To<GameService>().AsSingle();
            Container.Bind<IPlayerService>().To<PlayerService>().AsSingle();
            Container.Bind<IEnemyService>().To<EnemyService>().AsTransient();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
            Container.Bind<IMenuService>().To<MenuService>().AsSingle();

            // Factory for instantiating a player
            Container.BindFactory<PlayerController, PlayerController.Factory>()
                .FromComponentInNewPrefab(playerPrefabs[PlayerPrefs.GetInt(Constants.PlayerPrefsTitles.SelectedCharacter, 0) - 1]);
        }
    }
}
