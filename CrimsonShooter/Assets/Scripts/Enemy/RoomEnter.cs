using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{ 
    public void EnterRoom(int num)
    {
        IdleState.EnemyEnterRoom(num);
    }
}
