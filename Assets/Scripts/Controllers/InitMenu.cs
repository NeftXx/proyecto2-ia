using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Util;

namespace Controller
{
  public class InitMenu : MonoBehaviour
  {
    private bool isDatabaseOk = false;

    private void Awake()
    {
      StartCoroutine(ExtractDatabase());
    }

    private IEnumerator Start()
    {
      yield return new WaitUntil(() => isDatabaseOk);
      StartCoroutine(MainTable.Instance.DrawTable());
    }

    private IEnumerator ExtractDatabase()
    {
      if (!File.Exists(Configuration.Properties.DatabasePath))
      {
#if UNITY_EDITOR || UNITY_STANDALONE
        File.Copy(
          Configuration.Properties.DatabaseStreamingAssetsPath,
          Configuration.Properties.DatabasePath
        );
#elif UNITY_ANDROID || UNITY_IOS
				yield return StartCoroutine(CopyDatabase());
#endif
      }
      isDatabaseOk = true;
      yield return null;
    }

    private IEnumerator CopyDatabase()
    {
      using (UnityWebRequest request = UnityWebRequest.Get(
        Configuration.Properties.MobileDatabasePath
      ))
      {
        yield return request.SendWebRequest();
        if (string.IsNullOrEmpty(request.error))
        {
          File.WriteAllBytes(
            Configuration.Properties.DatabasePath,
            request.downloadHandler.data
          );
        }
      }
    }
  }
}