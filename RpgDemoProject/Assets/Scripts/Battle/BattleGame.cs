using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGame : MonoBehaviour
{
    private bool _pause = false;
	private void Awake()
	{
		
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_pause)
            return;

        UpdateGameInput();
    }

    void UpdateGameInput()
	{
        var dir = KeyBoardUtils.GetKeyBoardMoveDirection();
	}
}
