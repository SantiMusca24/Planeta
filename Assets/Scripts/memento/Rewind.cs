using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    protected MementoState memento;

    /// <summary>
    /// Donde voy a tomar el recuerdo y sacar de este cada variable que necesite
    /// </summary>
    /// <param name="wrappers"></param>
    protected abstract void BeRewind(ParamsMemento wrappers);

    /// <summary>
    /// Corrutina donde voy a guardar los estados
    /// </summary>
    /// <returns></returns>
    public abstract void StartToRec();

    //El Awake base que van a ejecutar los hijos
    protected virtual void Awake()
    {
        //Creo un nuevo MementoState donde voy a guardar mis recuerdos
        memento = new MementoState();

        //Empiezo la corrutina que va a estar seteada en los hijos
        //StartCoroutine(StartToRec());
    }
    
    /// <summary>
    /// Pregunto si tengo recuerdos y en caso de tener tomo el ultimo llamando a la funcion que seteo el hijo
    /// Y pasando por parametro el ultimo estado a recordar que trae mi MementoState
    /// </summary>
    public void Action()
    {
        if (memento.MemoriesQuantity() <= 0)
            return;

        BeRewind(memento.Remember());
    }
}
