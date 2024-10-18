using System.Collections;
using UnityEngine;

public class LastCandel : MonoBehaviour, IItem
{
    SpriteRenderer[] CandleFire;
    [SerializeField] ParticleSystem FireParticle;
    [SerializeField] Light[] Firelights;
    [SerializeField] ExitDoor exitDoor;
    Collider Candle_Collider;

    [SerializeField] private float Timer;

    private readonly string[] LastCandleTalk = {
        "'���� �̷� ��ҿ��� �Ǹ� ��ȯ�ϰ� �ִ�����..?'",
        "'�������� ����Ҹ��� �鸮�±�.. ���� ������ �̰��� ��� ������ ������ھ�..'",
        "'�������� ������ �߾��� �ϴ� �ǰ�.. ���ڱ� �Ҹ��� ���� ��������. �����ؾ߰ڱ�.'"
    };
    void Start()
    {
        exitDoor = GameObject.Find("MainDoor").GetComponent<ExitDoor>();
        FireParticle = transform.GetChild(3).GetComponent<ParticleSystem>();
        Firelights = GetComponentsInChildren<Light>();
        CandleFire = GetComponentsInChildren<SpriteRenderer>();
        Candle_Collider = GetComponent<Collider>();

        Timer = 60;
    }
    IEnumerator StartTimer()
    {
        while (Timer > 0) // Timer�� 0���� ū ���� �ݺ�
        {
            yield return new WaitForSeconds(1.0f);
            Timer -= 1.0f;
            InGameUIManager.instance.SetTimer(Timer);
            
            if(Timer <= 0)
            {
                yield return new WaitForSeconds(0.5f);
                GameManager.G_instance.CtrlLastLight(true);
                exitDoor.OpenCololider();
            }
            Debug.Log(Timer);
        }
    }

    public void ItemUIOn()
    {
        InGameUIManager.instance.SetPlayerUI_Text("���ʲ��� [G]");
    }

    public void CatchItem()
    {
        foreach (var candle in CandleFire)
            candle.enabled = false;

        Candle_Collider.enabled = false;
        FireParticle.Stop();
        foreach (var candle in Firelights)
        {
            candle.enabled = false;
        }
        InGameUIManager.instance.OnMisson("���� �ð����� ��Ƴ�������.");
        InGameUIManager.instance.OnTimer(true);
        InGameUIManager.instance.AutoSetTalk(LastCandleTalk);
        StartCoroutine(StartTimer());
    }

    public void Use()
    {
       //�κ��丮���� ����ϴ� ������Ʈ �ƴ� ���� x
    }
}