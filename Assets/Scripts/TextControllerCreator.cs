using UnityEngine;

public class TextControllerCreator : MonoBehaviour
      if (GameObject.Find("TextController(Clone)") == null)
          Instantiate(textControllerPref, transform.position, Quaternion.identity);