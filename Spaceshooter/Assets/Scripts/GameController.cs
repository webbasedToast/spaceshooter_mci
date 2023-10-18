using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{   
    public GameObject[] gameObjects;

    private GameObject _rocketShip;
    private GameObject _hazard;
    private Transform _hazardSpawnConstraints;
    private LineRenderer _lineRenderer;
    
    private float _timer;
    private int _points = 0;
    private int _damage = 0;
    
    private const float Interval = 5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rocketShip = Instantiate(gameObjects[0]);
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.1f, 0.1f);
        _lineRenderer.enabled = false;
    }
    
    // Update is called once per frame
    private void Update()
    {
        //SpawnHazards();
        ShipAttackAction();
    }

    private void ShipAttackAction()
    {
        // ToDo: Add hit tracking for hazards
        if (Input.GetMouseButtonDown(0))
        {
            _lineRenderer.SetPosition(0, _rocketShip.transform.position);
            _lineRenderer.SetPosition(1, GetCurrentMousePosition().GetValueOrDefault());
            _lineRenderer.enabled = true;
        }

        if (Input.GetMouseButtonUp(0)) _lineRenderer.enabled = false;
    }
    
    private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        return null;
    }
    
    private void SpawnHazards()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= Interval)) return; // Should create a new hazard every five seconds
        var value = Random.Range(1, 11);
            
        _hazard = Instantiate(gameObjects[value], new Vector3(5, Random.Range(-5, 5), 0), Quaternion.identity);
        _hazard.transform.localScale = new Vector3(
            Random.Range(0.1f, 0.5f),
            Random.Range(0.1f, 0.5f),
            0);
        _hazard.GetComponent<Rigidbody>().AddForce(transform.right * -2.43f, ForceMode.Impulse);
            
        _timer = 0;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10,10,2500,20),
            "Points:" + _points + " Health:" + (100-(_damage*25)) + "%");
    }
}