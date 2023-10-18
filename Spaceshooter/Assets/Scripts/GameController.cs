using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{   
    public GameObject rocketShip;
    public GameObject[] hazards;

    private GameObject _hazard;
    private float _timer;
    private Transform _hazardSpawnConstraints;
    
    private const float Interval = 5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        Instantiate(rocketShip);
    }

    // Update is called once per frame
    void Update()
    {   
        _timer += Time.deltaTime;
        if (_timer >= Interval) // Should create a new hazard every five seconds
        {
            var value = Random.Range(0, 10);
            
            var _hazard = Instantiate(hazards[value], new Vector3(5, Random.Range(-5, 5), 0), Quaternion.identity);
            _hazard.transform.localScale = new Vector3(
                Random.Range(0.1f, 0.5f),
                Random.Range(0.1f, 0.5f),
                0);
            _hazard.GetComponent<Rigidbody>().AddForce(transform.forward * 2.43f, ForceMode.Acceleration);
            
            _timer = 0;
        }
    }
}
