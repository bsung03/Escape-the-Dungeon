using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using st;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public double experience;
    public int level;
    public double expThreshold;
    public GameObject menu;
    
    private Bullet lazer;
    public int points;
    public int points1;
    public int points2;
    public TextMeshProUGUI point;
    public TextMeshProUGUI point1;
    public TextMeshProUGUI point2;

    //Each index of this array corresponds to how much the respective stat in the stats array should be incremented by in the level up function
    //This way we can take care of levelling up just with a loop
    public int[] increments = new int[] { 5, 5, 1, 1, 2, 5 };

    // Stats in order of index: Health, Max Health, Attack Power, Attack Speed, Movement Speed, Shield
    public int[] stats = new int[] { 100, 100, 5, 2, 4, 20 };

    [System.Serializable]
    public class Gunnerstats
    {
        public int Health = 100;
        public int MaxHealth = 100;
        public float Range = 0.3f;
        public int AttackPower = 1;
        public int MoveSpeed;
    }
    public Gunnerstats g;


    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    public float moveSpeed = 5f;

    private RaycastHit2D hit;

    public int gold, keys, score;

    public TextMeshPro GoldText, KeyText, ScoreText;

    private Vector3 lastMoveDir;
    GameObject[] potenial_menus;
    // Start is called before the first frame update
    void Start()
    {
        g = new Gunnerstats();
        lazer = new Bullet();
        points = 0;
        points1 = 0;
        points2 = 0;

        if (Instance != null)
        {
            //There's already another player don't create a new one
            Destroy(gameObject);
            return;
        }
        //else
        // this is the one and only player
        Instance = this;
        DontDestroyOnLoad(gameObject);
        level = 1;
       expThreshold = 30;
       experience = 0;
       boxCollider = GetComponent<BoxCollider2D>();
       lastMoveDir = new Vector3(0,0,0);
       keys = 8;
       gold = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        point.SetText(points.ToString());
        point1.SetText(points1.ToString());
        point2.SetText(points2.ToString());

        moveDelta = new Vector3(x,y,0);

        if(moveDelta.x < 0){
            transform.localScale = Vector3.one;
        }else if(moveDelta.x > 0){
            transform.localScale = new Vector3(-1,1,1);
        }


        //movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveSpeed * moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            transform.Translate(0,moveDelta.y * Time.deltaTime * moveSpeed, 0);
            //print("PLAYER MOVE");
        }
        else if (hit.collider.tag == "Player")
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * moveSpeed, 0);
            //print("PLAYER MOVE");
        }
        else
        {
            //Debug.Log("blocker");
            print("PLAYER Block"+ hit.collider.tag);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs( moveSpeed * moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0,0);
            //print("PLAYER MOVE");
        } else if(hit.collider.tag == "Player")
        {
            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0, 0);
            //print("PLAYER MOVE");
        }
        else
        {
            //Debug.Log("blocker");

            print("PLAYER Block" + hit.collider.tag);
        }



        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveSpeed * moveDelta.y * Time.deltaTime), LayerMask.GetMask("collect"));
        if (hit.collider == null)
        {

        }
        else
        {
            if (hit.collider.tag == "Gold")
            {
                gold += hit.collider.gameObject.GetComponent<Gold>().amount;
                Destroy(hit.collider.gameObject);
                print("Picked up gold, Amount: " + gold.ToString());
            }
            else if (hit.collider.tag == "Key")
            {
                keys++;
                Destroy(hit.collider.gameObject);
                print("Picked up a key");
            }
            else if (hit.collider.tag == "Powerup")
            {
                Destroy(hit.collider.gameObject);
                print("Picked up a powerup");
            }
            else if (hit.collider.tag == "PowerUp1")
            {
                Destroy(hit.collider.gameObject);
                g.AttackPower++;
                g.Range += 0.1f;

                Shooting.damage = g.AttackPower;
                Shooting.range = g.Range;
                ss();
                ss();
            }
            else if (hit.collider.tag == "PowerUp2")
            {
                Destroy(hit.collider.gameObject);

                Shooting.sskill = true;
            }
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveSpeed * moveDelta.x * Time.deltaTime), LayerMask.GetMask("collect"));
        if (hit.collider == null)
        {
           
        }
        else
        {
            if (hit.collider.tag == "Gold")
            {
                gold += hit.collider.gameObject.GetComponent<Gold>().amount;
                Destroy(hit.collider.gameObject);
                print("Picked up gold, Amount: " + gold.ToString());
            }
            else if (hit.collider.tag == "Key")
            {
                keys++;
                Destroy(hit.collider.gameObject);
                print("Picked up a key");
            }
            else if (hit.collider.tag == "Powerup")
            {
                Destroy(hit.collider.gameObject);
                print("Picked up a powerup");
            }
            else if (hit.collider.tag == "PowerUp1")
            {
                Destroy(hit.collider.gameObject);
                g.AttackPower++;
                g.Range += 0.1f;

                Shooting.damage = g.AttackPower;
                Shooting.range = g.Range;
                ss();
                ss();
            }
            else if (hit.collider.tag == "PowerUp2")
            {
                Destroy(hit.collider.gameObject);

                Shooting.sskill = true;
            }
        }

        //Level ups
        if (experience >= expThreshold) {
            experience -= expThreshold;
            levelUp();
            gunnerLevelup(g);
            points++;
        }

        /*if(GoldText == null)
        {
           //FIX -> GoldText =  GameObject.Find("Hand").GetComponent<TextMeshPro>();
        }
        else
        {
            GoldText.text = gold.ToString();
        }

        //updating gold amount
        if (KeyText == null)
        {
            KeyText =  GameObject.Find("Keytxt").GetComponent<TextMeshPro>();
        }
        else
        {
            KeyText.text = keys.ToString();
        }

        if (ScoreText == null)
        {
            ScoreText = GameObject.Find("Score").GetComponent<TextMeshPro>();
        }
        else
        {
            ScoreText.text = "Score: " + score.ToString();
        }
*/
        /*
        if(menu == null)
        {

            potenial_menus = GameObject.FindGameObjectsWithTag("menu");
            print("menu search: "+potenial_menus.Length.ToString());
            for (int i = 0; i < potenial_menus.Length; i++)
            {
                if(potenial_menus[i].name == "Menu" && potenial_menus[i].activeSelf)
                {
                    
                    menu = potenial_menus[i];
                } else if(potenial_menus[i].name == "Menu")
                {
                    print("Menu not active");
                }
            }
        }
        else
        {
            if(menu.activeSelf == false)
            {
                potenial_menus = GameObject.FindGameObjectsWithTag("menu");
                print("menu search: " + potenial_menus.Length.ToString());
                for (int i = 0; i < potenial_menus.Length; i++)
                {
                    if (potenial_menus[i].name == "Menu" && potenial_menus[i].activeSelf)
                    {
                        menu = potenial_menus[i];
                    }
                    else if (potenial_menus[i].name == "Menu")
                    {
                        print("Menu not active");
                    }
                }
            }
        }
        */

        if (stats[0] <= 0)
        {
            menu.SendMessage("Pause");
        }

        
    }

    public double adjustThreshold() {
        return expThreshold * 1.3;
    }

    public void levelUp() {
        //increment player's level
        level++;
        //Loop through the stats array and increment each one by its corresponding index in the increment array
        for (int i = 0; i < stats.Length; i++) {
            stats[i] += increments[i];
        }

        //Set a new exp threshold
        expThreshold = adjustThreshold();
    }

    public void gunnerLevelup(Gunnerstats g)
    {
      //  level++;
       // expThreshold = adjustThreshold();
        
        g.Health++;
        g.MaxHealth++;
        g.MoveSpeed++;
        g.AttackPower++;
        g.Range += 0.07f;

        Shooting.damage = g.AttackPower;
        Shooting.range = g.Range;
        ss();
        
    }
    public void ss()
    {
        if (Shooting.cooldown >= 0.2f)
        {
            Shooting.cooldown -= 0.1f;
        }
    }
    public void MskillPoint()
    {
        if (Shooting.skillcooldown1 >= 0 && points > 0)
        {
            if (points1 < 5)
            {
                Shooting.skillcooldown1 -= 0.5f;
                points--;
                points1++;
                point.SetText(points.ToString());
                point1.SetText(points1.ToString());
            }
            if (points1 >= 5)
            {
                Shooting.tele = true;
            }

        }
    }
    public void AskillPoint()
    {
        if (Shooting.skillcooldown >= 0 && points > 0)
        {
            if (points2 < 5)
            {
                Shooting.skillcooldown -= 0.5f;
                points--;
                points2++;
                point.SetText(points.ToString());
                point2.SetText(points2.ToString());
            }
            if (points2 >= 5)
            {
               
            }
            
        }
    }

    public void IncreaseScore(int s){
        score += s;
    }

    public void addExperience(int x) {
        experience += x;
    }

    public void DamagePlayer(float damage)
    {
        stats[0] =  stats[0] - (int) damage;
        print("Player: Damaged 20");
        print("Player: Health: " + stats[0].ToString());
    }

    public void increaseMovementSpeed(){
        moveSpeed += 1;
    }

    public void increaseHealth(){
        stats[0] += 1;
    }

}
