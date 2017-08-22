using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text marmotCounter;

    private List<HammerSlot> slots = new List<HammerSlot>();
    private List<Marmot> marmots;
    private Timer clock;

    private Animator marmotCounterAnim;
    private int totalHits = 0;

	void Start ()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HammerSlot");
        marmots = new List<Marmot>(GetComponentsInChildren<Marmot>());

        int index = 0;

        while(index < objs.Length)
        {
            HammerSlot hammerSlot = objs[index].GetComponent<HammerSlot>();
            slots.Add(hammerSlot);

            hammerSlot.Spawn();
            index++;
        }

        marmotCounterAnim = marmotCounter.GetComponent<Animator>();

        clock = Timer.Instance;
        clock.Play();

        StartMarmots();
	}

    private void StartMarmots()
    {
        ChooseOne();
    }

    private void ChooseOne()
    {
        int index = 0;

        List<Marmot> shuffleBag = new List<Marmot>();

        while(index < marmots.Count)
        {
            if (marmots[index].IsHide())
            {
                shuffleBag.Add(marmots[index]);
            }

            index++;
        }

        float nextMarmotTime = Random.Range(4f, 12f);

        if (shuffleBag.Count > 0)
        {
            float lifeTime = Random.Range(nextMarmotTime + (nextMarmotTime * 0.5f), nextMarmotTime - (nextMarmotTime * 0.5f));
            shuffleBag[Random.Range(0, shuffleBag.Count - 1)].GoOut(lifeTime);
        }
    }

    public void CountMarmot()
    {
        totalHits++;
        marmotCounter.text = string.Format("{0:00}", totalHits);
        marmotCounterAnim.SetTrigger("Pop");
    }

    public void NextMarmotShuffle(int marmotNum)
    {
        int count = 0;

        while(count < marmotNum)
        {
            ChooseOne();
            count++;
        }
    }

    void Update () {

	}

}
