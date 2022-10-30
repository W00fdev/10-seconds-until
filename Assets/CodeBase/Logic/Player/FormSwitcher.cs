using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UNUSED
/// </summary>
/*public class FormSwitcher : MonoBehaviour
{
    public enum FormType { BALL = 0, CUBE = 1, SWORD = 2 };

    private FormType _currentForm;

    [SerializeField] private EnemyDirector _enemyDirector;
    [SerializeField] private CameraFollow _cameraFollow;

    [Header("SPHERE COMPONENTS")]
    [SerializeField] private Rigidbody _rigidbodyBall;
    [SerializeField] private GameObject _ballFish;

    [Header("CUBE COMPONENTS")]
    [SerializeField] private Rigidbody _rigidbodyCube;
    [SerializeField] private GameObject _cubeFish;


    private void Start()
    {
    }


    public void SwitchFormRandomly()
    {
        int formIndex = (int)_currentForm;
        while (formIndex == (int)_currentForm)
            formIndex = Random.Range(0, 2);
        
        SwitchForm((FormType)formIndex);
    }

    public void SwitchForm(FormType newForm)
    {
        DisableCurrentForm();

        switch (newForm)
        {
            case FormType.BALL:
                _ballFish.transform.position = _cubeFish.transform.position;
                _ballFish.SetActive(true);
                _ballFish.GetComponent<PlayerShooting>()._fireReady = true;

                _rigidbodyBall.velocity = Vector3.zero;
                _rigidbodyBall.isKinematic = false;

                SwitchTargetsOnNewForm(_ballFish.transform);
                _cameraFollow.Distance = 5;
                break;

            case FormType.CUBE:
                _cubeFish.transform.position = _ballFish.transform.position;
                _cubeFish.SetActive(true);

                _rigidbodyCube.velocity = Vector3.zero;
                _rigidbodyCube.isKinematic = false;
                
                SwitchTargetsOnNewForm(_cubeFish.transform);
                _cameraFollow.Distance = 9;
                break;
        }

        _currentForm = newForm;
    }

    private void SwitchTargetsOnNewForm(Transform newTarget)
    {
        _cameraFollow.SwitchTarget(newTarget);
        _enemyDirector.SwitchTargetForEnemies(newTarget);
    }

    private void DisableCurrentForm()
    {
        switch(_currentForm)
        {
            case FormType.BALL:
                _ballFish.SetActive(false);

                _rigidbodyBall.velocity = Vector3.zero;
                _rigidbodyBall.isKinematic = true;
                break;

            case FormType.CUBE:
                _cubeFish.SetActive(false);

                _rigidbodyCube.velocity = Vector3.zero;
                _rigidbodyCube.isKinematic = true;
                break;
        }
    }
}*/
