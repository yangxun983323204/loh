using System;
using System.Collections;
using System.Collections.Generic;
using YX;
using UnityEngine;

public interface ICommandExec
{
    bool Execute(CommandEnv env, Command cmd);
}

public static class CommandExec
{
    static List<ICommandExec> _cmdHandlers = new List<ICommandExec>(3);

    static CommandExec()
    {
        _cmdHandlers.Add(new MetaCmdImpl());
        _cmdHandlers.Add(new MetaExCmdImpl());
        _cmdHandlers.Add(new BattleCmdImpl());
    }

    public static void Execute(CommandEnv env, Command cmd)
    {
        foreach (var handler in _cmdHandlers)
        {
            if (handler.Execute(env, cmd))
                return;
        }
    }
}
