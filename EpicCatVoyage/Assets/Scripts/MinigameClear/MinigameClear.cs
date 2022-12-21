using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StoreInfo.setFriendship(StoreInfo.getFriendship() + 10);
        StoreInfo.setCoin(StoreInfo.getCoin() + 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
