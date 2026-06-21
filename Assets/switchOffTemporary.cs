using UnityEngine;

public class switchOffTemporary : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SwtichOff();
        }
    }


    private void SwtichOff()
    {
        gameObject.SetActive(false);
    }
}
