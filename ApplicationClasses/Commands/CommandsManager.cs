using System;
using System.Collections.Generic;

namespace GraphClasses.Commands
{
    public class CommandsManager
    {
        /// <summary>
        /// Shows if there are commands to undo
        /// </summary>
        public bool CanUndo => UndoStack.Count > 0;
        /// <summary>
        /// Shows if there are commands to redo
        /// </summary>
        public bool CanRedo => RedoStack.Count > 0;

        /// <summary>
        /// Stack of commands to undo
        /// </summary>
        private Stack<ICommand> UndoStack { get; }
        /// <summary>
        /// Stack of commands to redo
        /// </summary>
        private Stack<ICommand> RedoStack { get; }

        /// <summary>
        /// Initializes a new CommandsManager instance
        /// </summary>
        public CommandsManager()
        {
            UndoStack = new Stack<ICommand>();
            RedoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Undoes last command
        /// </summary>
        public void Undo()
        {
            if (!CanUndo) return;
            var command = UndoStack.Pop();
            command.UnExecute();
            RedoStack.Push(command);
            if(!CanUndo) CanUndoChanged?.Invoke(false, null);
            if(RedoStack.Count == 1) CanRedoChanged?.Invoke(true, null);
        }

        /// <summary>
        /// Redoes command
        /// </summary>
        public void Redo()
        {
            if (!CanRedo) return;
            var command = RedoStack.Pop();
            command.Execute();
            UndoStack.Push(command);
            if (!CanRedo) CanRedoChanged?.Invoke(false, null);
            if (UndoStack.Count == 1) CanUndoChanged?.Invoke(true, null);
        }

        /// <summary>
        /// Executes command, pushes it into the undo stack
        /// and clears the redo stack 
        /// </summary>
        public void Execute(ICommand command)
        {
            command.Execute();
            UndoStack.Push(command);
            RedoStack.Clear();
            if (UndoStack.Count == 1) CanUndoChanged?.Invoke(true, null);
            CanRedoChanged?.Invoke(false, null);
        }

        /// <summary>
        /// Occurs when CanUndo value changes
        /// </summary>
        public static event EventHandler CanUndoChanged;
        /// <summary>
        /// Occurs when CanRedo value changes
        /// </summary>
        public static event EventHandler CanRedoChanged;
    }
}
