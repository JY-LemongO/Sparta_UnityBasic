using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Social
{
    private List<PersonCounter> _peopleList = new List<PersonCounter>();

    private int participantsCount;
    public int ParticipantsCount
    {
        get => participantsCount;
        private set
        {
            participantsCount = value;
            OnCountPeople?.Invoke(participantsCount, _peopleList);
        }
    }

    public void UpdateParticipants(PersonCounter pc)
    {
        if (_peopleList.Any(x => x == pc))
        {
            if(pc.Name == "")
            {
                _peopleList.Remove(pc);
                ParticipantsCount--;
                return;
            }
            else
            {
                OnCountPeople?.Invoke(ParticipantsCount, _peopleList);
                return;
            }            
        }
        _peopleList.Add(pc);
        ParticipantsCount++;
    }

    public event Action<int, List<PersonCounter>> OnCountPeople;
}
