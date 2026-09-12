using UnityEngine;
using UnityEngine.InputSystem;

public class AimScript : MonoBehaviour
{
    [SerializeField]
    float Offset;
    [SerializeField]
    GameObject BilleObject;
    [SerializeField]
    Transform ShootPosition;

    ScoreScript scoreScript;

    int BilleAmount =10 ;

    Transform BilleTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreScript = FindFirstObjectByType<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        Vector3 direction = MousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle += Offset;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if(Mouse.current.leftButton.wasPressedThisFrame && BilleTransform == null && BilleAmount>0 && !GameManager.instance.EndGame)
        {
            BilleTransform = Instantiate(BilleObject,ShootPosition.position,Quaternion.identity).transform;
            BilleTransform.GetComponent<BilleScript>().Launch(direction);
            BilleTransform.GetComponent<BilleScript>().aimScript = this;
            BilleAmount--; 
            scoreScript.ChangeBall(BilleAmount);
        }
    }

    public void RecupBille()
    {
        BilleAmount++;
        scoreScript.ChangeBall(BilleAmount);
    }

    public bool HadBall()
    {
        return BilleAmount > 0;
    }
}
