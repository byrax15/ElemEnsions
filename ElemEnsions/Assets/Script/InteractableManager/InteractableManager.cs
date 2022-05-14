using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private Transform _playerTransform;
    
    //KEY: IInteractable, VALUE: Indicator
    private Dictionary<GameObject, GameObject> _indicatorsByInteractables;
    
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
            GameObject handIndicator = Instantiate(_indicatorPrefab, _canvas.transform);
            _indicatorsByInteractables.Add(interactable, handIndicator);
        }
    }

    private void UpdateIndicators()
    {
        foreach (var pair in _indicatorsByInteractables)
        {
            if (pair.Key == null)
            {
                _indicatorsByInteractables.Remove(pair.Key);
                return;
            }
            
            Vector3 pos = Camera.main.WorldToViewportPoint(pair.Key.transform.position) - Vector3.up * 0.02f;

            if (Vector3.Distance(_playerTransform.position, pair.Key.transform.position) < 2 && pos.z > 0 && pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1)
            {
                pair.Value.GetComponent<RectTransform>().anchorMax = pos;
                pair.Value.GetComponent<RectTransform>().anchorMin = pos;
                pair.Value.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                pair.Value.SetActive(true);
            }
            else
            {
                pair.Value.SetActive(false);
            }
        }
    }
}
