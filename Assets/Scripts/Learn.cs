using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Learn : MonoBehaviour
{
    [SerializeField] private int times;
    [SerializeField] private Button btn;
    [SerializeField] private Text textMeshPro;
    private float _waitTime;

    public Func<UniTask<string>> wait;
    private void Start()
    {
        btn.onClick.AddListener(Click);
        wait += Wait;
    }
    
    private void Click()
    {
        wait.Invoke();
    }

    private async UniTask<string>Wait()
    {
        return await Open();
    }

    private async UniTask<string> Open()
    {
        for (var i = 0; i < times; i++)
        {
            await UniTask.Delay(1000);
            _waitTime += 1f;
        }
        Debug.Log("111111111111111");
        var text = _waitTime.ToString(CultureInfo.InvariantCulture);
        textMeshPro.text = text;
        return text;
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(Click);
        wait -= Wait;
    }
}