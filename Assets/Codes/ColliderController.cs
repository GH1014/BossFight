using System.Collections;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public ChildScript1 childScript1;
    public ChildScript2 childScript2;
    public ChildScript3 childScript3;
    public ChildScript4 childScript4;


    public void ColliderOff1()
    {
        childScript1.ToggleCollider();
    }

    public void ColliderOn1()
    {
        childScript1.ToggleCollider();
    }



    public void ColliderOff2()
    {
        childScript2.ToggleCollider();
    }

    public void ColliderOn2()
    {
        childScript2.ToggleCollider();
    }



    public void ColliderOff3()
    {
        childScript3.ToggleCollider();
    }

    public void ColliderOn3()
    {
        childScript3.ToggleCollider();
    }



    public void ColliderOff4()
    {
        childScript4.ToggleCollider();
    }

    public void ColliderOn4()
    {
        childScript4.ToggleCollider();
    }
}