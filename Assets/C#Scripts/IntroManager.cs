using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

[System.Serializable]
public class IntroContent
{
    public string text;
    public Sprite image;
    public VideoClip videoClip;
}

public class IntroManager : MonoBehaviour
{
    public VideoPlayer introVideoPlayer;
    public RawImage displayRawImage;
    public RawImage fadeImage;
    public TextMeshProUGUI introText;
    public IntroContent[] introContents;
    public CanvasGroup textCanvasGroup;
    public float textFadeDuration = 2f;
    public float screenFadeDuration = 2f;
    public GameObject arrowIndicator;
    public float arrowRightPadding = 50f;
    public float arrowBottomPadding = 50f;
    public KeyCode pauseKey = KeyCode.Escape;
    public Color loadingScreenColor = Color.black;

    
    private int currentContentIndex = 0;
    private bool isTyping = false;
    private bool isComplete = false;

    void Awake()
    {
        fadeImage.color = Color.black; // 화면을 검게 시작
        arrowIndicator.SetActive(false); 
        introText.text = "";
        if (introContents.Length > 0 && introContents[0].image != null)
        {
            displayRawImage.texture = introContents[0].image.texture;
            displayRawImage.color = Color.white;
        }

        StartCoroutine(StartIntroSequence());
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            SceneManager.LoadScene("SettingsScene");
        }

        if (isComplete && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
        {
            AdvanceContent();
        }
    }

    IEnumerator StartIntroSequence()
    {
        // 페이드 인 효과
        yield return StartCoroutine(FadeIn(fadeImage));

        // 첫 번째 콘텐츠 시작
        AdvanceContent();
    }

    void AdvanceContent()
    {
        arrowIndicator.SetActive(false);
        
        if (currentContentIndex >= introContents.Length)
        {
            StartCoroutine(EndIntroSequence());
            return;
        }

        IntroContent currentContent = introContents[currentContentIndex];

        if (isComplete)
        // StartIntroSequence경로로 실행되지 않은 경우(Update()함수 경로)에서만 출력
        {
            StartCoroutine(DisplayContentWithFade());
        }
        else
        // StartIntroSequence경로로 실행된 경우 출력
        {
            StartCoroutine(DisplayText(currentContent.text));
        }
        currentContentIndex++;
    }

    IEnumerator DisplayContentWithFade()
    {
        isComplete = false;
        IntroContent currentContent = introContents[currentContentIndex];

        // 페이드 아웃
        yield return StartCoroutine(FadeOut(fadeImage));

        if (currentContent.image != null)
        {
            introVideoPlayer.Stop();
            introText.text = "";
            displayRawImage.texture = currentContent.image.texture;
            displayRawImage.color = Color.white;
        }
        else if (currentContent.videoClip != null)
        {
            introText.text = "";
            StartCoroutine(PlayVideo(currentContent.videoClip));
        }

        // 페이드 인
        yield return StartCoroutine(FadeIn(fadeImage));

        StartCoroutine(DisplayText(currentContent.text));

        // 텍스트 출력 완료
        isComplete = true;
    }

    IEnumerator FadeOut(RawImage fadeImage)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < screenFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / screenFadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeIn(RawImage fadeImage)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < screenFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / screenFadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    IEnumerator PlayVideo(VideoClip videoClip)
    {
        introVideoPlayer.clip = videoClip;
        introVideoPlayer.Prepare();

        while (!introVideoPlayer.isPrepared)
        {
            yield return null;
        }

        displayRawImage.texture = introVideoPlayer.targetTexture;
        displayRawImage.color = Color.white;
        introVideoPlayer.Play();
    }

    IEnumerator DisplayText(string text)
    {
        isComplete = false;
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
        introText.text = text;

        float elapsedTime = 0f;
        Color originalColor = introText.color;

        while (elapsedTime < textFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            originalColor.a = Mathf.Clamp01(elapsedTime / textFadeDuration);
            introText.color = originalColor;
            yield return null;
        }

        isComplete = true;
        arrowIndicator.SetActive(true);
    }

    IEnumerator EndIntroSequence()
    {
        // 최종 페이드 아웃
        yield return StartCoroutine(FadeOut(fadeImage));
        SceneManager.LoadScene("TutorialScene");
    }
}