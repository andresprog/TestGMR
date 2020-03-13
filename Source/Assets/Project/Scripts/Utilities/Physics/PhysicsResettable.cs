using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for things that can be Resetted to some
/// kind of saved values.
/// </summary>
public interface IResettable
{
    void Reset();
}

/// <summary>
/// This version of <see cref="IResettable"/> resets the values
/// of transform and physical properties of the game object.
/// </summary>
public abstract class PhysicsResettable : MonoBehaviour, IResettable
{
    /// <summary>
    /// The Rigidbody to reset.
    /// </summary>
    public Rigidbody Rb { get; private set; }
    /// <summary>
    /// The Collider to reset.
    /// </summary>
    public Collider Col { get; private set; }

    // Original values. Saved to be able to reset back to them.
    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private bool originalUseGravity;
    private bool originalIsKinematic;
    private bool originalIsTrigger;
    private bool originalColEnabled;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();

        WriteResetTransformValues();
        WriteResetPhysicsValues();

        ExtraAwake();
    }

    /// <summary>
    /// Classes that inherit from <see cref="PhysicsResettable"/> can do
    /// extra Awake steps by overriding this method.
    /// An example is saving additional values to reset.
    /// </summary>
    protected abstract void ExtraAwake();

    /// <summary>
    /// We call this function to set the transform values we should reset to.
    /// </summary>
    public void WriteResetTransformValues()
    {
        // Save the transform values
        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }

    /// <summary>
    /// We call this function to set the physic values we should reset to.
    /// </summary>
    public void WriteResetPhysicsValues ()
    {
        // Save the physics values
        originalUseGravity = Rb.useGravity;
        originalIsKinematic = Rb.isKinematic;
        originalIsTrigger = Col.isTrigger;
        originalColEnabled = Col.enabled;
    }

    public void Reset()
    {
        Reset(false);
    }

    /// <summary>
    /// The method to reset physics and transform values.
    /// </summary>
    /// <param name="resetPosition"> If set to <c>true</c> it also resets position. </param>
    public void Reset(bool resetPosition)
    {
        ExtraReset();
        ResetPhysics();
        ResetTransform(resetPosition);
    }

    /// <summary>
    /// Classes that inherit from <see cref="PhysicsResettable"/> can do
    /// extra reset steps by overriding this method.
    /// </summary>
    protected abstract void ExtraReset();

    /// <summary> This function is used to set the physical properties of the object to its
    /// last saved values. </summary>
    public void ResetPhysics()
    {
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;

        Rb.useGravity = originalUseGravity;
        Rb.isKinematic = originalIsKinematic;
        Col.isTrigger = originalIsTrigger;
        Col.enabled = originalColEnabled;
    }

    /// <summary> This function is used to reset the object to its last saved parent and
    /// optionally to its last saved position. </summary>
    /// <param name="resetPosition"> If set to <c>true</c> it resets position. </param>
    public void ResetTransform(bool resetPosition = false)
    {
        transform.SetParent(originalParent);
        if (resetPosition)
        {
            transform.localPosition = originalLocalPosition;
            transform.localRotation = originalLocalRotation;
        }
    }
}
