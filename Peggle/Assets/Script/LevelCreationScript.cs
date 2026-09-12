using System.Collections.Generic;
using UnityEngine;

public class LevelCreationScript : MonoBehaviour
{
    [SerializeField]
    Transform PegsList;
    List<Transform> BluePegs;
    List<Transform> OrangePegs;
    [SerializeField]
    Transform violetPegs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BluePegs = new List<Transform>();
        foreach (Transform t in PegsList)
        {
            BluePegs.Add(t);

            t.GetComponent<PegsScript>().pegsDestroyed += UpdateTab;
        }
        OrangePegs = new List<Transform>();
        while (OrangePegs.Count < 25)
        {
            foreach (Transform t in BluePegs.ToArray())
            {
                if (OrangePegs.Count < 25 && Random.Range(0, 10) < 3)
                {
                    OrangePegs.Add(t);
                    BluePegs.Remove(t);
                    t.GetComponent<SpriteRenderer>().color = Color.red;
                    t.GetComponent<PegsScript>().type = PegsScript.PegsType.ORANGE;
                }
            }
        }
        foreach (Transform t in BluePegs)
        {
            t.GetComponent<SpriteRenderer>().color = Color.blue;
            t.GetComponent<PegsScript>().type = PegsScript.PegsType.BLUE;
        }

        int position = Random.Range(0, BluePegs.Count);
        violetPegs = BluePegs[position];
        BluePegs.RemoveAt(position);
        violetPegs.GetComponent<SpriteRenderer>().color = Color.darkViolet;
        violetPegs.GetComponent<PegsScript>().type = PegsScript.PegsType.VIOLET;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateTab(Transform pegsTransform)
    {
        if (BluePegs.Contains(pegsTransform))
        {
            BluePegs.Remove(pegsTransform);
        }
        else if (OrangePegs.Contains(pegsTransform))
        {
            OrangePegs.Remove(pegsTransform);
        }
    }

    public bool FinishGame()
    {
        return OrangePegs.Count == 0;
    }

    public void NewRound()
    {
        if (violetPegs)
        {
            violetPegs.GetComponent<SpriteRenderer>().color = Color.blue;
            violetPegs.GetComponent<PegsScript>().type = PegsScript.PegsType.BLUE;
            BluePegs.Add(violetPegs);

        }
        int position = Random.Range(0, BluePegs.Count);
        violetPegs = BluePegs[position];
        BluePegs.RemoveAt(position);
        violetPegs.GetComponent<SpriteRenderer>().color = Color.darkViolet;
        violetPegs.GetComponent<PegsScript>().type = PegsScript.PegsType.VIOLET;
    }
}
