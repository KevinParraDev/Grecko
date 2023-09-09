using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
     private int _currentLevel;

     private int _currentWorld;

     private int _maxLevelsInWorlds = 5;

     public static LevelManager Instance;

     private void Awake()
     {
          if(Instance == null)
          {
               Instance = this;

               // No destruir el LM durante el cambio de escenas
               DontDestroyOnLoad(this);
          }
          else
          {
               Destroy(gameObject);
          }
          
          
     }

     public int CurrentLevel
     {
          get { return _currentLevel; }
          set { _currentLevel = value; }
     }

     public int CurrentWorld
     {
          get { return _currentWorld; }
          set { _currentWorld = value; }
     }

     public void LoadNextLevel()
     {
          _currentLevel++;

          // Si llegamos al último nivel de un mundo, pasa al siguiente mundo
          // TODO: Definir si todos los mundos tendran los mismos niveles o no
          if(_currentLevel > _maxLevelsInWorlds)
          {
               _currentWorld++;
               CurrentLevel = 1;
          }

          // Se carga la escena del siguiente nivel
          LoadLevel(_currentWorld, _currentLevel);
     }

     public void LoadLevel(int World, int level)
     {
          HandleLevelChange(level, World);
     }

     private void HandleLevelChange(int World, int level)
     {
          // De esta forma se deben nombrar las escenas de niveles
          string sceneName = "World" + World + "_Level" + level;

          // Se puede realizar una corrutina en caso de querer alguna pantalla de carga
          SceneManager.LoadScene(sceneName);
     }
}
