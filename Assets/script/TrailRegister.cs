using UnityEngine;
using System.Collections.Generic;


public class TrailRegister : MonoBehaviour
{
    private readonly List<PencilTrail> trails = new();
    public static TrailRegister TrailRegisterInstance { get; private set; }
    void Awake()
    {
        if (TrailRegisterInstance == null)
        {
            TrailRegisterInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Register(PencilTrail trail)
    {
        if (!trails.Contains(trail))
        {
            trails.Add(trail);
        }
    }
    public void Unregister(PencilTrail trail)
    {

        trails.Remove(trail);

    }
    public IReadOnlyList<PencilTrail> Trails => trails;
}
