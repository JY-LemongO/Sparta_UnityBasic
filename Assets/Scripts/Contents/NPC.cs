using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject         _npcInteractPanel;
    [SerializeField] NPCTalk            _npcTalk;
    [SerializeField] Sprite             _sprite;
    [SerializeField] TextMeshProUGUI    _scriptTMP;
    [SerializeField] string[]           _npcRandomScripts;
    
    public Sprite GetNpcSprite() => _sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Player.SetCurrentNpc(this);
        _npcTalk.AppearTalk(true);

        RandomScript();
        StartCoroutine(PanelRoutine(0.2f));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        _npcTalk.AppearTalk(false);

        GameManager.Player.SetCurrentNpc(null);
        StartCoroutine(PanelRoutine(0));        
    }    

    private void RandomScript()
    {        
        int random = Random.Range(0, _npcRandomScripts.Length);
        _scriptTMP.text = _npcRandomScripts[random];
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
