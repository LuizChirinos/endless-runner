using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Triplano.Levels
{
    /// <summary>
    /// Asset responsible for managing LevelFlow
    /// </summary>
    [CreateAssetMenu(fileName = nameof(LevelFlow), menuName = "Triplano/Levels/LevelFlow")]
    public class LevelFlow : ScriptableObject
    {
        public delegate void OnChangedLevelState();

        public OnChangedLevelState OnStarted;
        public OnChangedLevelState OnPaused;
        public OnChangedLevelState OnWon;
        public OnChangedLevelState OnLost;

        public enum LevelState { STARTED, PAUSED, WON, LOSE }
        private LevelState levelStatus;

        public bool IsPaused { get => levelStatus == LevelState.PAUSED; }
        public bool Started { get => levelStatus == LevelState.STARTED; }
        public LevelState LevelStatus { get => levelStatus; }

        /// <summary>
        /// Changes level status to STARTED state
        /// </summary>
        public void StartLevel()
        {
            OnStarted?.Invoke();
            levelStatus = LevelState.STARTED;
        }

        /// <summary>
        /// Changes level status to PAUSED state
        /// </summary>
        public void PauseLevel()
        {
            OnPaused?.Invoke();
            levelStatus = LevelState.PAUSED;
        }
        public void WinLevel()
        {
            OnWon?.Invoke();
            levelStatus = LevelState.WON;
        }
        public void LoseLevel()
        {
            OnLost?.Invoke();
            levelStatus = LevelState.LOSE;
        }

        /// <summary>
        /// Resets the current scene
        /// </summary>
        public void ResetLevel()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Jumps to the next scene on the Build Settings (Resets the last scene, if there is no more scenes)
        /// </summary>
        public void NextLevel()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            if (buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
            {
                ResetLevel();
            }
            else
                LoadScene(buildIndex + 1);
        }

        /// <summary>
        /// Goes to the previous level on the Build Settings (Resets the first one, if there is no previous scenes)
        /// </summary>
        public void PreviousLevel()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            if (buildIndex == 0f)
            {
                ResetLevel();
            }
            else
                LoadScene(buildIndex - 1);
        }

        private void LoadScene(int buildIndex)
        {
            if (Application.isPlaying)
                SceneManager.LoadScene(buildIndex);
            //else
            //{
            //    string path = SceneManager.GetSceneByBuildIndex(buildIndex).path;
            //    EditorApplication.OpenScene(path);
            //}
        }
    }
}