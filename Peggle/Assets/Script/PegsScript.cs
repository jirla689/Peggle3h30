using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PegsScript : MonoBehaviour
{
    public enum PegsType
    {
        BLUE=10,
        ORANGE=100,
        VIOLET=500
    }

    public PegsType type;
    public bool Active = false;

    public Action<Transform> pegsDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(ref Action actionDestroy)
    {
        if (!Active)
        {
            Active = true;
            actionDestroy += DestroyPegs;
            switch (type)
            {
                case PegsType.BLUE:
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    break;
                case PegsType.ORANGE:
                    GetComponent<SpriteRenderer>().color = Color.gold;
                    break;
                case PegsType.VIOLET:
                    GetComponent<SpriteRenderer>().color = Color.violet;
                    break;
                default:
                    break;
            }
        }
    }

    public void DestroyPegs()
    {
        pegsDestroyed?.Invoke(transform);
        Destroy(gameObject);
    }
}
