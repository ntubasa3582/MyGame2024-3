using System.Collections.Generic;
using UnityEngine;
public class RandomNumSystem : MonoBehaviour
{
    [SerializeField] List<int> weights = new List<int>();   // 重み設定用変数
    /// <summary>ガチャ実行メソッド</summary>
    public int ChooseStart()
    {
        return Choose(weights);
    }

    /// <summary>抽選メソッド</summary>
    public int Choose(List<int> weight)
    {
        float total = 0f;
        //配列の要素をtotalに代入
        for (int i = 0; i < weight.Count; i++)
        {
            total += weight[i];
        }
        //Random.valueは0.1から1までの値を返す
        float random = Random.value * total;
        //weightがrandomより大きいかを探す
        for (int i = 0; i < weight.Count; i++)
        {
            if (random < weight[i])
            {
                //ランダムの値より重みが大きかったらその値を返す
                return i;
            }
            else
            {
                //次のweightが処理されるようにする
                random -= weight[i];
            }
        }
        //なかったら最後の値を返す
        return weight.Count - 1;
    }
}