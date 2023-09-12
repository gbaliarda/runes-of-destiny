using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    #region SINGLETON
    static public EventQueueManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    public Queue<ICommand> EventQueue => _eventQueue; 
    private Queue<ICommand> _eventQueue = new Queue<ICommand>();

    public void Update()
    {
        bool processedFrostBallSound = false;
        while (_eventQueue.Count > 0)
        {
            ICommand command = _eventQueue.Dequeue();
            if (command is CmdPlaySound cmdPlaySound)
            {
                if (cmdPlaySound.Listener is FrostBallSound)
                {
                    if (processedFrostBallSound) continue;
                    processedFrostBallSound = true;
                }
            }

            command.Execute();
        }
    }

    public void AddCommand(ICommand command) => _eventQueue.Enqueue(command);


}
