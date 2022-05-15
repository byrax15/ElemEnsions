using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _proximityIndicatorPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private PlayerController _playerController;
    
    private GameObject _currentInteractable;
    
    //KEY: Interactable object, VALUE: Array of 2 indicators (close and far)
    private Dictionary<GameObject, GameObject[]> _indicatorsByInteractables;

    private const float SHOW_PROXIMITY_INDICATOR_DISTANCE = 8.0f;
    private const float SHOW_INTERACTION_INDICATOR_DISTANCE = 1.5f;
    private const int CLOSE_INDEX = 0;
    private const int FAR_INDEX = 1;
    
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
        _indicatorsByInteractables = new Dictionary<GameObject, GameObject[]>();
        
        List<GameObject> interactables = FindObjectsOfType<Interactable>().Select(behaviour => behaviour.gameObject)
                                                                           .ToList();
        
        foreach (GameObject interactable in interactables)
        {
            GameObject closeIndicator =
                Instantiate(interactable.GetComponent<Interactable>().InteractionIndicatorPrefab, _canvas.transform);
            GameObject farIndicator = Instantiate(_proximityIndicatorPrefab, _canvas.transform);
            _indicatorsByInteractables.Add(interactable, new[] { 
                closeIndicator, 
                farIndicator
            });
        }
    }

    private void UpdateIndicators()
    {
        _currentInteractable = null;
        
        foreach ((GameObject key, GameObject[] values) in _indicatorsByInteractables)
        {
            if(key == null)
            {
                _indicatorsByInteractables.Remove(key);
                return;
            }

            if (_playerController.HeldItem == key || !key.activeSelf)
            {
                values[CLOSE_INDEX].SetActive(false);
                values[FAR_INDEX].SetActive(false);
                continue;
            }
            
            Vector3 interactableObjectPosition = key.transform.position;
            Vector3 viewportPosition = _playerCamera.WorldToViewportPoint(interactableObjectPosition);
            float distance = Vector3.Distance(_playerTransform.position, interactableObjectPosition);
            
            int indicatorIndexToShow = distance < SHOW_INTERACTION_INDICATOR_DISTANCE && _currentInteractable == null ? CLOSE_INDEX : FAR_INDEX;
            bool showIndicator = distance < SHOW_PROXIMITY_INDICATOR_DISTANCE && IsVisibleByPlayerCamera(key);

            if (showIndicator)
            {
                values[indicatorIndexToShow].GetComponent<RectTransform>().anchorMax = viewportPosition;
                values[indicatorIndexToShow].GetComponent<RectTransform>().anchorMin = viewportPosition;
                values[indicatorIndexToShow].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                if (indicatorIndexToShow == CLOSE_INDEX)
                    _currentInteractable = key;
            }
            
            values[CLOSE_INDEX].SetActive(false);
            values[FAR_INDEX].SetActive(false);
            
            values[indicatorIndexToShow].SetActive(showIndicator);
        }
    }

    private bool IsVisibleByPlayerCamera(GameObject objectToCheck)
    {
        Vector3 viewportPosition = _playerCamera.WorldToViewportPoint(objectToCheck.transform.position);
        return viewportPosition.z > 0 && viewportPosition.x is > 0 and < 1 && viewportPosition.y is > 0 and < 1;
    }

    public void DoCurrentInteraction()
    {
        if (_currentInteractable == null) return;

        if (_currentInteractable.GetComponent<Interactable>().Interact())
        {
            _indicatorsByInteractables[_currentInteractable][CLOSE_INDEX].SetActive(false);
            _indicatorsByInteractables[_currentInteractable][FAR_INDEX].SetActive(false);

            if (!_currentInteractable.GetComponent<Interactable>().CanInteractMultipleTime)
                _indicatorsByInteractables.Remove(_currentInteractable);
        }
    }
}
