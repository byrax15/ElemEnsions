using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _playerCamera;
    
    //KEY: IInteractable, VALUE: Indicator
    private Dictionary<GameObject, GameObject> _indicatorsByInteractables;

    private const float SHOW_PROXIMITY_INDICATOR_DISTANCE = 5.0f;
    private const float SHOW_INTERACTION_INDICATOR_DISTANCE = 2.0f;
    
    private void Awake()
    {
        InitializeIndicators();
    }

    private void LateUpdate()
    {
        UpdateIndicators();
    }

    private void InitializeIndicators()
    {
        _indicatorsByInteractables = new Dictionary<GameObject, GameObject>();
        
        List<GameObject> interactables = FindObjectsOfType<MonoBehaviour>().Where(behaviour => behaviour is IInteractable)
                                                                           .Select(behaviour => behaviour.gameObject)
                                                                           .ToList();
        
        foreach (GameObject interactable in interactables)
        {
            GameObject indicator = Instantiate(_indicatorPrefab, _canvas.transform);
            _indicatorsByInteractables.Add(interactable, indicator);
        }
    }

    private void UpdateIndicators()
    {
        foreach ((GameObject key, GameObject value) in _indicatorsByInteractables)
        {
            Vector3 interactableObjectPosition = key.transform.position;
            Vector3 viewportPosition = _playerCamera.WorldToViewportPoint(interactableObjectPosition);
            bool showIndicator = Vector3.Distance(_playerTransform.position, interactableObjectPosition) < SHOW_PROXIMITY_INDICATOR_DISTANCE &&
                                 IsVisibleByPlayerCamera(key);

            if (showIndicator)
            {
                value.GetComponent<RectTransform>().anchorMax = viewportPosition;
                value.GetComponent<RectTransform>().anchorMin = viewportPosition;
                value.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            
            value.SetActive(showIndicator);
        }
    }

    private bool IsVisibleByPlayerCamera(GameObject objectToCheck)
    {
        Vector3 viewportPosition = _playerCamera.WorldToViewportPoint(objectToCheck.transform.position);
        return viewportPosition.z > 0 && viewportPosition.x is > 0 and < 1 && viewportPosition.y is > 0 and < 1;
    }
}
