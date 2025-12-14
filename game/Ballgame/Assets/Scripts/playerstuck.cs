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
    [SerializeField] int Options;
    
   


    // Update is called once per frame
    void Update()
    {       

        if(timed > 0)
        {
            timed -= Time.deltaTime;
        }
        else
        {
            timed = Random.Range(maxtime, maxtime + 0.5f);

            float distance = Vector3.Distance(transform.position, playerpos);
            
            if (distance < offsetDistance)
            {
                rb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1) * rbforce);
            }
            playerpos = transform.position;
            
        }
    }
}
