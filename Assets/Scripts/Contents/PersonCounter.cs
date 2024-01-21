using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCounter : MonoBehaviour
{
    [SerializeField] string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            GameManager.Social.UpdateParticipants(this);
        }
    }

    private void Start()
    {
        Name = _name;
    }
}
