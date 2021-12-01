using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class flowerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flowerCanvas;
    private int minX = 20;
    private int maxX = 480;
    private float minY = 0;
    private float maxY = 300;
    private int maxWidth = 70;
    private int minWidth = 35;
    private int maxHeight = 100;
    private int minHeight = 50;
    public Sprite[] flowerImages;
    public List<GameObject> flowers;

    public static flowerScript instance;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void newFlower()
    {
        int posX = UnityEngine.Random.Range(minX, maxX);
        float posY = UnityEngine.Random.Range(minY, maxY);
        float scale = 1.4f*(1.4f * maxY - posY) / maxY;
        int flowerIndex = UnityEngine.Random.Range(0, 4);
        GameObject newFlower = new GameObject();

        RectTransform trans = newFlower.AddComponent<RectTransform>();
        trans.transform.SetParent(flowerCanvas.transform);
        trans.anchoredPosition = new Vector2(posX-250, posY-140);
        trans.sizeDelta = new Vector2(1,1);

        Image image = newFlower.AddComponent<Image>();
        image.rectTransform.localScale = new Vector2(minWidth * scale, minHeight * scale);
        image.sprite = flowerImages[flowerIndex];
        newFlower.transform.SetParent(flowerCanvas.transform);
        flowers.Add(newFlower);
    }

    public void addFlowers(int n)
    {
        int leftovers = n;
        int available = GameManager.instance.getMaxFlowers() - GameManager.instance.getPlayerFlowers();
        if (n > available)
            leftovers = available;
        for (int i = 0; i < leftovers; i++)
        {
            newFlower();
        }
        GameManager.instance.incFlowers(n);
    }
    public void removeFlowers(int n)
    {
        if (flowers.Count > 0)
        {
            if (flowers.Count < n)
            {
                n = flowers.Count;

            }
            for (int i = 0; i < n; i++)
            {
                GameObject flower = flowers.Last();
                flowers.Remove(flower);
                Destroy(flower);
            }
            GameManager.instance.decFlowers(n);
        }
    }
    
}
