using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
    public ParticleEmitter particleExplosionStart;

    /// <summary>
	/// Call OnCollision if collision with player and obstacles or walls 
	/// </summary>
	void OnCollisionEnter2D(Collision2D coll)
    {
        OnCollision(coll.gameObject, coll);
    }

    /// <summary>
    /// Check who is collide with the player: if walls: emit particles, if obstacles: game over
    /// </summary>
    void OnCollision(GameObject obj, Collision2D coll)
    {
        gameObject.SetActive(false);
    }
}
