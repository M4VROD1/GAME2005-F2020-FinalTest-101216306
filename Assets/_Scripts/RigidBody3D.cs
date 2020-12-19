using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType {
    STATIC,
    DYNAMIC
}


[System.Serializable]
public class RigidBody3D : MonoBehaviour {
    [Header("Gravity Simulation")] public float gravityScale;
    public float mass;
    public BodyType bodyType;
    public float timer;
    public bool isFalling;

    [Header("Attributes")] public Vector3 velocity;
    public Vector3 acceleration;
    private float gravity;
    public float friction = 0.75f;

    public bool justStartedFalling = false;

    // Start is called before the first frame update
    void Start() {
        timer = 0.0f;
        gravity = -0.001f;
        velocity = Vector3.zero;
        acceleration = new Vector3(0.0f, gravity * gravityScale, 0.0f);
        if (bodyType == BodyType.DYNAMIC) {
            isFalling = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (bodyType == BodyType.DYNAMIC) {
            timer += Time.deltaTime;

            if (isFalling) {
                if (!justStartedFalling) {
                    justStartedFalling = true;
                    velocity = Vector3.zero;
                }
                
                acceleration = new Vector3(0.0f, gravity * gravityScale, 0.0f);

                if (gravityScale < 0) {
                    gravityScale = 0;
                }

                if (gravityScale > 0) {
                    velocity += acceleration * 0.5f * timer * timer;
                }
            } else {
                timer = 0;
                velocity *= friction;
                if (justStartedFalling) {
                    justStartedFalling = false;
                }
            }

            transform.position += velocity;
        }
    }

    public void Stop() {
        timer = 0;
        isFalling = false;
        velocity = Vector3.zero;
    }
}
