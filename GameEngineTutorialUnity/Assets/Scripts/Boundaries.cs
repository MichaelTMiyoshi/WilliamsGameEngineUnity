using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      from: https://www.youtube.com/watch?v=ailbszpt_AI
 *      
 *      06/03/2020
 *          Had a problem inmplementing the boundary yesterday.  The problem
 *          was with the Clamp method.  It works like it says, but the tutorial
 *          either has a probem or something changed in Unity or I messed up.
 *          I had the min and max swapped.  I did hear in the video that the 
 *          screenBounds are opposite of something so I did not see anything
 *          wrong with the -1 multiplier when it was on the max.  But the 
 *          Boid kept disappearing right at the start.  So I commented out 
 *          everything and added stuff back in.  I output to the console and
 *          did all the stuff that I normally do.  Turned out to be Clamp.  The
 *          min and max values were in opposite places.  So I swapped them and
 *          things are hunky dory.
 *          
 *      06/04/2020
 *          On to screen wrapping.
 *          Plan:  
 *              public bool variable that says whether wrapping is on or not.
 *              When the bool is true, the boid will go to the other side of
 *              the screen.  When the bool is false, the boid will just sit
 *              on the edge.  Or it will bounce.  Leaning toward bouncing.
 *                 
 *          Results:
 *              Got both wrap and !wrap (bounce) working.  Mostly.
 *              The biggest headaches came with clamping.  I think that the 
 *              bouncing and wrapping were both working, but that when doing
 *              the comparisons to the edge, moving to the other edge or
 *              changing the velocity did not quite do the trick.  In wrapping,
 *              I needed to change the equality comparison so that the boid
 *              did not just wrap from one side to the other right away.  I 
 *              could not see this happening, but I am pretty sure that was it.
 *              In bouncing, I needed to access the velocity so I made a setter
 *              and getter in the boid code.  This almost worked.  I also 
 *              needed to make the boid move with the new velocity.  So I added
 *              that into the boundary code as well.
 *              
 *          Still to do:
 *              I still need to put in some fudge factor because there are a
 *              couple cases where the boid gets stuck on the edge in wrapping.
 *              Maybe even just a plus one somewhere.
 *          
 *          Done:
 *              Added fudgeFactor.  Used it to multiply the speed when updating
 *              the position.  Warp speed at the boundary.  Looks fine for 
 *              wrapping.  Made it public so I can test it.  Not noticable for
 *              bouncing or wrapping.  Still might need to up it for wrap.
 *              Gets caught on the edge every once in a while.  (Top is most
 *              noticable.)
 *              
 *      06/04/2020
 *          To do:
 *              There is an interesting boundary condition.  The boids go from
 *              side to side or top to bottom, oscillating between the two.
 *              Figured out it is a boundary condition.  Thought I had the
 *              answer, but not quite.  Will set up individual conditions for 
 *              each of the four boundaries.
 *              
 *      06/05/2020
 *          Fixed the boundary scenario.  Went back to old school.  Actually,
 *          took the bounce out of the equation since it was working.  Instead
 *          of looking at the boundary condition and then seeing if it was
 *          wrap or bounce, tested to see if it was wrap or bounce first, then
 *          tested the boundary condition.  Bounce still worked.  Then, went
 *          old school on wrap.  Tested each case individually.  Almost.
 *          Tested the off in x-direction too high else too low and the off
 *          in the y-direction too high else too low.  That way, it would not
 *          switch the boid right back to where it was.  Those conditions
 *          need to be mutually exclusive in the same iteration (frame).
 *          
 *          So I think it is fixed.  Both bounce and wrap seem to work.
 *          By the way.  I reused the fudge factor.  Instead of having it 
 *          do a quick something at the end of the frame, I decided that I 
 *          would use it as how many widths/heights of the object to use.
 *          Turns out that 0.5 seems to do the trick.  It is actually rather 
 *          funny when a whole or more of the object is used.  You cannot 
 *          notice at 0.5 though.  Boid does not just pop back onto the screen 
 *          seemingly in the middle of nowhere.
 *          
 *          Will clean up the comments later.  I would like to have the unused
 *          code in the commits.  They are instructive to me.  What things I 
 *          tried.  What things failed and what things worked.
 *          
 *          To do:
 *              It is on to collision avoidance.  I am not exactly sure what
 *              to do here.  It is almost the opposite of cohesion, but not
 *              quite.  If it were, why would you need either?
 */
public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    bool wrap;
    float fudgeFactor;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        wrap = GameManager.instance.wrap;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        wrap = GameManager.instance.wrap;
        fudgeFactor = GameManager.instance.boundaryFudgeFactor;
        Vector3 viewPos = transform.position;
        float xMax = screenBounds.x;
        float yMax = screenBounds.y;
        ShipController ship = GetComponent<ShipController>();
        Vector2 vel = ship.moveVelocity;
        if (wrap)
        {
            if (viewPos.x <= -xMax)
            {
                viewPos.x = xMax - fudgeFactor * objectWidth;
            }
            else if (xMax <= viewPos.x)
            {
                viewPos.x = -xMax + fudgeFactor * objectWidth;
            }
            if (viewPos.y <= -yMax)
            {
                viewPos.y = yMax - fudgeFactor * objectHeight;
            }
            else if (yMax <= viewPos.y)
            {
                viewPos.y = -yMax + fudgeFactor * objectHeight;
            }
        }
        else    // !wrap is bounce
        {
            if (viewPos.x <= -xMax || xMax <= viewPos.x)
            {
                vel.x = -1 * vel.x;
                ship.moveVelocity = vel;
            }
            if (viewPos.y <= -yMax || yMax <= viewPos.y)
            {
                vel.y = -1 * vel.y;
                ship.moveVelocity = vel;
            }

        }
        viewPos.x += vel.x * Time.deltaTime;
        viewPos.y += vel.y * Time.deltaTime;
        transform.position = viewPos;
    }
}