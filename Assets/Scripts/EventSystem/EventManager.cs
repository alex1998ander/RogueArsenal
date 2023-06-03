using System;

// Event naming convention: On + [contributor] + [action]
// Examples for contributors: Player, Enemy, Weapon

public static class EventManager {

    /// <summary>
    /// Player fires his weapon. This event is triggered regardless of the number of bullets or shots.
    /// </summary>
    public static readonly Event OnPlayerFire = new();
    

}

