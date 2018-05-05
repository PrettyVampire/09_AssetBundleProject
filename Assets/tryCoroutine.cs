using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryCoroutine : MonoBehaviour {

    private GameObject m_obj;
    private ResourceRequest m_request;
	// Use this for initialization
	void Start () {
        StartCoroutine(LoadResources());
        Debug.Log("开始协程");
	}
	
	IEnumerator LoadResources()
    {
        m_request = Resources.LoadAsync("Cube");
        yield return m_request;

        Debug.Log("异步加载完成了~~");
        m_obj = m_request.asset as GameObject;
        if (!m_obj)
        {
            Debug.Log("加载object失败");
        }

        GameObject obj = GameObject.Instantiate(m_obj);
    }

    private void Update()
    {
        if(m_request == null)
        {
            Debug.Log("loading....");
        }
    }
}
