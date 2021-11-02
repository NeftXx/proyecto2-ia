using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
  public class Configuration
  {
    public class Properties
    {
      public static string FileNameDebug => Application.dataPath + "/bitacora_201504420_201504240.txt";
      public static string DatabaseName => "database.s3db";
      public static string DatabasePath => string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
      public static string DatabaseStreamingAssetsPath => string.Format("{0}/StreamingAssets/{1}", Application.dataPath, DatabaseName);
#if UNITY_ANDROID
      public static string MobileDatabasePath => string.Format("jar:file://{0}!/assets/{1}", Application.dataPath, DatabaseName);
#elif UNITY_IOS
      public static string MobileDatabasePath => string.Format("file://{0}/Raw/{1}", Application.dataPath, DatabaseName);
#else
      public static string MobileDatabasePath => string.Empty;
#endif
    }

    public class Queries
    {
      public class Space
      {
        public static string Insert => "insert into `space` (`name`, `flat_id`, `upper_left_id`, `upper_right_id`, `center_id`, `lower_left_id`, `lower_right_id`) values ('{0}', {1}, {2}, {3}, {4}, {5}, {6});";
        public static string Update => "update `space` set `name` = '{0}', `flat_id` = {1}, `upper_left_id`={2}, `upper_right_id`={3}, `center_id`={4}, `lower_left_id`={5}, `lower_right_id`={6} where `id` = {7};";
        public static string Delete => "delete from `space` where `id` = {0};";
        public static string ListAll => "select `id` as Id, `name` as Name, `flat_id` as FlatId, `upper_left_id` as UpperLeftId, `upper_right_id` as UpperRightId, `center_id` as CenterId, `lower_left_id` as LowerLeftId, `lower_right_id` as LowerRightId from `space` order by `flat_id` asc;";
        public static string GetById => "select `id` as Id, `name` as Name, `flat_id` as FlatId, `upper_left_id` as UpperLeftId, `upper_right_id` as UpperRightId, `center_id` as CenterId, `lower_left_id` as LowerLeftId, `lower_right_id` as LowerRightId from `space` where `id` = {0} ";
        public static string CheckRepeatedFlat => "select count(`id`) as Count from `space` where `flat_id` = {0};";
        public static string Count => "select count(`id`) as Count from `space`;";
      }
      
      public class Flat
      {
        public static string ListAll => "select `id`, `name` from `flat` order by `id` asc;";
        public static string GetById => "select `id`, `name` from `flat` where `id` = {0} ";
      }

      public class Furniture
      {
        public static string ListAll => "select `id`, `name` from `furniture` order by `id` asc;";
        public static string GetById => "select `id`, `name` from `furniture` where `id` = {0} ";
      }
    }

    public class Scenes
    {
      public static string MainMenu => "MainMenuScene";
      public static string Camera => "CameraScene";
      public static string SpacesMenu => "SpacesMenuScene";
      public static string RegisterUpdate => "RegisterUpdateScene";
    }
  }
}
