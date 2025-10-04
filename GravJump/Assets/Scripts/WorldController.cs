using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldController : MonoBehaviour
{
    
    public static float GravityConst = 0.5f;

    [SerializeField] private List<MassiveBody> massiveBodySamples;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            var x = Random.Range(-100, 100);
            var z = Random.Range(-100, 100);
            var position = new Vector3(x, 0, z);
            var newBody = Instantiate(massiveBodySamples[Random.Range(0,massiveBodySamples.Count-1)], position, Quaternion.identity);
            var direction = Vector3.Cross((newBody.transform.position - transform.position).normalized, Vector3.up);
            var body = newBody.GetComponent<Rigidbody>();
            var r = Vector3.Distance(body.transform.position, transform.position);
            var orbitalSpeed = Mathf.Sqrt(GravityConst  * GetComponent<Rigidbody>().mass / r);
            body.linearVelocity = direction * orbitalSpeed;
        }
        massiveBodySamples.ForEach(element => element.gameObject.SetActive(false));;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
