using System.Collections;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds pause; // ���ڿ��ƶ�����ͣ�ĵȴ�ʱ��
    private bool isAnimating = false; // ����Ƿ����ڲ��Ŷ���

    private void Awake()
    {
        pause = new WaitForSeconds(0.04f); // ��ʼ���ȴ�ʱ��Ϊ0.04��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAnimating == false) // ���û�����ڲ��Ŷ���
        {
            if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock()); // ������ʱ����ת����
            }
            else
            {
                StartCoroutine(RotateClock()); // ����˳ʱ����ת����
            }

            // �����ײ����ı�ǩ��"Player"���򲥷�rustle����
            //if (collision.gameObject.tag == "Player")
            //{
            //    AudioManager.Instance.PlaySound(SoundName.effectRustle);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAnimating == false) // ���û�����ڲ��Ŷ���
        {
            if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock()); // ������ʱ����ת����
            }
            else
            {
                StartCoroutine(RotateClock()); // ����˳ʱ����ת����
            }

            // �����ײ����ı�ǩ��"Player"���򲥷�rustle����
            //if (collision.gameObject.tag == "Player")
            //{
            //    AudioManager.Instance.PlaySound(SoundName.effectRustle);
            //}
        }
    }

    private IEnumerator RotateAntiClock()
    {
        isAnimating = true; // ���Ϊ���ڲ��Ŷ���

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f); // ��ʱ����ת2��

            yield return pause; // �ȴ�һ��ʱ��
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f); // ��ʱ����ת-2��

            yield return pause; // �ȴ�һ��ʱ��
        }

        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f); // ��ʱ����ת2��

        yield return pause; // �ȴ�һ��ʱ��

        isAnimating = false; // ���Ϊ�������Ž���
    }

    private IEnumerator RotateClock()
    {
        isAnimating = true; // ���Ϊ���ڲ��Ŷ���

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f); // ˳ʱ����ת-2��

            yield return pause; // �ȴ�һ��ʱ��
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f); // ˳ʱ����ת2��

            yield return pause; // �ȴ�һ��ʱ��
        }

        gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f); // ˳ʱ����ת-2��

        yield return pause; // �ȴ�һ��ʱ��

        isAnimating = false; // ���Ϊ�������Ž���
    }
}