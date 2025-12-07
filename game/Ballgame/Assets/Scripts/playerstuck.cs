using UnityEditor.AnimatedValues;
using UnityEngine;

public class playerstuck : MonoBehaviour
{
    [SerializeField] GameObject playerball;
    [SerializeField] Vector3 playerpos;
    [SerializeField] float maxtime;
    [SerializeField] float timed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float rbforce;
    [SerializeField] float offsetDistance;
    
   


    // Update is called once per frame
    void Update()
    {       

        if(timed > 0)
        {
            timed -= Time.deltaTime;
        }
        else
        {
            timed = maxtime;

            float distance = Vector3.Distance(transform.position, playerpos);
            
            if (distance < offsetDistance)
            {
                rb.AddForce(Vector2.up * rbforce);
            }
            playerpos = transform.position;
            
        }
    }
}
