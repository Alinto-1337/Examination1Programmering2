using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterVirus : MonoBehaviour
{

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.005f);        
    }
}
