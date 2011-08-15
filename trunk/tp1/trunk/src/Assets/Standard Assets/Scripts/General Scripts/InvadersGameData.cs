using UnityEngine;

public class InvadersGameData : MonoBehaviour {
    private const int invaderQty = 64;
    private const int invaderRowQty = 12;
    private bool[] armedInvaders;
	public Invader invaderPrefab;
	
	// Use this for initialization
	void Start () 
    {
		// Instantiates a prefab in a grid
		int gridX = 16;
		int gridY = 4;
		int i = 0;
		float spacing = 3.0f;
		Invader clone;
		
		armedInvaders = new bool[invaderQty];
		
	    for (int y = 0; y < gridY ; y++) 
		{
	        for (int x = 0 ; x <gridX ; x++) 
			{
	            Vector3 pos = new Vector3(-9.411484f + x * spacing, 41.72719f - y * spacing, 35.47127f);
	            clone = Instantiate(invaderPrefab, pos, Quaternion.Euler(270.0f,0f,0f)) as Invader;
				clone.InvaderID = i;
				i++;
	        }
	    }
		
        for (i = 0; i < invaderQty - invaderRowQty; i++) 
        {
            armedInvaders[i] = false;
        }

        for (i = invaderRowQty * 3; i < invaderRowQty; i++)
        {
            armedInvaders[i] = true;
        }
	}
    // Update is called once per frame
    void Update()
    {
	}
	
    public bool canFire(int invaderID)
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
}
