using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] private Transform _playerTF;
    private LineRenderer _lineTongue;
    private Hitch _hitch;
    private Vector3 _hitchPosition;
    private bool _hook;

    private void Start()
    {
        _lineTongue = GetComponent<LineRenderer>();
        _lineTongue.enabled = false;
    }

    public void SetInitialPosition(bool lookAtRight)
    {
        if(lookAtRight)
            _lineTongue.SetPosition(0, new Vector3(-0.3f, 0.55f, 0));
        else
            _lineTongue.SetPosition(0, new Vector3(0.3f, 0.55f, 0));
    }

    public void Hook(Hitch hitch, Vector3 hitchPosition)
    {
        _playerTF.gameObject.GetComponent<Animator>().SetBool("TongueHook", true);
        _hitch = hitch;
        _lineTongue.enabled = true;
        _hitchPosition = hitchPosition;

        StartCoroutine(TongueShot(_hitchPosition));
    }

    private IEnumerator TongueShot(Vector3 hitchPosition)
    {
        for (int i = 1; i < 10; i++)
        {
            _lineTongue.SetPosition(1, ((hitchPosition - _playerTF.position) * i * 0.1f));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        _hook = true;

        _hitch.HitchShot();
    }

    private void Update()
    {
        if (_hook)
        {
            if (Vector2.Distance(_hitchPosition, _playerTF.position) >= 1)
                _lineTongue.SetPosition(1, _hitchPosition - _playerTF.position);
            else
            {
                _lineTongue.enabled = false;
                _playerTF.gameObject.GetComponent<Animator>().SetBool("TongueHook", false);
                _hook = false;
                _hitch.Impulse(1);
                _hitch.DisableMotion();
            }
        }
    }
}
