using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphPoint : MonoBehaviour
{
    [SerializeField] private RectTransform _point;
    [SerializeField] private LineRenderer _graphLine;
    [SerializeField] private TextMeshProUGUI _minuteText;

    public void Init(float yIncrement, int minute)
    {
        _point.anchoredPosition = new Vector2(0, _point.anchoredPosition.y + yIncrement);
        _minuteText.text = minute.ToString();
    }

    public void Connect()
    {
        // Update grid...
        transform.parent.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(true);

        Vector3 worldPos = _point.transform.position;
        worldPos.z = 0;

        _graphLine.positionCount = _graphLine.positionCount + 1;
        _graphLine.SetPosition(_graphLine.positionCount - 1, worldPos);
    }
}