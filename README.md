# asteroidz
Asteroids in Unity3d. A simple portfolio piece. Unity 2019.22 LTS

Using:
* Prime[31] RecyclerKit for object pooling (changed a little bit)
* Prime[31] MessageKit
* Kenny Assets Future Font
* Dissolve Shader found here: https://gist.github.com/benloong/b25066cb140398b402f2ad8295a28d42

Everything else is by me (if I add sounds and forget to update this md, this probably isn't the case anymore!)

### Differences from classic Asteroids
* No space ships
* No levels; the game runs continuously and asteroids spawn faster and faster
* Asteroids don't provide scores - instead they drop targets which give scores
* Ammo limits

See AsteroidManager to adjust score, size, speed and spawn behaviour of asteroids. See the player prefab to adjust handling