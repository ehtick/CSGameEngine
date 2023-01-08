### LOG 1 - 03/01/2023 (Only Multiplayer)
Added Features
- added worker thread system, where players take turns sending and receiving data to prevent simultaneous edits of the master player list

Patches:
- fixed problem with player rendering, where player would be rendered 25 pixels down and right of their actual position. This problem was due to the way that Raylib rendered circles, where the coordinates would be at the center of the circle, instaed of the top right corner of its rectangle equivilent.


### Log 2 - 04/01/2023 (Only Multiplayer)
Added Features
- added gamemode initiation system where the player obtains all of the stat boosters from the given gamemode


### Log 3 - 08/01/2023 (Only Multiplayer)
Added Features
- added white player texture rotation instead of loading animation
- added coloured player textures by tinting depending on context, gold for player, light gray for dead player, bright red for alive enemy, faded red for spectating enemies
- added player rotation speed to configuration file
- started player combat system added spectator mode for when player dies

Patches:
- fixed Rlgl problem where player was creating trail of other players behind them
- fixed low fps issue with Rlgl rotations
- player rendering is not independent of other entity loading so that fog does not impact the player's image