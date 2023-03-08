using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerScript : MonoBehaviour
{ //Select the tower the script is applied
    public TowerType Type;  

    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;

    private TowerSystem _ts;

    private CircleCollider2D TowerCollider;
    //___________________________________________________________________________

    public bool isSettingUp;

    private void Awake()
    {
        _ts = GetComponent<TowerSystem>();
    }

    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        TowerCollider = this.GetComponent<CircleCollider2D>();

        // WHEN THE TOWER IS SET IT START THE SET UP MODE
        isSettingUp = true;
        UpdateRange(_ts.range);

    }


    private void Update()
    {
        if (isSettingUp.Equals(true))
        {
            //ACCED TO THE POSITION OF THE MOUSE

            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.transform.rotation = Quaternion.AngleAxis(angle,Vector3.back);
                isSettingUp = false;
            }
        }
    }

   
    // UPDATE THE RANGE OF THE LASER
    public void UpdateRange(float newRange)
    {
        transform.GetChild(1).transform.localScale += new Vector3(0f, newRange, 0f);
        print(transform.GetChild(1).name);
    }
}
