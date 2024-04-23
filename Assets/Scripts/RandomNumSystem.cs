using System.Collections.Generic;
using UnityEngine;
public class RandomNumSystem : MonoBehaviour
{
    [SerializeField] List<int> weights = new List<int>();   // �d�ݐݒ�p�ϐ�
    /// <summary>�K�`�����s���\�b�h</summary>
    public int ChooseStart()
    {
        return Choose(weights);
    }

    /// <summary>���I���\�b�h</summary>
    public int Choose(List<int> weight)
    {
        float total = 0f;
        //�z��̗v�f��total�ɑ��
        for (int i = 0; i < weight.Count; i++)
        {
            total += weight[i];
        }
        //Random.value��0.1����1�܂ł̒l��Ԃ�
        float random = Random.value * total;
        //weight��random���傫������T��
        for (int i = 0; i < weight.Count; i++)
        {
            if (random < weight[i])
            {
                //�����_���̒l���d�݂��傫�������炻�̒l��Ԃ�
                return i;
            }
            else
            {
                //����weight�����������悤�ɂ���
                random -= weight[i];
            }
        }
        //�Ȃ�������Ō�̒l��Ԃ�
        return weight.Count - 1;
    }
}