using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject _fist, _foot;

    private float _atkDuration = 0.05f;
    private float _recoilDuration = 0.1f;

    public IEnumerator FistAnimation(bool facingLeft, Vector3 pos, float range)
    {
        _fist.GetComponent<MeshRenderer>().enabled = true;

        float time = 0;
        Vector3 targetPos = facingLeft ? pos - new Vector3(range, 0, 0) : pos + new Vector3(range, 0, 0);


        while (time < _atkDuration)
        {
            _fist.transform.position = Vector3.Lerp(pos, targetPos, time / _atkDuration);
            time += Time.deltaTime;

            yield return null;
        }

        _fist.transform.position = targetPos;
        time = 0;

        while (time < _recoilDuration)
        {
            _fist.transform.position = Vector3.Lerp(targetPos, pos, time / _recoilDuration);
            time += Time.deltaTime;

            yield return null;
        }

        _fist.transform.position = pos;

        _fist.GetComponent<MeshRenderer>().enabled = false;
    }

    public IEnumerator FootAnimation(bool facingLeft, Vector3 pos, float range)
    {
        _foot.GetComponent<MeshRenderer>().enabled = true;

        float time = 0;

        Vector3 startPos = pos - new Vector3(0, 0.7f, 0);
        Vector3 targetPos = facingLeft ? pos - new Vector3(range, 0, 0) : pos + new Vector3(range, 0, 0);


        while (time < _atkDuration)
        {
            _foot.transform.position = Vector3.Lerp(startPos, targetPos, time / _atkDuration);
            time += Time.deltaTime;

            yield return null;
        }

        _foot.transform.position = targetPos;
        time = 0;

        while (time < _recoilDuration)
        {
            _foot.transform.position = Vector3.Lerp(targetPos, startPos, time / _recoilDuration);
            time += Time.deltaTime;

            yield return null;
        }

        _foot.transform.position = startPos;

        _foot.GetComponent<MeshRenderer>().enabled = false;
    }
}
