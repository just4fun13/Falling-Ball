using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    Vector2 startPosition = Vector2.zero;
    Vector2 position = Vector2.zero;
    [SerializeField]
    Transform morphT;
    [SerializeField]
    Transform morphT2;
    
    Transform curTransform;
    // Start is called before the first frame update
    void Start()
    {       
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        curTransform = morphT;
    }

    public void OnPointerDown (BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData) data;
        startPosition = pointer.position;
    }

    public void OnDrag ( BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData) data;
        position = pointer.position - startPosition;
//        morphT.rotation = Quaternion.Euler (position.y/10f, 0f, -position.x/10f);
        curTransform.Rotate (Time.deltaTime * 0.1f * new Vector3(position.x , -position.y, 0f));
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != curTransform)
        {
            if (curTransform == morphT)
            {
                morphT.rotation = Quaternion.identity;
                morphT.position = new Vector3 (morphT2.position.x, morphT2.position.y, morphT2.position.z - 17f);
            }
            else
            {
                morphT2.rotation = Quaternion.identity;
                morphT2.position = new Vector3 (morphT.position.x, morphT.position.y + 17f, morphT.position.z - 17f); 
            }
            curTransform = collision.transform;
            Debug.Log ("Time has changed");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
