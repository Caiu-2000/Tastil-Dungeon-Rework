using System.Collections;
using TMPro;
using UnityEngine;
//Ta hecho rapido, no bien
public class forestExit : MonoBehaviour, IInteractable
{
    public string interactMessage => "Huir";

    [SerializeField]GameObject text;
    [SerializeField] GameObject player;
    [SerializeField] Transform pos;
    [SerializeField] GameObject cam;

    public void Interact(PlayerMaster _player = null)
    {
        StartCoroutine(exit());
    }

    private IEnumerator exit()
    {
        yield return FadeController.Instance.Fade();
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = pos.position;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<CharacterController>().enabled = true;
        cam.transform.rotation = Quaternion.identity;
        text.SetActive(false);
        yield return FadeController.Instance.UnFade();
    }
}
