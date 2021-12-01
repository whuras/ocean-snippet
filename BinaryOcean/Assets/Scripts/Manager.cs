using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float neighbourRadius = 1;
    public LayerMask numberMask;

    public int size = 10;
    public Vector3 offset = new Vector3(0, 0, 0);

    public GameObject prefab0;
    public GameObject prefab1;
    private List<Number> numbers = new List<Number>();

    private float timer;
    private float maxTimer = 10f;

    private float uiTimer = 0f;
    private bool startFade = false;
    public Canvas canvas;
    private Image background;
    private Text[] text;

    private bool startTransition = false;
    
    // Start is called before the first frame update
    void Start()
    {
        background = canvas.GetComponentInChildren<Image>();
        text = canvas.GetComponentsInChildren<Text>();

        timer = Random.Range(0, 10);
        PopulateNumbers();
        foreach(Number num in numbers)
        {
            num.UpdateNeighbours(neighbourRadius, numberMask);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            startTransition = true;

        if (startTransition)
        {
            if (!startFade)
            {
                uiTimer += Time.deltaTime;
            }

            if (uiTimer >= 3f && !startFade)
            {
                startFade = true;
                background.CrossFadeAlpha(0f, 2.0f, false);
                text[0].CrossFadeAlpha(0, 3.0f, false);
                text[1].CrossFadeAlpha(0, 3.0f, false);
            }

            timer += Time.deltaTime;
            if (timer >= maxTimer)
            {
                timer = Random.Range(0, 10);

                float duration = 0.5f;
                float heighMultiplier = 0.4f;
                int counter = 5;
                numbers[Random.Range(0, numbers.Count)].StartBob(duration, heighMultiplier, counter, true); ;
            }
        }

    }

    private void PopulateNumbers()
    {
        int[] digits = { 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1 };
        int k = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos = Vector3.zero;
                if (j % 2 == 0)
                    pos = new Vector3(i * offset.x + offset.x / 2, offset.y, j * offset.z);
                else
                    pos = new Vector3(i * offset.x, offset.y, j * offset.z);


                GameObject obj = Instantiate(digits[k] == 0 ? prefab0 : prefab1, pos, prefab1.transform.rotation, transform);
                k += 1;

                if (k >= digits.Length)
                    k = 0;

                numbers.Add(obj.GetComponent<Number>());
            }
        }
    }
}
