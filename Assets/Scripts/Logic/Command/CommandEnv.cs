using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEnv
{
    Stack<float> _numStack = new Stack<float>();
    Stack<string> _strStack = new Stack<string>();
    List<Command> _cmdList = new List<Command>(4);
    int _p = 0;

    public Actor Self { get; }
    public Actor Enemy { get; }

    public void SetCommandList(IEnumerable<Command> cmds)
    {
        Reset();
        _cmdList.AddRange(cmds);
    }

    public void AddCommand(Command cmd)
    {
        _cmdList.Add(cmd);
    }

    public void Run()
    {
        var s = RunStep();
        while (s)
        {
            s = RunStep();
        }
    }

    public bool RunStep()
    {
        if (_p >= _cmdList.Count)
            return false;

        var cmd = _cmdList[_p];
        _p++;
        CommandExec.Execute(this, cmd);
        return true;
    }

    public void PushNum(float v)
    {
        _numStack.Push(v);
    }

    public float PopNum()
    {
        return _numStack.Pop();
    }

    public void PushStr(string v)
    {
        _strStack.Push(v);
    }

    public string PopStr()
    {
        return _strStack.Pop();
    }

    public void MovePointer(int offset)
    {
        _p += offset;
    }

    public void SetPointer(int v)
    {
        _p = v;
    }

    public void Clear()
    {
        _numStack.Clear();
        _strStack.Clear();
        _cmdList.Clear();
        _p = 0;
    }

    public void Reset()
    {
        _numStack.Clear();
        _strStack.Clear();
        _p = 0;
    }
}