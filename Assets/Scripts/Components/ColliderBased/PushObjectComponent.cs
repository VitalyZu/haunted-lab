using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectComponent : MonoBehaviour
{
    [SerializeField] float _force;
    private static Dictionary<int, Creature> _creatues = new Dictionary<int, Creature>();
    public void PushTarget(GameObject target)
    {
        Debug.Log("Push Target");
        var id = target.GetInstanceID();
        var direction = GetDirection(target);
        //var force = new Vector2(_force * direction, 1f);

        Creature creature;
        if (_creatues.TryGetValue(id, out creature))
        {
            creature.PushSelf(direction);
        }
        else
        {
            creature = target.GetComponent<Creature>();
            if (creature != null)
            {
                creature.PushSelf(direction);
                _creatues.Add(id, creature);
            }
        }
    }

    private int GetDirection(GameObject target)
    {
        Debug.Log(gameObject.transform.position.x);
        Debug.Log(target.transform.position.x);
        return gameObject.transform.position.x > target.transform.position.x ? -1 : 1;
    }
}
