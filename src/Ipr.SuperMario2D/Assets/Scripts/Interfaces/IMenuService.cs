namespace Assets.Scripts.Interfaces
{
    public interface IMenuService
    {
        public void StartGame();
        public void GoToCharacterSelect();
        public void SelectCharacter(int player);
        public void GoToSettings();
        public void SaveSettings(int time);
    }
}
