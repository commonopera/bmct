using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Chong : MonoBehaviour
{
    public TextMeshProUGUI TextBoxes;
    Coroutine ChongRoutine;

    private void Awake()
    {
        Screen.SetResolution(1800, 1200, true);
        ChongRoutine = StartCoroutine(ChongTime());
    }

    IEnumerator ChongTime()
    {
        yield return new WaitForSeconds(1.6f);
        TextBoxes.SetText("C");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("CO");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COM");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMM");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMO");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON ");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON O");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON OP");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON OPE");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON OPER");
        yield return new WaitForSeconds(.08f);
        TextBoxes.SetText("COMMON OPERA");
        yield return new WaitForSeconds(.6f);
        ChongRoutine = null;
        SceneManager.LoadScene("title");
    }
}
