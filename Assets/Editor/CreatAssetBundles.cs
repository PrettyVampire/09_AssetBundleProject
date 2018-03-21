using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 资源打包
/// </summary>
public class CreateAssetBundles
{
    //相当于是拓展编译器，在Assets下创建一个Build AssetBundles菜单
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");//"Assets/AssetBundles";
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        //打包的输出路径、打包方式、平台
        /* BuildAssetBundleOptions
         * BuildAssetBundleOptions.None：使用LZMA算法压缩，压缩的包更小，但是加载时间更长。使用之前需要整体解压。
         *          一旦被解压，这个包会使用LZ4重新压缩。使用资源的时候不需要整体解压。在下载的时候可以使用LZMA算法，
         *          一旦它被下载了之后，它会使用LZ4算法保存到本地上。
         * BuildAssetBundleOptions.UncompressedAssetBundle：不压缩，包大，加载快
         * BuildAssetBundleOptions.ChunkBasedCompression：使用LZ4压缩，压缩率没有LZMA高，但是我们可以加载指定资源而不用解压全部。
         */
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}