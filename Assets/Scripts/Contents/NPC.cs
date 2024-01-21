using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject         _npcInteractPanel;
    [SerializeField] TextMeshProUGUI    _scriptTMP;
    [SerializeField] string[]           _npcScripts;
    
    private int _scriptIndex = 0;

    public void NextScript()
    {
        if (_scriptIndex == _npcScripts.Length - 1)
        {
            StartCoroutine(PanelRoutine(0));
            ResetScript();
            return;
        }            

        _scriptIndex++;
        _scriptTMP.text = _npcScripts[_scriptIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        StartCoroutine(PanelRoutine(0.2f));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        StartCoroutine(PanelRoutine(0));
        ResetScript();
    }

    private void ResetScript()
    {
        _scriptIndex = 0;
        _scriptTMP.text = _npcScripts[_scriptIndex];
    }

    private IEnumerator PanelRoutine(float end)
    {
        float current = 0;
        float percent = 0;
        float time = 0.2f;

        Vector3 start = _npcInteractPanel.transform.localScale;

        while (percent<1)
        {
            current += Time.deltaTime;
            percent = current / time;

            _npcInteractPanel.transform.localScale = Vector3.Lerp(start, Vector3.one * end, percent);
            yield return null;
        }        
    }
}
