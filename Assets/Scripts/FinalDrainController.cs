using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalDrainController : MonoBehaviour
{
    public Slider timingSlider;
    public TextMeshProUGUI resultText;
    public Image teboImage;
    public Sprite normalTeboSprite;
    public Sprite splashTeboSprite;

    public float speed = 1.5f;
    private bool movingRight = true;
    private bool isLocked = false;

    void Start()
    {
        resultText.text = "";
        timingSlider.value = 0f;
    }

    void Update()
    {
        if (isLocked) return;

        // スライダーの値を左右にオート移動
        float step = speed * Time.deltaTime;
        if (movingRight)
            timingSlider.value += step;
        else
            timingSlider.value -= step;

        if (timingSlider.value >= 1f)
        {
            timingSlider.value = 1f;
            movingRight = false;
        }
        else if (timingSlider.value <= 0f)
        {
            timingSlider.value = 0f;
            movingRight = true;
        }

        // タップ検出
        if (Input.GetMouseButtonDown(0))
        {
            EvaluateTiming(timingSlider.value);
        }
    }

    void EvaluateTiming(float value)
    {
        isLocked = true;

        float center = 0.5f;
        float diff = Mathf.Abs(value - center);

        if (diff < 0.05f)
            resultText.text = "Perfect!";
        else if (diff < 0.15f)
            resultText.text = "Good!";
        else
            resultText.text = "Miss...";

        // テボ画像をスプラッシュに切り替え
        teboImage.sprite = splashTeboSprite;

        // 2秒後に戻す
        Invoke("ResetGame", 2f);
    }

    void ResetGame()
    {
        resultText.text = "";
        timingSlider.value = 0f;
        movingRight = true;
        isLocked = false;
        teboImage.sprite = normalTeboSprite;
    }
}
