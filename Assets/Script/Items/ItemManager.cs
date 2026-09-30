using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public List<Item> AllItems = new List<Item>();
    public List<Item> PurchasedItems = new List<Item>();

    public static ItemManager Instance { get; private set; }

    private List<Pocket> PocketList = new List<Pocket>();
    private List<GameObject> BouncePinList = new List<GameObject>();

    [SerializeField] private SuitsMults SuitsMults;

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
        foreach (Item item in PurchasedItems)
        {
            if (item.ItemTag.Equals("PocketPasive"))
            {
                float mult = 0f;
                //Sorry por el switch, se tendria que modificar o hacer otro csv para los pocket items para evitarlo (y no nos vamos engañar, es trabajo extra y me da flojera y la jam es este miercoles, no da tiempo)
                switch (item.ItemName)
                {
                    case "HeartAce":
                        mult = 0f;

                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Heart"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                                mult = pocket.GetPocketValue();
                            }
                        }

                        SuitsMults.ModifyText("Heart", mult);
                        break;
                    case "DiamondAce":
                        mult = 0f;

                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Diamond"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                                mult = pocket.GetPocketValue();
                            }
                        }

                        SuitsMults.ModifyText("Diamond", mult);
                        break;
                    case "ClubsAce":
                        mult = 0f;

                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Clubs"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                                mult = pocket.GetPocketValue();
                            }
                        }

                        SuitsMults.ModifyText("Clubs", mult);
                        break;
                    case "SpadesAce":
                        mult = 0f;

                        foreach (Pocket pocket in PocketList)
                        {
                            if (pocket.Suit.Equals("Spades"))
                            {
                                pocket.ModifyMult(item.ValueOfStats[0]);
                                mult = pocket.GetPocketValue();
                            }
                        }

                        SuitsMults.ModifyText("Spades", mult);
                        break;
                    case "RedPocketSuit":
                        foreach (Pocket pocket in PocketList)
                        {
                            mult = 0f;
                            if (pocket.Color.Equals("Red")) {
                                pocket.ModifyMult(item.ValueOfStats[0], true);
                                mult = pocket.GetPocketValue();
                                SuitsMults.ModifyText("Heart", mult);
                                SuitsMults.ModifyText("Diamond", mult);
                            }
                            else
                            {
                                if (pocket.Suit.Equals("Clubs"))
                                {
                                    pocket.ModifyMult(item.ValueOfStats[1], false);
                                    mult = pocket.GetPocketValue();
                                    SuitsMults.ModifyText("Clubs", mult);
                                }
                                else if (pocket.Suit.Equals("Spades")) {
                                    pocket.ModifyMult(item.ValueOfStats[2], true);
                                    mult = pocket.GetPocketValue();
                                    SuitsMults.ModifyText("Spades", mult);
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
                                    mult = pocket.GetPocketValue();
                                    SuitsMults.ModifyText("Clubs", mult);
                                }
                                else if (pocket.Suit.Equals("Spades"))
                                {
                                    pocket.ModifyMult(item.ValueOfStats[0], false);
                                    mult = pocket.GetPocketValue();
                                    SuitsMults.ModifyText("Spades", mult);
                                }
                            }
                            else
                            {
                                pocket.ModifyMult(item.ValueOfStats[1], false);
                                mult = pocket.GetPocketValue();
                                SuitsMults.ModifyText("Heart", mult);
                                SuitsMults.ModifyText("Diamond", mult);
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
        foreach(Item item in PurchasedItems)
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
        return PurchasedItems.Where(item => item.ItemTag == "Pin").ToList();
    }

    public void ConsumePinItem(string pinName)
    {
        Item itemToRemove = PurchasedItems.FirstOrDefault(item => item.ItemName == pinName && item.ItemTag == "Pin");

        if (itemToRemove != null) {
            PurchasedItems.Remove(itemToRemove);
        }   
    }
}