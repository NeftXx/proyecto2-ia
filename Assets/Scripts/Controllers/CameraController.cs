using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using Service;

public class CameraController : MonoBehaviour
{
  private SpaceService spaceService;

  void Awake()
  {
    spaceService = new SpaceService();
  }

  void Start()
  {
    VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
  }

  void AddGameObject(Transform transform, long furnitureId, Vector3 position)
  {
    switch (furnitureId)
    {
      case 1:
        GameObject silla = GameObject.CreatePrimitive(PrimitiveType.Cube);
        silla.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        silla.transform.position = position;
        silla.GetComponent<Renderer>().material.color = Color.blue;
        silla.transform.SetParent(transform);
        break;
      case 2:
        GameObject mesa = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mesa.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        mesa.transform.position = position;
        mesa.GetComponent<Renderer>().material.color = Color.red;
        mesa.transform.SetParent(transform);
        break;
      case 3:
        GameObject computador = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        computador.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        computador.transform.position = position;
        computador.GetComponent<Renderer>().material.color = Color.green;
        computador.transform.SetParent(transform);
        break;
      case 4:
        GameObject sillon = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sillon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sillon.transform.position = position;
        sillon.GetComponent<Renderer>().material.color = Color.yellow;
        sillon.transform.SetParent(transform);
        break;
      case 5:
        GameObject lamparas = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        lamparas.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        lamparas.transform.position = position;
        lamparas.GetComponent<Renderer>().material.color = Color.cyan;
        lamparas.transform.SetParent(transform);
        break;
    }
  }

	void CrearEspacio(Model.Space space, string recurso, string nombre)
	{
		var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
    var runtimeImageSource = objectTracker.RuntimeImageSource;

    runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, recurso, 0.7f, nombre);

    var dataset = objectTracker.CreateDataSet();
    var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, nombre);

    trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
    objectTracker.ActivateDataSet(dataset);

		var transform = trackableBehaviour.gameObject.transform;
		AddGameObject(transform, space.UpperLeftId, new Vector3(0.0f, 0.0f, 0.0f));
		AddGameObject(transform, space.UpperRightId, new Vector3(0.2f, 0.0f, 0.0f));
		AddGameObject(transform, space.CenterId, new Vector3(0.4f, 0.0f, 0.0f));
		AddGameObject(transform, space.LowerLeftId, new Vector3(0.6f, 0.0f, 0.0f));
		AddGameObject(transform, space.LowerRightId, new Vector3(0.8f, 0.0f, 0.0f));
	}

  void CreateImageTargetFromSideloadedTexture()
  {
    var spaces = spaceService.ListAll();
    foreach (var space in spaces)
    {
      switch (space.FlatId)
      {
        case 1:
          CrearEspacio(space, "Vuforia/Piso_Madera.png", "Piso_Madera");
          break;
        case 2:
          CrearEspacio(space, "Vuforia/Piso_Piedra.png", "Piso_Piedra");
          break;
        case 3:
          CrearEspacio(space, "Vuforia/Piso_Cemento.jpg", "Piso_Cemento");
          break;
        case 4:
          CrearEspacio(space, "Vuforia/Piso_Marmol.jpg", "Piso_Marmol");
          break;
        case 5:
          CrearEspacio(space, "Vuforia/Piso_Cristal.jpg", "Piso_Cristal");
          break;
        case 6:
          CrearEspacio(space, "Vuforia/Piso_Ladrillo.jpg", "Piso_Ladrillo");
          break;
      }
    }
  }
}
