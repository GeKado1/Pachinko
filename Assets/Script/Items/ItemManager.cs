using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public List<Item> AllItems = new List<Item>();
    public List<Item> ActiveItems = new List<Item>();

    public static ItemManager Instance { get; private set; }

    private List<Pocket> PocketList = new List<Pocket>();
    private List<GameObject> BouncePinList = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SearchPockets();
        ModifyPockets();

        SearchBouncePins();
        UpgradeBouncePins();
    }

    private void SearchPockets()
    {
        
        GameObject[] pockets = GameObject.FindGameObjectsWithTag("Pocket");

        foreach (GameObject pocket in pockets)
        {
            if (pocket.GetComponent<Pocket>() != null)
            {
                PocketList.Add(pocket.GetComponent<Pocket>());
            }
        }
    }

    private void ModifyPockets()
    {
        foreach (Item item in ActiveItems)
        {
            if (item.ItemTag.Equals("PocketPasive"))
            {
                //Sorry por el switch, se tendria que modificar o hacer otro csv para los pocket items para evitarlo (y no nos vamos engañar, es trabajo extra y me da flojera y la jam es este miercoles, no da tiempo)
                switch (item.ItemName)
                {
                    case "HearthAce":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Hearth"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                            }
                        }
                        break;
                    case "DiamondAce":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Diamond"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                            }
                        }
                        break;
                    case "ClubsAce":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Clubs"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                            }
                        }
                        break;
                    case "SpadesAce":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Spades"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                            }
                        }
                        break;
                    case "RedPocketSuit":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Color.Equals("Red")) {
                                pocket.ModifyMult(item.ValueOfStats[0], true);
                            }
                            else
                            {
                                if (pocket.Suit.Equals("Clubs"))
                                {
                                    pocket.ModifyMult(item.ValueOfStats[1], false);
                                }
                                else if (pocket.Suit.Equals("Spades")) {
                                    pocket.ModifyMult(item.ValueOfStats[2], true);
                                }
                            }
                        }
                        break;
                    case "BlackPocketSuit":
                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Color.Equals("Black"))
                            {
                                if (pocket.Suit.Equals("Clubs"))
                                {
                                    pocket.ModifyMult(item.ValueOfStats[0], true);
                                }
                                else if (pocket.Suit.Equals("Spades"))
                                {
                                    pocket.ModifyMult(item.ValueOfStats[0], false);
                                }
                            }
                            else
                            {
                                pocket.ModifyMult(item.ValueOfStats[1], false);
                            }
                        }
                        break;
                }
            }
        }
    }

    private void SearchBouncePins()
    {
        GameObject[] bouncePins = GameObject.FindGameObjectsWithTag("BouncePin");

        foreach (GameObject bouncePin in bouncePins)
        {
            BouncePinList.Add(bouncePin);
        }
    }

    private void UpgradeBouncePins()
    {
        foreach(Item item in ActiveItems)
        {
            if (item.ItemTag.Equals("Bouncer"))
            {
                foreach(GameObject bouncePin in BouncePinList)
                {
                    bouncePin.GetComponent<BouncePinController>().HaveGoldUpgrade();
                }
            }
        }
    }

    public Item GetItem()
    {
        int randomItem = Random.Range(0, AllItems.Count);

        return AllItems[randomItem];
    }

    public List<Item> GetPinItems()
    {
        List<Item> list = new List<Item>();

        foreach (Item item in ActiveItems) {
            if (item.ItemTag.Equals("Pin"))
            {
                list.Add(item);
            }
        }

        return list;
    }
}