using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerScript : MonoBehaviour
{ //Select the tower the script is applied
    public TowerType Type;  

    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;

    private Enemy _enemyS;
    private ObjectPooler _objPool;
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
        _enemyS = FindObjectOfType<Enemy>();
        _objPool = ObjectPooler.Instance;
        TowerCollider = this.GetComponent<CircleCollider2D>();


        isSettingUp = true;
        UpdateRange(_ts.range);

    }


    private void Update()
    {
        if (isSettingUp.Equals(true))
        {
            //Acede a la posicion del mouse cuando hace click

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

   

    public void UpdateRange(float newRange)
    {
        TowerCollider.radius = newRange;
    }
}
