using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoState
{
    List<ParamsMemento> _rememberPosition; //Lista que simula ser un stack, donde voy a agregar estados opacos
                                           //y tomarlos (del ultimo al primero)

    public MementoState()
    {
        _rememberPosition = new List<ParamsMemento>();
    }

    /// <summary>
    /// Devuelvo la cantidad de estados (recuerdos) que tiene mi lista
    /// </summary>
    /// <returns></returns>
    public int MemoriesQuantity()
    {
        return _rememberPosition.Count;
    }

    /// <summary>
    /// Tomo un recuerdo de la lista (el ultimo en este caso), lo borro y lo devuelvo
    /// </summary>
    /// <returns></returns>
    public ParamsMemento Remember()
    {
        int index = _rememberPosition.Count - 1;

        var currentPos = _rememberPosition[index];

        _rememberPosition.RemoveAt(index);

        return currentPos;
    }

    /// <summary>
    /// Guardo un recuerdo en mi lista usando ParametersMemento como intermediario para que
    /// me adapte el params object a object
    /// </summary>
    /// <param name="parameterWrapper"></param>
    public void Rec(params object[] parameterWrapper)
    {
        _rememberPosition.Add(new ParamsMemento(parameterWrapper));
    }

}
