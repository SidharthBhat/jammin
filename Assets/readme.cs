/*
 * Some things to note about how this works
 * Enemy is the whole enemy. EnemyMain is the actual enemy that works n all
 * PatrolPoints are its patrol route, it goes through the points in the list and wraps around, so patrol routes must form a loop
 * 
 * Player must be on the player layer and tagged Player
 * Obstructions, like doors and walls, must be on the obstruction layer, and have an empty child with the same hitbox, and that child on the player layer
*/