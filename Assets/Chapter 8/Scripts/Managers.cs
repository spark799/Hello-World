using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour {

    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> _startSequence;


    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        // Start each managers start up process
        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        // wait a second
        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        // until all modules are ready
        while(numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            // Check each status until they all of them are ready
            foreach (IGameManager manager in _startSequence)
            {
               if(manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if(numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }

            // wait a second before checking again
            yield return null;

        }

        Debug.Log("All Managers started up");

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
