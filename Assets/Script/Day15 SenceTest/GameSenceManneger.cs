using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSenceManneger : MonoBehaviour
{
    GameSenceManneger _instance;
    public GameSenceManneger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameSenceManneger();
            }
            return _instance;
        }
    }
    public string gameSence;//游戏场景的场景名称
    public Canvas canvas;//画布
    public TextMeshProUGUI startButtonText;//开始游戏按钮的文本组件
    public Button startGameButton;//开始游戏的按钮
    public Slider loadSlider;//显示加载的进度条
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(_instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        startButtonText.text = "Start Game";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("按下了W");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("SampleScene").isLoaded == false)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void OncilicButton()
    {
        StartCoroutine(LoadSence(gameSence));
    }
    /// <summary>
    /// 异步加载新场景
    /// 注：在真正打开新场景之前，process不一定到0.9，只是靠近0.9，并且isDone永远不会为true
    /// 所以开始加载先进行一次UI刷新，然后固定等待x秒，在执行之后的逻辑
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSence(string Sencename)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(Sencename);//异步加载场景保存引用
        startGameButton.enabled = false;//禁用按钮
        async.allowSceneActivation = false;//禁止加载完成后自动打开新场景
        float loadingPro1 = async.progress;//获取当前进度
        loadSlider.value = loadingPro1 + 0.1f;//改变滑动条的值
        startButtonText.text = "Loading...." + loadingPro1 / 0.9f * 100 + "/100";//改变显示的文字为加载中
        yield return new WaitForSeconds(1);
        do
        {
            float loadingPro = async.progress;//获取当前进度
            loadSlider.value = loadingPro + 0.1f;//改变滑动条的值
            startButtonText.text = "Loading...." + loadingPro / 0.9f * 100 + "/100";//改变显示的文字为加载中
            yield return null;
        }
        while (async.progress < 0.9f);
        startGameButton.enabled = true;//启用按钮
        startButtonText.text = "Enter Game";
        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(
            () =>
            {
                canvas.enabled = !canvas.enabled;//关闭画布
                async.allowSceneActivation = true;//打开新场景
            }
        );
    }
}
