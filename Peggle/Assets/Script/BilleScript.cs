using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
public class BilleScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float LaunchSpeed = 50;

    Action BilleOut;
    int amountBluePegs = 0;
    int amountOrangePegs = 0;
    int amountVioletPegs = 0;

    ScoreScript scoreScript;

    public AimScript aimScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreScript = FindFirstObjectByType<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(Vector3 Direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Direction * LaunchSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Pegs")
        {
            PegsScript pegsScript = collision.transform.GetComponent<PegsScript>();
            if (!pegsScript.Active)
            {
                if (pegsScript.type == PegsScript.PegsType.BLUE)
                {
                    amountBluePegs++;
                }
                else if (pegsScript.type == PegsScript.PegsType.ORANGE)
                {
                    amountOrangePegs++;
                }
                else if (pegsScript.type == PegsScript.PegsType.VIOLET)
                {
                    amountVioletPegs++;
                }
                scoreScript.ChangeMakeScore(amountBluePegs, amountOrangePegs, amountVioletPegs);
            }
            pegsScript.Activate(ref BilleOut);
        }
        else if(collision.transform.tag == "Out")
        {
            BilleOut?.Invoke();
            scoreScript.ChangeScore(amountBluePegs, amountOrangePegs, amountVioletPegs);
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Basket")
        {
            BilleOut?.Invoke();
            aimScript.RecupBille();
            scoreScript.ChangeScore(amountBluePegs, amountOrangePegs, amountVioletPegs);
            Destroy(gameObject);
        }
    }
}
