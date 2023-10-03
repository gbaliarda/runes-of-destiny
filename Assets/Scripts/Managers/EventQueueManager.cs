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
    private Queue<ICommand> _eventUndoQueue = new Queue<ICommand>();

    public void Update()
    {
        bool processedSpellSound = false;
        while (_eventQueue.Count > 0)
        {
            ICommand command = _eventQueue.Dequeue();
            if (command is CmdPlaySound cmdPlaySound)
            {
                if (cmdPlaySound.Listener is SpellSound)
                {
                    Debug.Log("Sound is SpellSound");
                    if (processedSpellSound) continue;
                    Debug.Log("Continuing");
                    processedSpellSound = true;
                }
            }

            command.Execute();
        }

        while (_eventUndoQueue.Count > 0)
        {
            ICommand command = _eventUndoQueue.Dequeue();
            command.Undo();
        }
    }

    public void AddCommand(ICommand command) => _eventQueue.Enqueue(command);

    public void AddUndoCommand(ICommand command) => _eventUndoQueue.Enqueue(command);


}
