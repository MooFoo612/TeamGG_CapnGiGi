using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform LvlBlock_1;
    #endregion

    void Awake() 
    {
        Instantiate(LvlBlock_1, new Vector3(-1, -5), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       
    }

   
}
