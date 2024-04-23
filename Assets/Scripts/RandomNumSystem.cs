using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomNumSystem : MonoBehaviour
{
    [FormerlySerializedAs("weights")] [SerializeField] List<float> _weights = new List<float>(); // �d�ݐݒ�p�ϐ�

    /// <summary>�K�`�����s���\�b�h</summary>
    public int ChooseStart()
    {
        return Choose(_weights);
    }

    /// <summary>���I���\�b�h</summary>
    int Choose(List<float> weight)
    {
        float total = 0f;
        //�z��̗v�f��total�ɑ��
        foreach (var t in weight)
        {
            total += t;
        }

        //Random.value��0.1����1�܂ł̒l��Ԃ�
        float random = Random.value * total;
        //weight��random���傫������T��
        for (int i = 0; i < weight.Count; i++)
        {
            if (random < weight[i]) return i; //�����_���̒l���d�݂��傫�������炻�̒l��Ԃ�
            else random -= weight[i]; //����weight�����������悤�ɂ���
        }

        //�Ȃ�������Ō�̒l��Ԃ�
        return weight.Count - 1;
    }
}