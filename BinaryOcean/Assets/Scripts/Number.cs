using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public float frequency = 200;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    private AudioSource[] audioSources;
    private AudioSource audioSource;

    public List<Number> neighbours = new List<Number>();
    public bool isBobbing = false;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[Random.Range(0, audioSources.Length - 1)];
    }


    public void UpdateNeighbours(float radius, LayerMask mask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
        foreach(var hitCollider in hitColliders)
        {
            Number num = hitCollider.GetComponent<Number>();
            if (num != null && num != this)
                neighbours.Add(num);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isBobbing)
                audioSource.Play();

            float duration = 0.5f;
            float heighMultiplier = 0.4f;
            int counter = 5;

            StartBob(duration, heighMultiplier, counter);
        }
    }

    public void StartBob(float duration, float heightMultiplier, int counter, bool playSound = false)
    {
        if(!isBobbing && counter > 0)
        {
            if(playSound)
                audioSource.Play();

            StartCoroutine(Bob(duration, heightMultiplier, counter));
        }
            
    }

    IEnumerator Bob(float duration, float heightMultiplier, int counter)
    {
        isBobbing = true;

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = transform.position + Vector3.up * heightMultiplier;

        float elapsedTime = 0f;

        yield return new WaitForSeconds(Random.Range(0, 3) * 0.1f);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        counter -= 1;
        heightMultiplier *= 0.5f;

        foreach (Number number in neighbours)
            number.StartBob(duration, heightMultiplier, counter);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(toPosition, fromPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isBobbing = false;
    }
}
