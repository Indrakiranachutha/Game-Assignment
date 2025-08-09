using UnityEngine;
using UnityEngine.UI;
/// Displays only the grid position numbers of the cube the mouse is hovering over.
/// Shows "Not Specified" if no cube is detected.
public class GridCellPosition : MonoBehaviour
{
    [SerializeField] private Text UITextElement;

    void Update()
    {
        if (UITextElement == null || Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform hitTransform = hit.collider.transform;
            if (hitTransform != null)
            {
                Vector3 pos = hitTransform.position;
                int x = Mathf.RoundToInt(pos.x);
                int y = Mathf.RoundToInt(pos.z);

                UITextElement.text = $"{x}, {y}";
                return;
            }
        }

        UITextElement.text = "Not Specified";
    }
}
