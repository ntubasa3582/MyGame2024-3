using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSysTem : MonoBehaviour
{
    RandomNumSystem _randomNumSystem;
    RaycastHit _clickHit;
    Vector3 _clickPos;
    [SerializeField] List<float> _punchPower = new List<float>();
    [SerializeField] List<int> _weights = new List<int>();   // 重み設定用変数
    [SerializeField] List<int> _buyPunchPower = new List<int>();
    [SerializeField] Text _clickCountText = default;
    [SerializeField] Text _nextPunchPowerBuyText = default;
    [SerializeField] Text _scaleAppleText = default;
    [SerializeField] GameObject[] _spawnPoints;     //オブジェクトの出現地点
    [SerializeField] GameObject[] _spawnObjects;    //クリックしたら生成されるオブジェクト
    GameObject _clickObj = default;
    int _randomNum = 0;
    int _punchPowerCount = 0;
    int _buyPunchPowerCount = 0;
    float _spawnCount = 0;
    private void Awake()
    {
        _randomNumSystem = GameObject.FindAnyObjectByType<RandomNumSystem>();
    }

    void Start()
    {
        _nextPunchPowerBuyText.text = _buyPunchPower[_buyPunchPowerCount].ToString();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePos, out _clickHit))
            {
                if (_clickHit.collider.gameObject.name == "Tree")
                {
                    for (int i = 0; i < _punchPower[_punchPowerCount]; i++)
                    {
                        InstanceObject();
                    }
                }
            }
        }
    }

    public void InstanceObject()
    {
        _randomNumSystem.ChooseStart();
        switch (_randomNumSystem.ChooseStart())
        {

            case 0:
                AppleInstance(0,1);
                return;
            case 1:
                AppleInstance(1,1.5f);
                return;
            case 2:
                AppleInstance(2,2);
                return;
        }
    }

    private void AppleInstance(int value,float value2)
    {
        Instantiate(_spawnObjects[value], _spawnPoints[RandomNamValue(0, _spawnPoints.Length)].transform.position, Quaternion.identity);
        _spawnCount += 1 * _punchPower[_punchPowerCount]*value2;
        _clickCountText.text = _spawnCount.ToString("f0");
    }

    public int RandomNamValue(int minValue, int MaxValue)
    {
        return Random.Range(minValue, MaxValue);
    }
    public void PunchPowerLevelUp()
    {
        if (_spawnCount >= _buyPunchPower[_buyPunchPowerCount])
        {
            _spawnCount -= _buyPunchPower[_buyPunchPowerCount];
            _buyPunchPowerCount++;
            _punchPowerCount++;
            _nextPunchPowerBuyText.text = _buyPunchPower[_buyPunchPowerCount].ToString();
            _scaleAppleText.text = _punchPower[_punchPowerCount].ToString();
            _clickCountText.text = _spawnCount.ToString("f0");
        }
        else
        {
            Debug.Log("強化できない");
        }
    }
}
