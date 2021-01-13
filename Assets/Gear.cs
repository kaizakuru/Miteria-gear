using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour
{
    public DataBase data;

    public List<ItemGear> items = new List<ItemGear>();
    
    public GameObject gameObjectShow;


    public GameObject GearMainObject;
    public GameObject GearDownObject;

    public int maxCount;
    public int maxCountDown;

    public Camera cam;
    public EventSystem es; 

    public int currentID;
    public ItemGear currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject backGround;

    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }


        for (int i = 0; i < maxCount; i++)//Тест для заполнения 
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 99));
        }
        UpdateGear();

        //Тест для заполнения нижнего
        for (int i = 0; i < maxCountDown; i++)//Тест для заполнения 
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 99));
        }
        UpdateGear();
        //___________________________

    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.I))//Открывает и закрывает по кнопке I
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateGear();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < 100)
                {
                    items[i].count += count;

                    if (items[i].count > 100)
                    {
                        count = items[i].count - 100;
                        items[i].count = 50;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }







        //Тест для нижнего
        for (int i = 0; i < maxCountDown; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < 100)
                {
                    items[i].count += count;

                    if (items[i].count > 100)
                    {
                        count = items[i].count - 100;
                        items[i].count = 50;
                    }
                    else
                    {
                        count = 0;
                        i = maxCountDown;
                    }
                }
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < maxCountDown; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCountDown;
                }
            }
        }
        //_______________






    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }


    public void AddGearItem(int id, ItemGear gerItem)
    {
        items[id].id = gerItem.id;
        items[id].count = gerItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[gerItem.id].img;

        if (gerItem.count > 1 && gerItem.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = gerItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }


    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, GearMainObject.transform) as GameObject;

            newItem.name = i.ToString();//Превращает содержимое в текстовую часть 

            ItemGear ig = new ItemGear();
            ig.itemGameObject = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);//При взамодествие с предметом что бы масштаб был такой же


            Button tempButton = newItem.GetComponent<Button>();//Каждый пункт -- кнопка

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ig);
        }









        //______________________
        for (int i = 0; i < maxCountDown; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, GearDownObject.transform) as GameObject;

            newItem.name = i.ToString();//Превращает содержимое в текстовую часть 

            ItemGear ig = new ItemGear();
            ig.itemGameObject = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);//При взамодествие с предметом что бы масштаб был такой же


            Button tempButton = newItem.GetComponent<Button>();//Каждый пункт -- кнопка

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ig);
        }
        //______________________








    }

    public void UpdateGear()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }








        //__________________
        for (int i = 0; i < maxCountDown; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
        //__________________







    }

    public void SelectObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyGearItem(items[currentID]);
            movingObject.gameObject.SetActive(true);//Что то перемешаем
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            ItemGear ig = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != ig.id)
            {
                AddGearItem(currentID, ig);

                AddGearItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (ig.count + currentItem.count <= 100)
                {
                    ig.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[ig.id], ig.count + currentItem.count - 100);

                    ig.count = 100;
                }

                ig.itemGameObject.GetComponentInChildren<Text>().text = ig.count.ToString();

            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);

        }
    }

    public void MoveObject()//Работа с камерой
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = GearMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);

        Vector3 posd = Input.mousePosition + offset;
        posd.z = GearDownObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(posd);
    }

   

    public ItemGear CopyGearItem(ItemGear old)
    {
        ItemGear New = new ItemGear();
        
        New.id = old.id;
        New.itemGameObject = old.itemGameObject;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]

public class ItemGear
{
    public int id;
    public GameObject itemGameObject;

    public int count;//показывает сколько в инвенторе элементов
}