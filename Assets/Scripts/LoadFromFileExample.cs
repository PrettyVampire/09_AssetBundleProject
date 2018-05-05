using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadFromFileExample : MonoBehaviour
{
    void Start()
    {
        string path = "Assets/AssetBundles/scene/sphere.test";
        //AssetBundle share = AssetBundle.LoadFromFile("Assets/AssetBundles/material.test");//如果材质是独立的一个包，就需要先加载材质

        AssetBundle assetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/AssetBundles");
        AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] dependencies = manifest.GetAllDependencies("scene/sphere.test"); //Pass the name of the bundle you want the dependencies for.
        foreach (string dependency in dependencies)
        {
            AssetBundle.LoadFromFile(Path.Combine("Assets/AssetBundles", dependency));
        }

        //从本地加载
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        //从内存加载
        //AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));

        if (ab == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = ab.LoadAsset<GameObject>("Sphere");
        GameObject.Instantiate(prefab);
    }

    IEnumerator Start1()
    {
        while (!Caching.ready)
            yield return null;

        //路径绝对地址前加 @ file//或者file///的标识
        //string wwwPath = @"file://D:/Unity3d/APlan_Siki/09_AssetBundleProject/Assets/AssetBundles/scene/sphere.test";
        string wwwPath = @"http://172.0.0.1/AssetBundles/scene/sphere.test";
        var www = WWW.LoadFromCacheOrDownload(wwwPath, 5);
        yield return www;//等待加载完毕
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;//迭代停止
        }

        AssetBundle ab = www.assetBundle;

        var prefab = ab.LoadAsset<GameObject>("Sphere");
        GameObject.Instantiate(prefab);
    }

    IEnumerator Start2()
    {
        //服务器地址
        string uri = @"http://localhost//AssetBundles/scene/sphere.test";
        //本地地址
        //string uri = @"file://D:/Unity3d/APlan_Siki/09_AssetBundleProject/Assets/AssetBundles/scene/sphere.test";
        UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();//开始下载到缓存
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        var prefab = ab.LoadAsset<GameObject>("Sphere");
        GameObject.Instantiate(prefab);
    }
}
