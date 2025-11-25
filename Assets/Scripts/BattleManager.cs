using UnityEngine;

public class BattleManager : MonoBehaviour
{
  [SerializeField] private Transform pfCharacterCombat;

    private void Start (){
        Instantiate(pfCharacterCombat, new Vector3(-5, 5), Quaternion.identity);
        Instantiate(pfCharacterCombat, new Vector3(+5, 5), Quaternion.identity);
    }

}
