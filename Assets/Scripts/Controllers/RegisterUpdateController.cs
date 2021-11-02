using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Service;
using Util;

namespace Controller
{
  public class RegisterUpdateController : MonoBehaviour
  {
    [Header("Needed UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown flatDropdown;
    [SerializeField] private TMP_Dropdown chairDropdown;
    [SerializeField] private TMP_Dropdown deskDropdown;
    [SerializeField] private TMP_Dropdown computerDropdown;
    [SerializeField] private TMP_Dropdown armChairDropdown;
    [SerializeField] private TMP_Dropdown lampsDropdown;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button okButton;

    private bool isUpdate = false;
    private Model.Space space;
    private SpaceService spaceService;

    private void Awake()
    {
      space = new Model.Space();
      spaceService = new SpaceService();
      if (Transporter.Instance != null && Transporter.Instance.IsUpdate)
      {
        LoadSpace(Transporter.Instance.SpaceId);
      }
      titleText.text = (isUpdate ? "Actualizar espacio" : "Registrar nuevo espacio");
      SetEventListeners();
    }

    private void AssignValueDropdown(long key, int value)
    {
      switch(key)
      {
        case 1:
          chairDropdown.value = value;
          break;
        case 2:
          deskDropdown.value = value;
          break;
        case 3:
          computerDropdown.value = value;
          break;
        case 4:
          armChairDropdown.value = value;
          break;
        case 5:
          lampsDropdown.value = value;
          break;
      }
    }

    private void LoadSpace(long id)
    {
      if (id <= 0)
      {
        isUpdate = false;
        return;
      }
      space = spaceService.GetById(id);
      nameInput.text = space.Name;
      flatDropdown.value = (int)space.FlatId - 1;
      AssignValueDropdown(space.UpperLeftId, 0);
      AssignValueDropdown(space.UpperRightId, 1);
      AssignValueDropdown(space.CenterId, 2);
      AssignValueDropdown(space.LowerLeftId, 3);
      AssignValueDropdown(space.LowerRightId, 4);
      isUpdate = true;
    }

    private void SetEventListeners()
    {
      cancelButton.onClick.AddListener(BackToPreviousScene);
      okButton.onClick.AddListener(() => Save(isUpdate));
    }

    private void BackToPreviousScene()
    {
      SceneManager.LoadScene(Configuration.Scenes.SpacesMenu);
    }

    private bool CheckIfTextIsNullOrEmpty(string text) =>
      string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);

    private IEnumerator CountAndGetBack()
    {
      yield return new WaitForSecondsRealtime(3f);
      BackToPreviousScene();
    }

    private bool VerifyPositions()
    {
      int chair = chairDropdown.value;
      int desk = deskDropdown.value;
      int computer = computerDropdown.value;
      int armChair = armChairDropdown.value;
      int lamps = lampsDropdown.value;

      bool chairCorrectlyPositioned = chair != desk && chair != computer && chair != armChair && chair != lamps;
      bool deskCorrectlyPositioned = desk != chair && desk != computer && desk != armChair && desk != lamps;
      bool computerCorrectlyPositioned = computer != chair && computer != desk && computer != armChair && computer != lamps;
      bool armChairCorrectlyPositioned = armChair != chair && armChair != desk && armChair != computer && armChair != lamps;
      bool lampsCorrectlyPositioned = lamps != chair && lamps != desk && lamps != computer && lamps != armChair;

      return chairCorrectlyPositioned &&
        deskCorrectlyPositioned &&
        computerCorrectlyPositioned &&
        armChairCorrectlyPositioned &&
        lampsCorrectlyPositioned;
    }

    private void SetPositions(TMP_Dropdown dropdown, long idFurniture)
    {
      switch (dropdown.value)
      {
        case 0:
          space.UpperLeftId = idFurniture;
          break;
        case 1:
          space.UpperRightId = idFurniture;
          break;
        case 2:
          space.CenterId = idFurniture;
          break;
        case 3:
          space.LowerLeftId = idFurniture;
          break;
        case 4:
          space.LowerRightId = idFurniture;
          break;
      }
    }

    private bool CheckRepeatedFlat(int flatId)
    {
      return flatId != space.FlatId && spaceService.CheckRepeatedFlat(flatId);
    }

    private void Save(bool isUpdate)
    {
      try
      {
        if (isUpdate && space.Id <= 0)
        {
          titleText.text = "Error en el Id del espacio!";
          ProjectDebug.LogError("Error en el Id del espacio!");
          return;
        }

        if (CheckIfTextIsNullOrEmpty(nameInput.text))
        {
          titleText.text = "El nombre del espacio no puede estar vacío!";
          ProjectDebug.LogError("El nombre del espacio no puede estar vacío!");
          return;
        }

        space.Name = nameInput.text;
        int flatId = flatDropdown.value + 1;
        if (CheckRepeatedFlat(flatId))
        {
          titleText.text = "Ya existe un espacio con el mismo tipo de piso!";
          ProjectDebug.LogError("Ya existe un espacio con el mismo tipo de piso!");
          return;
        }
        space.FlatId = flatId;
        bool correctPostions = VerifyPositions();

        if (correctPostions)
        {
          SetPositions(chairDropdown, 1);
          SetPositions(deskDropdown, 2);
          SetPositions(computerDropdown, 3);
          SetPositions(armChairDropdown, 4);
          SetPositions(lampsDropdown, 5);
        }
        else
        {
          titleText.text = "Posiciones incorrectas! Verifique que las posiciones no esten repetidas.";
          ProjectDebug.LogError("Posiciones incorrectas! Verifique que las posiciones no esten repetidas.");
          return;
        }

        bool success = spaceService.Save(space);

        if (success)
        {
          if (isUpdate)
          {
            titleText.text = "Espacio actualizado!";
            ProjectDebug.Log("Espacio actualizado!");
          }
          else
          {
            titleText.text = "Espacio registrado!";
            ProjectDebug.Log("Espacio registrado!");
          }
          okButton.interactable = false;
          CountAndGetBack();
        }
        else
        {
          titleText.text = "Error al guardar el espacio!";
          ProjectDebug.LogError("Error al guardar el espacio!");
        }
      }
      catch (System.Exception ex)
      {
        ProjectDebug.LogErrorFormat("Save -> {0}", ex.Message);
        throw ex;
      }
    }
  }
}
