using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject {

    public Vector3 CurrentPosition;
    public int Health = 4;
    public bool FacingRight;

    // Ability
    public bool AchievedDoubleJump = false;
    public bool AchievedDash = false;
    public bool AchievedWallJump = false;
    // Add other abilities 

}
