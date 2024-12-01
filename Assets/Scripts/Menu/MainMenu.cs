using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ������� ��� ������ ����� ����
    public void StartNewGame()
    {
        // ��������� ������� �����
        SceneManager.LoadScene("GameScene");
    }

    // ������� ��� ����������� ���� (���� ������, ����� ����� �������� ����������)
    public void ContinueGame()
    {
        // �� ������ ������ ����������� �� �����������
        Debug.Log("Continue game functionality not implemented yet.");
    }

    // ������� ��� ������ �� ����
    public void QuitGame()
    {
        // ������� ����
        Application.Quit();
        Debug.Log("Quit game");
    }
}
