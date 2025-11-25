using System.Collections.Generic;
using UnityEngine;

// Minimal selection manager: can be extended with UI.
// Add UnitData assets into availableUnits in inspector.
public class PartySelectionManager : MonoBehaviour
{
    [Header("Available units (drag UnitData assets here)")]
    public List<UnitData> availableUnits = new List<UnitData>();

    [Header("Selection")]
    public List<UnitData> selectedUnits = new List<UnitData>();
    public int maxPartySize = 4;

    // Call from UI to toggle selection
    public void ToggleSelect(UnitData data)
    {
        if (data == null) return;

        if (selectedUnits.Contains(data))
        {
            selectedUnits.Remove(data);
            Debug.Log($"Deselected {data.unitName}");
        }
        else
        {
            if (selectedUnits.Count >= maxPartySize)
            {
                Debug.Log("Party full");
                return;
            }
            selectedUnits.Add(data);
            Debug.Log($"Selected {data.unitName}");
        }
    }

    // Confirm selection and start battle
    public void ConfirmSelection()
    {
        if (selectedUnits.Count == 0)
        {
            Debug.LogWarning("No units selected!");
            return;
        }

        BattleManager.Instance.StartBattle(selectedUnits);
    }

    // Helper to clear selection (UI can call)
    public void ClearSelection()
    {
        selectedUnits.Clear();
    }
}