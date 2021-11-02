using Util;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

namespace Controller
{
  public class MainTable : MonoBehaviour
  {
    [Header("Needed UI Elements")]
    [SerializeField] private CanvasGroup tableCanvasGroup;
    [SerializeField] private Transform tableContent;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject textColumnPrefab;
    [SerializeField] private GameObject textButtonColumnPrefab;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button newSpaceButton;

    private Vector3 defaultPosition;
    private SpaceService _spaceService;

    public static MainTable Instance { get; private set; }

    private void Awake()
    {
      Instance = this;
      _spaceService = new SpaceService();
      SetEventListeners();
    }

    private void Start()
    {
    }

    private void SetEventListeners()
    {
      newSpaceButton.onClick.AddListener(() => GotoRegisterUpdate(0));
      prevButton.onClick.AddListener(() => GotoMainMenu());
    }

    private void GotoMainMenu()
    {
      SceneManager.LoadScene(Configuration.Scenes.MainMenu);
    }

    private void GotoRegisterUpdate(long id)
    {
      long count = _spaceService.Count();
      bool isUpdate = id > 0;
      Transporter.Instance.IsUpdate = isUpdate;
      Transporter.Instance.SpaceId = id;
      if (isUpdate)
      {
        SceneManager.LoadScene(Configuration.Scenes.RegisterUpdate);
      }
      else if (count < 6)
      {
        SceneManager.LoadScene(Configuration.Scenes.RegisterUpdate);
      }
      else
      {
        ProjectDebug.LogError("No puedes registrar más de 6 espacios");
      }
    }

    private IEnumerator ClearTable()
    {
      foreach (Transform child in tableContent)
      {
        Destroy(child.gameObject);
      }
      yield return new WaitUntil(() => tableContent.childCount == 0);
    }

    private GameObject CreateColumn(GameObject prefab, Transform parent, string value, Color32? color = null)
    {
      GameObject column = Instantiate(prefab);
      column.transform.SetParent(parent);
      column.transform.SetAsLastSibling();
      TextMeshProUGUI textMeshPro = column.GetComponent<TextMeshProUGUI>();
      if (textMeshPro != null)
      {
        textMeshPro.text = value;
        if (color != null)
        {
          textMeshPro.color = color.Value;
        }
      }
      RectTransform rectTransform = column.GetComponent<RectTransform>();
      if (rectTransform != null)
      {
        rectTransform.localScale = Vector3.one;
      }
			return column;
    }

		private void CreateTextColumn(Transform rowParent, string value) => CreateColumn(textColumnPrefab, rowParent, value);

		private void CreateButtonColumn(Transform rowParent, string value, UnityAction callback, Color? color = null)
		{
			GameObject column = CreateColumn(textButtonColumnPrefab, rowParent, value, color);
			Button button = column.GetComponent<Button>();
			if (button != null)
			{
				button.onClick.AddListener(callback);
			}
		}

    private IEnumerator DeleteSpace(long id)
    {
      if (id <= 0)
      {
        yield break;
      }
      bool hasDeleted = _spaceService.DeleteById(id);
      if (hasDeleted)
      {
        ProjectDebug.Log("Espacio eliminado!");
        tableCanvasGroup.alpha = 0.5f;
        tableCanvasGroup.interactable = false;
        yield return new WaitForSecondsRealtime(1f);
        tableCanvasGroup.alpha = 1f;
        tableCanvasGroup.interactable = true;
        yield return StartCoroutine(DrawTable());
      }
    }

		public IEnumerator DrawTable()
		{
			yield return StartCoroutine(ClearTable());
			List<Model.Space> spaces = _spaceService.ListAll();
			foreach (Model.Space space in spaces)
			{
				GameObject row = Instantiate(rowPrefab);
				row.transform.SetParent(tableContent);
				row.transform.SetAsLastSibling();

				RectTransform rectTransform = row.GetComponent<RectTransform>();
				if (rectTransform != null)
				{
					rectTransform.localScale = Vector3.one;
				}
				CreateTextColumn(row.transform, space.Name);
        switch(space.FlatId)
        {
          case 1:
            CreateTextColumn(row.transform, "Madera");
            break;
          case 2:
            CreateTextColumn(row.transform, "Piedra");
            break;
          case 3:
            CreateTextColumn(row.transform, "Cemento");
            break;
          case 4:
            CreateTextColumn(row.transform, "Mármol");
            break;
          case 5:
            CreateTextColumn(row.transform, "Cristal");
            break;
          case 6:
            CreateTextColumn(row.transform, "Ladrillo");
            break;
        }
				CreateButtonColumn(
          row.transform,
          "Editar",
          () => GotoRegisterUpdate(space.Id),
          Color.green
        );
				CreateButtonColumn(
          row.transform,
          "Eliminar",
          () => StartCoroutine(DeleteSpace(space.Id)),
          Color.red
        );
			}
		}
  }
}
