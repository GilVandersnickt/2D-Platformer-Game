using Assets.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Game
{
    public class MenuController : MonoBehaviour
    {
        private IMenuService _menuService;

        [Inject]
        public void Construct(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public void CharacterSelect()
        {
            _menuService.GoToCharacterSelect();
        }

        public void Settings()
        {
            _menuService.GoToSettings();
        }
        public void SaveSettings(int time)
        {
            _menuService.SaveSettings(time);
        }
        public void SelectCharacter(int player)
        {
            _menuService.SelectCharacter(player);
            StartGame();
        }
        public void StartGame()
        {
            _menuService.StartGame();
        }


    }
}
