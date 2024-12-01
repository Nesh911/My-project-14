using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Функция для начала новой игры
    public void StartNewGame()
    {
        // Загружаем игровую сцену
        SceneManager.LoadScene("GameScene");
    }

    // Функция для продолжения игры (пока пустая, можно позже добавить сохранения)
    public void ContinueGame()
    {
        // На данный момент продолжение не реализовано
        Debug.Log("Continue game functionality not implemented yet.");
    }

    // Функция для выхода из игры
    public void QuitGame()
    {
        // Закрыть игру
        Application.Quit();
        Debug.Log("Quit game");
    }
}
