using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Gamepad gamePad;

    private bool _isGoal = false;

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public int mapLevel = 1;

    [SerializeField]
    private GameObject[] obj;

    private void Awake()
    {

        gamePad = GetComponent<Gamepad>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //gamePad = GetComponent<Gamepad>();
    }

    private void Update() {

        if(_isGoal)
        {
            // クリアしたらSelectへ遷移
            if (gamePad.startButton.trigger)
            {
                _isGoal = false;
                SceneManager.LoadScene("Select");
            }
        }
        else
        {
            // デバック用にSelectへ遷移
            if (gamePad.backButton.trigger)
            {
                SceneManager.LoadScene("Select");
            }
        }
        Pose();
    }

    private void isClear()
    {
        _isGoal = true;
    }

    private void isFail(GameObject failObject)
    {
        failObject.GetComponent<Character>().Restart();
    }
    private void OnGUI()
    {
        if (_isGoal)
        {
            //if (GUI.Button(new Rect(Screen.width / 2, 100, 450, 100), "Clear"))
            GUI.TextArea(new Rect(Screen.width / 2F, Screen.height / 2F, 75, 50), "Clear!");
        }
    }

    private void Pose()
    {
        if(gamePad.startButton.down)
        {
            for(int i=0;i<obj.Length;i++)
            {
                if (i <2)
                {
                    obj[i].GetComponent<Player>().enabled = !obj[i].GetComponent<Player>().enabled;
                    obj[i].GetComponent<Rigidbody>().isKinematic = !obj[i].GetComponent<Rigidbody>().isKinematic;
                }
                else if(i==2)
                {
                    obj[i].GetComponent<Rigidbody>().isKinematic = !obj[i].GetComponent<Rigidbody>().isKinematic;
                    obj[i].GetComponent<Child>().enabled = !obj[i].GetComponent<Child>().enabled;
                }
                else
                    obj[i].GetComponent<LinkedPlayer>().enabled = !obj[i].GetComponent<LinkedPlayer>().enabled;
            }
        }
    }

}
