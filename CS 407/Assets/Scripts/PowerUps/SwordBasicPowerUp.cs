using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicPowerUp : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public PlayerLooking playerLooking;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerLooking = player.GetComponent<PlayerLooking>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            increaseMoveSpeed();
        }
    }

    public void increaseAttackSpeed(){
        playerLooking.decreaseTimeBTWAttack();
    }

    public void increaseAttackPower(){
        playerLooking.increaseBaseDamage();
    }

    public void increaseMoveSpeed(){
        playerController.increaseMovementSpeed();
    }

    public void increaseHealth(){
        playerController.increaseHealth();
    }
}
