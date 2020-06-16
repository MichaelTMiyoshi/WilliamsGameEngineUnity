/*
 * GameManager.cs
 * Michael T. Miyoshi
 * (school project)
 * 06/15/2020 - 06/16/2020
 * 
 * Singleton for all global variables and controls
 * 
 * Empty game object (called GameManagerObject) in scene.
 * 
 * for future reference:
 * scrolling background (there are many ways to do this, but here is one)
 *  https://www.youtube.com/watch?v=IgZQjGyB9zg
 * 
 * Game Engine Tutorial ported over to Unity.
 * 
 * I took the Game Engine Tutorial written by Eric Williams and made the 
 * project in Unity.  Not completely.  Just through the collisions.
 * 
 * The ship is controlled by the arrow keys for movement.
 * The ship shoots lasers with the space bar.
 * The ship should be able to be controlled by a joystick.
 * Firing the lasers can also be done with the left mouse button.
 * Lasers destroy meteors when they collide.  (Actually, the meteors destroy
 *      the lasers and themselves when they collide.)
 * Meteors destroy each other when they collide.
 * Meteors take life from the ship when they collide.
 * Lasers and meteors are destroyed when they reach the end of the screen.
 * 
 * The GameManager holds the screen size and has all the globals.  It also
 * spawns the meteors.
 * 
 * I took the Boundaries.cs file from a different project.  You need to add
 * it as a component to the ship to make it limit the ship or make the ship 
 * wrap around.  I chose to have the ship wrap as the default.  The boundary
 * as a limit is a bit buggy.  The ship can get stuck on a side.  I did not
 * change this, but leave it as an exercise for anybody who would like to 
 * use this as a starting point in learning Unity for developing 2D games.
 * 
 *******************************************
 * Still to do.
 *******************************************
 * 
 * Explosions.  The animation and sound for explosions need to be added.
 * UI.  There needs to be a User Interface to display health.
 * Game Over.  There needs to be a scene for game over and restarting.
 * Scrolling Background.  It would be nice to have a scrolling background to
 *      suggest movement.
 * Mods.  There are many mods that could be made.  Including, but not limited
 *      to changing the sprites, changing the motion of meteors, changing 
 *      the controls, changing the rotation of the ship, the sky (rather, the
 *      universe) is the limit.
 *  
 *  Cleaned up the code to make things a bit more clear.
 *      Many public variables were made into [SerializeField] private
 *      fields.  These are fields that need to have objects dragged and 
 *      dropped into them in the Unity interface.  Using this notation makes
 *      it easier to see which fields need to have the dragging and dropping
 *      done and which are really public fields that can be accessed by other
 *      objects.  Will continue to use this notation in the future.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject background;
    public int maxHealth;
    public bool wrap;
    public float boundaryFudgeFactor;
    public Vector2 screenBounds;
    public float timeBetweenMeteorSpawnMin;
    public float timeBetweenMeteorSpanwMax;
    float timeBetweenMeteorSpawn;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }

        maxHealth = 5;
        wrap = true;
        boundaryFudgeFactor = 0.5f;
        timeBetweenMeteorSpawnMin = 0.5f;
        timeBetweenMeteorSpanwMax = 1.0f;
        timeBetweenMeteorSpawn = Random.Range(timeBetweenMeteorSpawnMin, timeBetweenMeteorSpanwMax);
    }
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        timeBetweenMeteorSpawn -= Time.deltaTime;
        if (timeBetweenMeteorSpawn <= 0)
        {
            float force = Random.Range(75.0f, 125.0f);
            timeBetweenMeteorSpawn = Random.Range(timeBetweenMeteorSpawnMin, timeBetweenMeteorSpanwMax);
            Vector2 position = new Vector2(screenBounds.x, Random.Range(-1 * screenBounds.y, screenBounds.y));
            GameObject meteorObject = Instantiate(meteorPrefab, position, Quaternion.identity);
            MeteorController meteor = meteorObject.GetComponent<MeteorController>();
            meteor.Spawn(Vector2.left, force);
        }
    }
}
