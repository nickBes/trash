using UnityEngine.UI;
using UnityEngine;
using System;

public class Score : MonoBehaviour{
    public Text time;
    public GameObject winPanel;
    public GameObject losePanel;
    public int scoreFactor;
    bool panelCreated;
    float countTime = 0;
    PlayerMovement player;
    void Start(){
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        countTime = 0;
    }
    void Update(){
        if (!player.finished && !player.dead) {
            countTime += Time.deltaTime;
            time.text = ((float)Math.Round((decimal)countTime, 2)).ToString();
        } else if (player.dead) {
            if (!panelCreated) {
                Instantiate(losePanel, transform);
                panelCreated = true;
            }
        } else {
            if (!panelCreated) {
                GameObject temp = Instantiate(winPanel, transform);
                temp.GetComponent<winPanel>().setScore(((float)Math.Round(1/countTime * scoreFactor, 2)).ToString());
                panelCreated = true;
            }
        }
    }
}
