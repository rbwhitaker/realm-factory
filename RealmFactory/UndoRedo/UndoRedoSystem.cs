using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmEngine.UndoRedo
{
    /// <summary>
    /// An undo/redo system for the program.  This manages
    /// the list of program states that existed in the operation
    /// of the program, and keeps track of where in the undo/redo
    /// history the program currently is.
    /// </summary>
    public class UndoRedoSystem
    {
        /// <summary>
        /// Stores the list of program states that have been
        /// in use.
        /// </summary>
        private List<ProgramState> states;

        // Stores the current index into the list of program states.
        private int currentIndex;

        /// <summary>
        /// Creates a new undo/redo system with no program states
        /// in it.
        /// </summary>
        public UndoRedoSystem()
        {
            Clear();
        }

        /// <summary>
        /// Adds a new state to the undo/redo system.  If the undo/redo
        /// system is in the middle of the list of states (that is,
        /// someone has undone an action (or many) then any undone
        /// actions are wiped out.  You're going down a different path
        /// at this point, an the original path you backed out of becomes
        /// unusable.  That's pretty typical operation for undo/redo.
        /// </summary>
        /// <param name="programState"></param>
        public void PushState(ProgramState programState)
        {
            if (states.Count == 0)
            {
                states.Add(programState);
                currentIndex = 0;
            }
            else
            {
                while (CanRedo())
                {
                    states.RemoveAt(states.Count - 1);
                }

                states.Add(programState);
                currentIndex++;
            }

            OnSystemChanged();
        }

        /// <summary>
        /// Indicates whether the system can currently undo a state.
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return currentIndex > 0;
        }

        /// <summary>
        /// Indicates whether the system can currently redo a state.
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return currentIndex < states.Count - 1;
        }

        /// <summary>
        /// Undoes the current state, back to where it was before.  Or 
        /// rather, returns the previous state--the program must make
        /// the needed changes to ensure that the original state is
        /// recovered.  If Undo is called when CanUndo is false, null
        /// will be returned.
        /// </summary>
        /// <returns></returns>
        public ProgramState Undo()
        {
            if(!CanUndo()) { return null; }
            
            currentIndex--;

            OnSystemChanged();
            
            return states[currentIndex];
        }

        /// <summary>
        /// Redoes the current state, back to where it was before
        /// the last undo.  Or rather, returns the next state--the
        /// program must make the needed changes to ensure that the original
        /// state is recovered.  If Redo is called when CanRedo is false, null
        /// will be returned.
        /// </summary>
        /// <returns></returns>
        public ProgramState Redo()
        {
            if (!CanRedo()) { return null; }

            currentIndex++;

            OnSystemChanged();
            
            return states[currentIndex];
        }

        /// <summary>
        /// An event that is raises whenever the undo/redo system is modified
        /// (undo or redo is called, a new state is added, or the system is 
        /// cleared out).
        /// </summary>
        public event EventHandler SystemChanged;

        /// <summary>
        /// Raises the SystemChanged event.
        /// </summary>
        public void OnSystemChanged()
        {
            if (SystemChanged != null)
            {
                SystemChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Clears out the entire undo/redo system to scratch.
        /// </summary>
        public void Clear()
        {
            states = new List<ProgramState>();
            currentIndex = -1;
            OnSystemChanged();
        }
    }
}
