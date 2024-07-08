using UnityEngine;

[CreateAssetMenu(menuName ="Connection")]
public class Connection : ScriptableObject
{
    public static Connection ActiveConnection { get; set; }
}