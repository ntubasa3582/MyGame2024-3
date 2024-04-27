using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ClickSysTem : MonoBehaviour
{
    [SerializeField] List<float> _punchPower = new();
    [SerializeField] List<float> _buyPunchPower = new();
    [SerializeField] Text _clickCountText;
    [SerializeField] Text _nextPunchPowerBuyText;
    [SerializeField] Text _scaleAppleText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _clearTimeText;
    [SerializeField] Image[] _gameClearImages;
    [SerializeField] GameObject[] _spawnPoints; //オブジェクトの出現地点
    [SerializeField] GameObject[] _spawnObjects; //クリックしたら生成されるオブジェクト
    [SerializeField] GameObject _punchUI;
    [SerializeField] GameObject _resultUI;
    RandomNumSystem _randomNumSystem;
    SoundManager _soundManager;
    RaycastHit _clickHit;
    Vector3 _clickPos;
    GameObject _clickObj;
    int _randomNum;
    int _punchPowerCount;
    int _buyPunchPowerCount;
    float _timer;
    float _spawnCount;
    bool _isGameClear;
    bool _timerStop = false;

    void Awake()
    {
        _randomNumSystem = FindAnyObjectByType<RandomNumSystem>();
        _soundManager = FindAnyObjectByType<SoundManager>();
    }

    void Start()
    {
        _nextPunchPowerBuyText.text = _buyPunchPower[_buyPunchPowerCount].ToString("f0");
        _scaleAppleText.text = _punchPower[_punchPowerCount].ToString();
        _resultUI.SetActive(false);
    }

    void Update()
    {
        if (!_timerStop)
        {
            _timer += Time.deltaTime;
            _timerText.text = _timer.ToString("f0");
        }

        if (!_isGameClear)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mousePos, out _clickHit))
                {
                    if (_clickHit.collider.gameObject.name == "Tree")
                    {
                        for (var i = 0; i < _punchPower[_punchPowerCount]; i++)
                        {
                            InstanceObject();
                        }
                    }
                }
            }
        }

        if (_spawnCount >= 5000000)
        {
            _isGameClear = true;
            _timerStop = true;
            _resultUI.SetActive(true);
            _clearTimeText.text = _timer.ToString("f1");
            _soundManager.AudioPlay(1);
        }
    }

    void InstanceObject()
    {
        _randomNumSystem.ChooseStart();
        switch (_randomNumSystem.ChooseStart())
        {
            case 0:
                AppleInstance(0, 1);
                return;
            case 1:
                AppleInstance(1, 1.5f);
                return;
            case 2:
                AppleInstance(2, 1.5f);
                return;
            case 3:
                AppleInstance(3, 25);
                return;
            case 4:
                AppleInstance(4, 50);
                return;
        }
    }

    void AppleInstance(int value, float value2)
    {
        Instantiate(_spawnObjects[value], _spawnPoints[RandomNumValue(0, _spawnPoints.Length)].transform.position,
            Quaternion.identity);
        _spawnCount += 1 * _punchPower[_punchPowerCount] * value2;
        _clickCountText.text = _spawnCount.ToString("f0");
        _clickCountText.rectTransform.DOPivotY(0.3f, 0.1f).OnComplete(() =>
        {
            _clickCountText.rectTransform.DOPivotY(0.5f, 0.1f);
        });
        _soundManager.AudioPlay(0);
    }

    int RandomNumValue(int minValue, int maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    public void PunchPowerLevelUp()
    {
        if (!_isGameClear)
        {
            if (_punchPowerCount != 9)
            {
                if (_spawnCount >= _buyPunchPower[_buyPunchPowerCount])
                {
                    _spawnCount -= _buyPunchPower[_buyPunchPowerCount];
                    _buyPunchPowerCount++;
                    _punchPowerCount++;
                    _nextPunchPowerBuyText.text = _buyPunchPower[_buyPunchPowerCount].ToString();
                    _scaleAppleText.text = _punchPower[_punchPowerCount].ToString();
                    _clickCountText.text = _spawnCount.ToString("f0");
                    _soundManager.AudioPlay(2);
                }
                else
                {
                    Debug.Log("強化できない");
                    _soundManager.AudioPlay(3);
                }
            }
            else
            {
                _nextPunchPowerBuyText.text = "最大です";
            }

            _punchUI.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f)
                .OnComplete(() => { _punchUI.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f); });
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}