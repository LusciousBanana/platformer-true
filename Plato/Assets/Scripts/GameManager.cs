using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text finishText;
    PlayerControl PC;
    // Start is called before the first frame update
    void Start()
    {
        PC = PC.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        finishText.text = PC.score.ToString();
    }
}
