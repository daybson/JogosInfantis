using UnityEngine;
using System.Collections;

/*
 * Classe genérica que implementa o design pattern Singleton:
 *   Garante que haja apenas uma instância da classe, que pode ser acessada
 *   via propriedade 'Instance'.
 * 
 * T pode ser qualquer tipo que herde de MonoBehaviour.
 * */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            /**
             * Se a instância for nula, encontra todas as instâncias ativas na cena.
             * Define a instância de uso como a primeira encontrada.
             * 
             * Se houver mais de uma, destrói as demais.
             */
            if (instance == null)
            {
                var objs = FindObjectsOfType<T>();

                instance = objs[0];

                if (objs.Length > 1)
                {
                    for (var i = objs.Length; i > 1; i--)
                        Destroy(objs[i].gameObject);
                }

                if (instance == null)
                    Debug.LogError("There's no singleton instantiated!");
            }

            return instance;
        }
    }
}