using UnityEngine;

public class InvadersGameData : MonoBehaviour {
    private const int invaderQty = 64;
    private const int invaderRowQty = 16;
	private const float descendDistance = 1.0f;
    private static bool[] armedInvaders;
	public static float invadersDirection;
	public static bool directionChanged;
	public Invader invaderPrefab;
	
	// Use this for initialization
	void Start () 
    {
		invadersDirection = 1.0f;
		directionChanged = false;
		
		// Instantiates prefabs in a 16x4 grid
		int gridX = 16;
		int gridY = 4;
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
				i++;
	        }
	    }
		
		// Sets invaders missile launch capabilities, initially only bottom row can fire
        for (i = 0; i < invaderQty - invaderRowQty; i++) 
        {
            armedInvaders[i] = false;
        }

        for (i = invaderRowQty * 3; i < invaderQty; i++)
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
	
    public void changeArmedStatus(int invader, bool status)
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
}
