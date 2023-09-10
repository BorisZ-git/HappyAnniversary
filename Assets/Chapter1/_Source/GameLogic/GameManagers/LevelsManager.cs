using UnityEngine.SceneManagement;

namespace GameManager.LevelsManager
{
    public static class LevelsManager
    {
        public static InputParent playerInput;
        public static InputParent ridePlatformInput;
        public static GameUI.UIController uIController;
        public static void FinishScene()
        {
            CheckInput();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public static void RestartLevel()
        {
            CheckInput();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void LoadLevel(int buildIndex)
        {
            CheckInput();
            SceneManager.LoadScene(buildIndex);
        }
        private static void CheckInput()
        {
            if(playerInput != null)
            {
                playerInput.Untying();
            }
            if(ridePlatformInput != null)
            {
                ridePlatformInput.Untying();
            }
            if(uIController != null)
            {
                uIController.Untying();
            }
        }
    }
}

