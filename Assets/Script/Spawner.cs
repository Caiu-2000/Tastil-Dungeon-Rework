using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject[] enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.numpad1Key.wasPressedThisFrame)
            StartCoroutine(Spawn());
        if(Keyboard.current.numpad4Key.wasPressedThisFrame)
            StopAllCoroutines();
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
    }
}
