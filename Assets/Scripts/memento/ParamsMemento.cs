using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamsMemento
{
    public object[] parameters;

    public ParamsMemento(params object[] parameterWrapper) // Tomo un params object que se crea al pasar por parametro un new object[]
    {
        parameters = new object[parameterWrapper.Length];

        for (int i = 0; i < parameterWrapper.Length; i++)
        {
            //Lo guardo en mi array de objecxt para que me quede en cada indice el estado opaco guardado
            parameters[i] = parameterWrapper[i];
        }
    }
}
