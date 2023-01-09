using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public TextMeshProUGUI towerlvl;

    private TowerSystem Ts;


    private void Awake()
    {
        Ts = FindObjectOfType<TowerSystem>();
    }
    private void Start()
    {
        towerlvl.text = Ts.currentLvl.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * 2 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * 2 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * 2 * Time.deltaTime);
        }
    }
}
