using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuStartManager : MonoBehaviour
{
    public float fadeDuration = 2f; // 페이드 아웃 시간
    public Image image; // 페이드 효과를 줄 Image

    public void OnStartButtonClicked()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    public void OnQuitButtonClicked()
    {
        StartCoroutine(QuitAgent());
    }

    private IEnumerator QuitAgent()
    {
        yield return new WaitForSeconds(0.5f);
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = 0; // 초기 alpha값 0 설정
        image.color = color;

        // 페이드 아웃 효과
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // alpha 값 증가
            image.color = color;
            yield return null;
        }

        SceneManager.LoadScene("IntroScene");
    }
}