using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgDemo
{
	/// <summary>
	/// 界面管理器
	/// </summary>
	public class UIManager : USingleton<UIManager>
	{
		public RectTransform RootCanvasTransform
		{
			get
			{
				return _rootCanvasTransform;
			}
		}
		private RectTransform _rootCanvasTransform;

		public Canvas RootCanvas
		{
			get
			{
				return _rootCanvas;
			}
		}
		private Canvas _rootCanvas;

		public CanvasScaler RootCanvasScaler
		{
			get
			{
				return _rootCanvasScaler;
			}
		}
		private CanvasScaler _rootCanvasScaler;

		public GraphicRaycaster RootGraphicRaycaster
		{
			get
			{
				return _rootGraphicRaycaster;
			}
		}
		private GraphicRaycaster _rootGraphicRaycaster;

		public EventSystem UIEvetSystem
		{
			get
			{
				return _uiEvetSystem;
			}
		}
		private EventSystem _uiEvetSystem;

		public StandaloneInputModule InputModule
		{
			get
			{
				return _inputModule;
			}
		}
		private StandaloneInputModule _inputModule;

		public Camera UICamera
		{
			get
			{
				return _uiCamera;
			}
		}
		private Camera _uiCamera;

		private UISettings _uiSettings;

		private Dictionary<string, RectTransform> _layeredNodes;

		public void Init()
		{
			_layeredNodes = new Dictionary<string, RectTransform>();
			_uiSettings = Resources.Load<UISettings>("UISettings");

			CreateCanvas();
			CreateEventSystem();
			AddLayers();
		}

		private void CreateCanvas()
		{
			GameObject canvasGameObject = new GameObject("Canvas");
			canvasGameObject.transform.SetParent(transform, false);
			canvasGameObject.layer = LayerMask.NameToLayer("UI");

			_rootCanvasTransform = canvasGameObject.AddComponent<RectTransform>();

			_rootCanvas = canvasGameObject.AddComponent<Canvas>();
			RootCanvas.renderMode = _uiSettings.renderMode;
			//RootCanvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;

			if (_uiSettings.renderMode == RenderMode.ScreenSpaceCamera)
			{
				CreateScreenSpaceCamera(_uiSettings.orthographic);
			}

			_rootCanvasScaler = canvasGameObject.AddComponent<CanvasScaler>();
			RootCanvasScaler.referenceResolution = _uiSettings.referenceResolution;
			RootCanvasScaler.uiScaleMode = _uiSettings.uiScaleMode;
			RootCanvasScaler.screenMatchMode = _uiSettings.screenMathMode;
			RootCanvasScaler.matchWidthOrHeight = _uiSettings.matchWidthOrHeight;

			_rootGraphicRaycaster = canvasGameObject.AddComponent<GraphicRaycaster>();
		}

		private void CreateScreenSpaceCamera(bool orthographic)
		{
			GameObject uiCamera = new GameObject("UICamera");
			uiCamera.transform.SetParent(transform, false);

			_uiCamera = uiCamera.AddComponent<Camera>();
			_uiCamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
			_uiCamera.depth = 1000;
			_uiCamera.allowHDR = false;
			_uiCamera.allowMSAA = false;
			_uiCamera.clearFlags = CameraClearFlags.Depth;
			_uiCamera.orthographic = orthographic;

			RootCanvas.worldCamera = _uiCamera;
		}

		private void CreateEventSystem()
		{
			GameObject eventSystemGameObject = new GameObject("EventSystem");
			eventSystemGameObject.transform.SetParent(transform, false);

			_uiEvetSystem = eventSystemGameObject.AddComponent<EventSystem>();
			_inputModule = eventSystemGameObject.AddComponent<StandaloneInputModule>();
		}

		private void AddLayers()
		{
			//RectTransform layeredNodeHide = AddLayer("");
			//layeredNodeHide.gameObject.SetActive(false);

			if (_uiSettings != null)
			{
				if (_uiSettings.layers != null)
				{
					for (int i = 0; i < _uiSettings.layers.Count; i++)
					{
						AddLayer(_uiSettings.layers[i]);
					}
				}
			}
		}

		private void OnDestroy()
		{
			if (_uiSettings != null)
			{
				if (_uiSettings.layers != null)
				{
					for (int i = _uiSettings.layers.Count - 1; i >= 0; i--)
					{
						RemoveLayer(_uiSettings.layers[i]);
					}
				}
			}
		}

		public int GetResoulutionX()
		{
			return (int)RootCanvasScaler.referenceResolution.x;
		}

		public int GetResoulutionY()
		{
			return (int)RootCanvasScaler.referenceResolution.y;
		}

		public RectTransform AddLayer(UILayer layer)
		{
			if (Instance != null)
			{
				RectTransform layeredNode = CreateLayer(layer);

				layeredNode.SetParent(RootCanvasTransform, false);

				if (!Instance._layeredNodes.ContainsKey(layer.name))
				{
					Instance._layeredNodes.Add(layer.name, layeredNode);
				}
				return layeredNode;
			}
			return null;
		}

		private RectTransform CreateLayer(UILayer layer)
		{
			GameObject layerGo = new GameObject(layer.name);

			RectTransform layerTransform = layerGo.AddComponent<RectTransform>();
			if (layer.subCanvas)
			{
				layerGo.AddComponent<Canvas>();
				layerGo.AddComponent<GraphicRaycaster>();
			}
			layerGo.layer = LayerMask.NameToLayer("UI");

			layerTransform.anchorMin = Vector2.zero;
			layerTransform.anchorMax = Vector2.one;
			layerTransform.sizeDelta = Vector2.zero;

			return layerTransform;
		}

		public void RemoveLayer(UILayer layer)
		{
			if (Instance != null)
			{
				RectTransform layeredNode;

				if (Instance._layeredNodes.TryGetValue(layer.name, out layeredNode))
				{
					if (layeredNode != null)
					{
						Destroy(layeredNode.gameObject);
					}
				}
			}
		}

		public GameObject GetLayerNode(string layerName)
		{
			if (Instance != null)
			{
				RectTransform layeredNode;

				if (Instance._layeredNodes.TryGetValue(layerName, out layeredNode))
				{
					if (layeredNode != null)
					{
						return layeredNode.gameObject;
					}
				}
			}
			return null;
		}

		public bool AttachToLayer(GameObject uiGameObject, string layerName)
		{
			if (Instance != null)
			{
				RectTransform layeredNode;

				if (Instance._layeredNodes.TryGetValue(layerName, out layeredNode))
				{
					uiGameObject.transform.SetParent(layeredNode, false);

					return true;
				}
			}
			return false;
		}
	}
}