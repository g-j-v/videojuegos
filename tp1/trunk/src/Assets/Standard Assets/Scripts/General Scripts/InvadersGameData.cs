using UnityEngine;

public class InvadersGameData : MonoBehaviour {
    private int invaderQty;
    private const int invaderRowQty = 16;
	private const float descendDistance = 1.0f;
    private static bool[] armedInvaders;
	private static float score;
	public static float invadersDirection;
	public static bool directionChanged, gameLost;
	public static int remainingInvaders;
	public Invader invaderPrefab;
	
	// Use this for initialization
	void Start () 
    {
		invadersDirection = 1.0f;
		directionChanged = gameLost = false;
		
		// Instantiates prefabs in a 16x4 grid
		int gridX = 16;
		int gridY = 3;
		remainingInvaders = invaderQty = gridX * gridY;
		
		int i = 0;
		float spacing = 3.5f;
		Invader clone;
		
		armedInvaders = new bool[invaderQty];
		
	    for (int y = 0; y < gridY ; y++) 
		{
	        for (int x = 0 ; x <gridX ; x++) 
			{
				// first clone must not occupied column 0 of grid
				if (x == 0 && y == 0)
				{
					x = 1;
				}
	            Vector3 pos = new Vector3(-9.411484f + x * spacing, 41.72719f - y * spacing, 35.47127f);
	            clone = Instantiate(invaderPrefab, pos, Quaternion.Euler(270.0f,0f,0f)) as Invader;
				clone.InvaderID = i;
				clone.tag = "Enemy";
				i++;
	        }
	    }
		
		// Sets invaders missile launch capabilities, initially only bottom row can fire
        for (i = 0; i < invaderQty - invaderRowQty; i++) 
        {
            armedInvaders[i] = false;
        }

        for (i = invaderRowQty * 2; i < invaderQty; i++)
        {
            armedInvaders[i] = true;
        }
	}
    // Update is called once per frame
    void Update()
    {
	}
	
    public static bool canFire(int invaderID)
    {
        return armedInvaders[invaderID];
    }
	
	public static void notifyDecease(int invaderID)
	{
		int currRow = invaderID / invaderRowQty + 1;
		changeArmedStatus(invaderID, false);
		if (currRow > 1)
		{
			int triggerID = invaderID - invaderRowQty;
			
			// Evita que si se mata un invader de la fila del medio, el de arriba dispare si el de la 
			// tercera sigue vivo
			if (currRow != 2 || !canFire(invaderID + invaderRowQty))
			{
				changeArmedStatus(triggerID, true);
			}
		}
	}
	
    public static void changeArmedStatus(int invader, bool status)
    {
        if (invader >= 0)
        {
            armedInvaders[invader] = status;
            return;
        }
    }
	
	public static void descendInvaders()
	{
		Invader[] invaders =  FindObjectsOfType(typeof(Invader)) as Invader[];
        foreach (Invader inv in invaders) {
            inv.descend(descendDistance);
        }
	}
	
	public void OnPauseGame()
	{
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) 
		{
    		go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	public void OnResumeGame()
	{
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) 
		{
    		go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	public static float Score
	{
		get
		{
			return score;
		}
		set
		{
			if (value > 0)
			{
				score = value;
			}
		}	
	}
}
