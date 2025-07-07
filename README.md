# RetinaDamage

A small Unity component that damages the player’s eyes if they stare at a directional light (the “sun”) for too long.  
This approach is tried and tested in several projects and works out of the box.

## How to use

1. Add the script to any GameObject.
2. Set `looker` to the player’s camera transform.
3. Leave **`target` empty** to let the script find the first `Directional Light` automatically.

## What it does

- Checks if the camera is looking at the light within a set angle (`thresholdAngle`, default 16°).  
- Only runs between 08:00 and 17:00. (Hook up to your own in-game day/night cycle) 
- When the player stares too long, calls `DamagePlayer()` once per second.

## License

MIT. Use freely in personal or commercial projects.
