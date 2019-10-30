using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RoomManager : MonoBehaviour
{
    public double experience;
    public int level;
    public double expThreshold;
    public int score;
    public GameObject menu;

    //Each index of this array corresponds to how much the respective stat in the stats array should be incremented by in the level up function
    //This way we can take care of levelling up just with a loop
    public int[] increments = new int[] { 5, 5, 1, 1, 2, 5 };

    // Stats in order of index: Health, Max Health, Attack Power, Attack Speed, Movement Speed, Shield
    public int[] stats = new int[] { 100, 100, 5, 2, 4, 20 };

    private BoxCollider2D boxCollider;

    private RaycastHit2D hit;

    public int gold = 0;

    public int keys = 0;

    public TextMeshPro GoldText, KeyText, ScoreText;
    void Start()
    {
        level = 1;
        expThreshold = 30;
        experience = 0;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gold")
        {
            gold += other.gameObject.GetComponent<Gold>().amount;
            Destroy(other.gameObject);
            print("Picked up gold, Amount: " + gold.ToString());
        }
        else if (other.tag == "Key")
        {
            keys++;
            Destroy(other.gameObject);
            print("Picked up a key");
        }
        else if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
            print("Picked up a powerup");
        }
    }
    void FixedUpdate()
    {
              //Level ups
        if (experience >= expThreshold)
        {
            experience -= expThreshold;
            levelUp();
        }
        //updating gold amount
        GoldText.text = gold.ToString();

        //updating keys amount
        KeyText.text = keys.ToString();

        //updating score amount
        ScoreText.text = "Score: " + score.ToString();

        if (stats[0] <= 0)
        {
            menu.SendMessage("Pause");
        }
    }

    public double adjustThreshold()
    {
        return expThreshold * 1.3;
    }

    public void levelUp()
    {
        //increment player's level
        level++;
        //Loop through the stats array and increment each one by its corresponding index in the increment array
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] += increments[i];
        }

        //Set a new exp threshold
        expThreshold = adjustThreshold();
    }

    public void IncreaseScore(int s)
    {
        score += s;
    }

    public void addExperience(int x)
    {
        experience += x;
    }

    public void DamagePlayer(float damage)
    {
        stats[0] = stats[0] - (int)damage;
        print("Player: Damaged 20");
    }
}
