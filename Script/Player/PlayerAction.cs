using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Image Gauge;
    PlayerController playerController;
    [SerializeField]
    BuildingBase ColliderBuilding;
    public enum ActionType
    {
        Attack, Construction, Farming, Harvest
    }
    public ActionType actionType;
    public BuildingBase InterActionBuilding;
    [SerializeField]
    bool isAction;
    void Start()
    {
        Gauge.fillAmount = 0;
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(actionType);
            if (actionType != ActionType.Harvest)
                actionType++;
            else
                actionType = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(!isAction)
            Action(actionType);
        }
        BeforeAction(actionType);
    }
    void BeforeAction(ActionType _Action)
    {
        switch (_Action)
        {
            case ActionType.Attack:

                break;
            case ActionType.Construction:

                break;
            case ActionType.Farming:
                Vector3 dir = MapData.Instance.GetTilePos(transform.position);
                break;
            default:
                break;
        }
    }
    void Action(ActionType _Action)
    {
        switch (_Action)
        {
            case ActionType.Attack:

                break;
            case ActionType.Construction:

                break;
            case ActionType.Farming:
                StartCoroutine(GaugeSet(3));
                break;
            default:
                break;
        }
    }
    IEnumerator GaugeSet(float time)
    {
        isAction = true;
        Gauge.fillAmount = 0;
        Vector3 dir = MapData.Instance.GetTilePos(transform.position);
        Vector2 Dir = new Vector2(dir.x + playerController.SightVector.x,
            dir.y + playerController.SightVector.y);
        GridManager.Instance.DrawGrid(1, 1, Dir);
        Debug.Log("밭갈기");
        while (Gauge.fillAmount < 1)
        {
            Gauge.fillAmount += 1 / time * Time.deltaTime;
            yield return null;
        }
        GridManager.Instance.DeleteGrid();
        Gauge.fillAmount = 0;
        isAction = false;
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Building")
        {
            InterActionBuilding = collision.GetComponent<BuildingBase>();
            if (InterActionBuilding.GetComponent<IInteraction>() != null)
            {
                EventManager.Instance.PostNotification(EventType.CollisionBuilding, InterActionBuilding, InterActionBuilding);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Building")
        {
            if( collision.gameObject == InterActionBuilding.gameObject)
            {
                EventManager.Instance.PostNotification(EventType.UnCollisionBuilding, InterActionBuilding, InterActionBuilding);
                InterActionBuilding = null;
            }
        }
    }
}
