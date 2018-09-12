using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public static class BlueprintEditorPanel
{
	//Private attributes
	private static float _maxZoom = 1.5f;
	private static float _minZoom = 0.5f;
	private static float _zoomSensitivity = 0.01f;
	private static float _panSensitivity = 0.1f;
	private static float _zoom = 1.0f;
	private static Vector2 _pan = new Vector2(0.0f, 0.0f);
	private static Matrix4x4 _originalMatrix;

	//Performs pre-rendering tasks for the blueprint editor panel
	public static void PreRender()
	{
		//Store the original GUI matrix
		_originalMatrix = GUI.matrix;

		//Scale the GUI around the editor panel pivot by the zoom multiplier
		GUIUtility.ScaleAroundPivot(new Vector2(_zoom * 2.0f, _zoom * 2.0f), new Vector2(0.0f, 55.0f));

		//Begin group for rendering the blueprint editor panel
		GUI.BeginGroup(new Rect(0.0f, 55.0f, Screen.width / (_zoom * 2.0f), (Screen.height - 106.0f) / (_zoom * 2.0f)));
	}

	//Performs post-rendering tasks for the blueprint editor panel
	public static void PostRender()
	{
		//End group for rendering the blueprint editor panel
		GUI.EndGroup();

		//Restore the original GUI matrix
		GUI.matrix = _originalMatrix;
	}

	//Renders the blueprint editor panel and updates EditorGUILayout and GUILayout components inside the blueprint editor panel
	public static void Render()
	{
		//Render the grid tile texture
		GUI.DrawTextureWithTexCoords(new Rect(0.0f, 0.0f, Screen.width, Screen.height), BlueprintStyleHelper.GetPanelBackgroundTexture(), new Rect(_pan.x, _pan.y, Screen.width * 0.05f, Screen.height * 0.05f));

		//Render the blueprint
		BlueprintEditor.GetBlueprint().Render();
	}

	//Handles events for the blueprint editor panel
	public static void HandleEvents()
	{
		//If the mouse position is inside the blueprint editor panel
		if (new Rect(0.0f, 55.0f, Screen.width, Screen.height - 106.0f).Contains(Event.current.mousePosition))
		{
			//If the current event type is a scroll wheel event
			if (Event.current.type == EventType.ScrollWheel)
			{
				//Handle scroll wheel events
				HandleScrollWheelEvents();
			}
			//Otherwise, if the current event type is a mouse drag event
			else if (Event.current.type == EventType.MouseDrag)
			{
				//Handle mouse drag events
				HandleMouseDragEvents();
			}
			//Otherwise, if the current event type is a mouse down event
			else if (Event.current.type == EventType.MouseDown)
			{
				//Handle mouse down events
				HandleMouseDownEvents();
			}
			//Otherwise, if the current event type is a mouse up event
			else if(Event.current.type == EventType.MouseUp)
			{
				//Handle mouse up events
				HandleMouseUpEvents();
			}
		}
	}

	//Handles scroll wheel events for the blueprint editor panel
	private static void HandleScrollWheelEvents()
	{
		//Update zoom
		_zoom = Mathf.Clamp(_zoom - Event.current.delta.y * _zoomSensitivity, _minZoom, _maxZoom);

		//Process event
		Event.current.Use();
	}

	//Handles mouse drag events for the blueprint editor panel
	private static void HandleMouseDragEvents()
	{
		//If the mouse drag event is being triggered by the middle mouse button
		if (Event.current.button == 2)
		{
			//Update pan
			_pan += new Vector2(-Event.current.delta.x, Event.current.delta.y) * _panSensitivity * Time.deltaTime;

			//Translate all blueprint nodes in the blueprint by the mouse delta
			BlueprintEditor.GetBlueprint().Translate(Event.current.delta * 0.667f);
		}

		//Process event
		Event.current.Use();
	}

	//Handles mouse down events for the blueprint editor panel
	private static void HandleMouseDownEvents()
	{
		//If the mouse down event is being triggered by the right mouse button
		if (Event.current.button == 1)
		{
			//Create context menu
			GenericMenu contextMenu = new GenericMenu();

			//Add attribute blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Attribute/Generic/Bool"), false, AddBlueprintNode<BlueprintNodeAttributeBool>);
			contextMenu.AddItem(new GUIContent("Attribute/Generic/Float"), false, AddBlueprintNode<BlueprintNodeAttributeFloat>);
			contextMenu.AddItem(new GUIContent("Attribute/Generic/Int"), false, AddBlueprintNode<BlueprintNodeAttributeInt>);
			contextMenu.AddItem(new GUIContent("Attribute/Generic/String"), false, AddBlueprintNode<BlueprintNodeAttributeString>);
			contextMenu.AddItem(new GUIContent("Attribute/Math/Quaternion"),false, AddBlueprintNode<BlueprintNodeAttributeQuaternion>);
			contextMenu.AddItem(new GUIContent("Attribute/Math/Vector2"), false, AddBlueprintNode<BlueprintNodeAttributeVector2>);
			contextMenu.AddItem(new GUIContent("Attribute/Math/Vector3"), false, AddBlueprintNode<BlueprintNodeAttributeVector3>);
			contextMenu.AddItem(new GUIContent("Attribute/Math/Vector4"), false, AddBlueprintNode<BlueprintNodeAttributeVector4>);
			contextMenu.AddItem(new GUIContent("Attribute/Object/GameObject"), false, AddBlueprintNode<BlueprintNodeAttributeGameObject>);
			contextMenu.AddItem(new GUIContent("Attribute/Special/Blueprint"), false, AddBlueprintNode<BlueprintNodeAttributeBlueprint>);

			//Query list of blueprint filepaths
			string[] blueprintFilepaths = BlueprintPathHelper.GetBlueprintFilepaths();

			//For each blueprint filepath
			for (int filepathIndex = 0; filepathIndex < blueprintFilepaths.Length; filepathIndex++)
			{
				//Add custom blueprint node to the context menu
				contextMenu.AddItem(new GUIContent("Blueprint/" + BlueprintPathHelper.GetNameFromPath(blueprintFilepaths[filepathIndex])), false, AddBlueprintNodeBlueprint, blueprintFilepaths[filepathIndex]);
			}

			//Add compare blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Compare/CompareBools"), false, AddBlueprintNode<BlueprintNodeCompareBools>);
			contextMenu.AddItem(new GUIContent("Compare/CompareFloats"), false, AddBlueprintNode<BlueprintNodeCompareFloats>);
			contextMenu.AddItem(new GUIContent("Compare/CompareInts"), false, AddBlueprintNode<BlueprintNodeCompareInts>);
			contextMenu.AddItem(new GUIContent("Compare/CompareStrings"), false, AddBlueprintNode<BlueprintNodeCompareStrings>);

			//Add connection blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Connection/Animation/AnimatorSourceOneToMany"), false, AddBlueprintNodeConnectionOneToMany<BlueprintConnectionAttributeInputAnimator, BlueprintConnectionAttributeOutputAnimator>, "Animator");
			contextMenu.AddItem(new GUIContent("Connection/Audio/AudioSourceOneToMany"), false, AddBlueprintNodeConnectionOneToMany<BlueprintConnectionAttributeInputAudioSource, BlueprintConnectionAttributeOutputAudioSource>, "AudioSource");
			contextMenu.AddItem(new GUIContent("Connection/UI/ButtonOneToMany"), false, AddBlueprintNodeConnectionOneToMany<BlueprintConnectionAttributeInputButton, BlueprintConnectionAttributeOutputButton>, "Button");
			contextMenu.AddItem(new GUIContent("Connection/UI/TextOneToMany"), false, AddBlueprintNodeConnectionOneToMany<BlueprintConnectionAttributeInputText, BlueprintConnectionAttributeOutputText>, "Text");
			contextMenu.AddItem(new GUIContent("Connection/BlueprintOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionBlueprintOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/BoolOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionBoolOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/FloatOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionFloatOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/GameObjectOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionGameObjectOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/IntOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionIntOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/QuaternionOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionQuaternionOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/StringOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionStringOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/TransformOneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionTransformOneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/Vector2OneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionVector2OneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/Vector3OneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionVector3OneToMany>);
			contextMenu.AddItem(new GUIContent("Connection/Vector4OneToMany"), false, AddBlueprintNode<BlueprintNodeConnectionVector4OneToMany>);

			//Add event blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Event/Collision/OnCollisionEnter"), false, AddBlueprintNode<BlueprintNodeEventOnCollisionEnter>);
			contextMenu.AddItem(new GUIContent("Event/Collision/OnCollisionExit"), false, AddBlueprintNode<BlueprintNodeEventOnCollisionExit>);
			contextMenu.AddItem(new GUIContent("Event/Collision/OnCollisionStay"), false, AddBlueprintNode<BlueprintNodeEventOnCollisionStay>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetAnyKey"), false, AddBlueprintNode<BlueprintNodeEventGetAnyKey>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetAnyKeyDown"), false, AddBlueprintNode<BlueprintNodeEventGetAnyKeyDown>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetAxis"), false, AddBlueprintNode<BlueprintNodeEventGetAxis>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetButton"), false, AddBlueprintNode<BlueprintNodeEventGetButton>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetButtonDown"), false, AddBlueprintNode<BlueprintNodeEventGetButtonDown>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetButtonUp"), false, AddBlueprintNode<BlueprintNodeEventGetButtonUp>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetKey"), false, AddBlueprintNode<BlueprintNodeEventGetKey>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetKeyDown"), false, AddBlueprintNode<BlueprintNodeEventGetKeyDown>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetKeyUp"), false, AddBlueprintNode<BlueprintNodeEventGetKeyUp>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButton"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButton>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButton(GameObject : object)"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButton_GameObject>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButtonDown"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButtonDown>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButtonDown(GameObject : object)"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButtonDown_GameObject>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButtonUp"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButtonUp>);
			contextMenu.AddItem(new GUIContent("Event/Input/GetMouseButtonUp(GameObject : object)"), false, AddBlueprintNode<BlueprintNodeEventGetMouseButtonUp_GameObject>);
			contextMenu.AddItem(new GUIContent("Event/UI/OnButtonClick"), false, AddBlueprintNode<BlueprintNodeEventOnButtonClick>);
			contextMenu.AddItem(new GUIContent("Event/Start"), false, AddBlueprintNode<BlueprintNodeEventStart>);
			contextMenu.AddItem(new GUIContent("Event/Update"), false, AddBlueprintNode<BlueprintNodeEventUpdate>);

			//Add function blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Functions/Audio/Play"), false, AddBlueprintNode<BlueprintNodeFunctionPlay_AudioSource>);
			contextMenu.AddItem(new GUIContent("Functions/Debug/Log"), false, AddBlueprintNode<BlueprintNodeFunctionLog>);
			contextMenu.AddItem(new GUIContent("Functions/Debug/LogError"), false, AddBlueprintNode<BlueprintNodeFunctionLogError>);
			contextMenu.AddItem(new GUIContent("Functions/Debug/LogWarning"), false, AddBlueprintNode<BlueprintNodeFunctionLogWarning>);
			contextMenu.AddItem(new GUIContent("Functions/GameObject/Instantiate"), false, AddBlueprintNode<BlueprintNodeFunctionInstantiate_GameObject>);
			contextMenu.AddItem(new GUIContent("Functions/Scene/LoadScene"), false, AddBlueprintNode<BlueprintNodeFunctionLoadScene_String>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/LookAt(Transform : target)"), false, AddBlueprintNode<BlueprintNodeFunctionLookAt_Transform>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/LookAt(Transform : transform, Transform target)"), false, AddBlueprintNode<BlueprintNodeFunctionLookAt_TransformTransform>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/LookAt(Transform : transform, Vector3 location)"), false, AddBlueprintNode<BlueprintNodeFunctionLookAt_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/LookAt(Vector3 : location)"), false, AddBlueprintNode<BlueprintNodeFunctionLookAt_Vector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Rotate(Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionRotate_Quaternion>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Rotate(Transform : transform, Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionRotate_TransformQuaternion>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Rotate(Transform : transform, Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionRotate_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Rotate(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionRotate_Vector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Scale(Transform : transform, Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionScale_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Scale(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeFunctionScale_Vector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Translate(Transform : transform, Vector3 : translation)"), false, AddBlueprintNode<BlueprintNodeFunctionTranslate_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Functions/Transform/Translate(Vector3 : translation)"), false, AddBlueprintNode<BlueprintNodeFunctionTranslate_Vector3>);

			//Add get blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Get/GameObject/GetGameObject"), false, AddBlueprintNode<BlueprintNodeGetGetGameObject>);
			contextMenu.AddItem(new GUIContent("Get/Input/GetAxis"), false, AddBlueprintNode<BlueprintNodeGetGetAxis>);
			contextMenu.AddItem(new GUIContent("Get/Input/GetMousePosition"), false, AddBlueprintNode<BlueprintNodeGetGetMousePosition>);
			contextMenu.AddItem(new GUIContent("Get/Input/GetMouseScrollDelta"), false, AddBlueprintNode<BlueprintNodeGetGetMouseScrollDelta>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetX(Quaternion : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetX_Quaternion>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetY(Quaternion : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetY_Quaternion>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetZ(Quaternion : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetZ_Quaternion>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetW(Quaternion : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetW_Quaternion>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetX(Vector2 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetX_Vector2>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetY(Vector2 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetY_Vector2>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetX(Vector3 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetX_Vector3>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetY(Vector3 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetY_Vector3>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetZ(Vector3 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetZ_Vector3>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetX(Vector4 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetX_Vector4>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetY(Vector4 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetY_Vector4>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetZ(Vector4 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetZ_Vector4>);
			contextMenu.AddItem(new GUIContent("Get/Math/GetW(Vector4 : vector)"), false, AddBlueprintNode<BlueprintNodeGetGetW_Vector4>);
			contextMenu.AddItem(new GUIContent("Get/Special/GetBlueprint"), false, AddBlueprintNode<BlueprintNodeGetGetBlueprint>);
			contextMenu.AddItem(new GUIContent("Get/Special/GetBlueprintVariable"), false, AddBlueprintNode<BlueprintNodeGetGetBlueprintVariable>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetForward"), false, AddBlueprintNode<BlueprintNodeGetGetForward>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetForward(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetForward_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalPosition"), false, AddBlueprintNode<BlueprintNodeGetGetLocalPosition>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalPosition(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetLocalPosition_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalRotation"), false, AddBlueprintNode<BlueprintNodeGetGetLocalRotation>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalRotation(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetLocalRotation_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalScale"), false, AddBlueprintNode<BlueprintNodeGetGetLocalScale>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetLocalScale(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetLocalScale_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetPosition"), false, AddBlueprintNode<BlueprintNodeGetGetPosition>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetPosition(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetPosition_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetRight"), false, AddBlueprintNode<BlueprintNodeGetGetRight>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetRight(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetRight_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetRotation"), false, AddBlueprintNode<BlueprintNodeGetGetRotation>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetRotation(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetRotation_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetScale"), false, AddBlueprintNode<BlueprintNodeGetGetScale>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetScale(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetScale_Transform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetTransform"), false, AddBlueprintNode<BlueprintNodeGetGetTransform>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetTransform(GameObject : object)"), false, AddBlueprintNode<BlueprintNodeGetGetTransform_GameObject>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetUp"), false, AddBlueprintNode<BlueprintNodeGetGetUp>);
			contextMenu.AddItem(new GUIContent("Get/Transform/GetUp(Transform : transform)"), false, AddBlueprintNode<BlueprintNodeGetGetUp_Transform>);

			//Add math blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Math/AddFloat"), false, AddBlueprintNode<BlueprintNodeMathAddFloat>);
			contextMenu.AddItem(new GUIContent("Math/AddInt"), false, AddBlueprintNode<BlueprintNodeMathAddInt>);
			contextMenu.AddItem(new GUIContent("Math/AddVector2"), false, AddBlueprintNode<BlueprintNodeMathAddVector2>);
			contextMenu.AddItem(new GUIContent("Math/AddVector3"), false, AddBlueprintNode<BlueprintNodeMathAddVector3>);
			contextMenu.AddItem(new GUIContent("Math/AddVector4"), false, AddBlueprintNode<BlueprintNodeMathAddVector4>);
			contextMenu.AddItem(new GUIContent("Math/Acos"), false, AddBlueprintNode<BlueprintNodeMathAcos>);
			contextMenu.AddItem(new GUIContent("Math/Asin"), false, AddBlueprintNode<BlueprintNodeMathAsin>);
			contextMenu.AddItem(new GUIContent("Math/Atan"), false, AddBlueprintNode<BlueprintNodeMathAtan>);
			contextMenu.AddItem(new GUIContent("Math/Atan2"), false, AddBlueprintNode<BlueprintNodeMathAtan2>);
			contextMenu.AddItem(new GUIContent("Math/Cos"), false, AddBlueprintNode<BlueprintNodeMathCos>);
			contextMenu.AddItem(new GUIContent("Math/CrossVector3"), false, AddBlueprintNode<BlueprintNodeMathCrossVector3>);
			contextMenu.AddItem(new GUIContent("Math/DivideFloat"), false, AddBlueprintNode<BlueprintNodeMathDivideFloat>);
			contextMenu.AddItem(new GUIContent("Math/DivideInt"), false, AddBlueprintNode<BlueprintNodeMathDivideInt>);
			contextMenu.AddItem(new GUIContent("Math/DivideVector2"), false, AddBlueprintNode<BlueprintNodeMathDivideVector2>);
			contextMenu.AddItem(new GUIContent("Math/DivideVector3"), false, AddBlueprintNode<BlueprintNodeMathDivideVector3>);
			contextMenu.AddItem(new GUIContent("Math/DivideVector4"), false, AddBlueprintNode<BlueprintNodeMathDivideVector4>);
			contextMenu.AddItem(new GUIContent("Math/DotVector2"), false, AddBlueprintNode<BlueprintNodeMathDotVector2>);
			contextMenu.AddItem(new GUIContent("Math/DotVector3"), false, AddBlueprintNode<BlueprintNodeMathDotVector3>);
			contextMenu.AddItem(new GUIContent("Math/DotVector4"), false, AddBlueprintNode<BlueprintNodeMathDotVector4>);
			contextMenu.AddItem(new GUIContent("Math/LerpFloat"), false, AddBlueprintNode<BlueprintNodeMathLerpFloat>);
			contextMenu.AddItem(new GUIContent("Math/LerpQuaternion"), false, AddBlueprintNode<BlueprintNodeMathLerpQuaternion>);
			contextMenu.AddItem(new GUIContent("Math/LerpVector2"), false, AddBlueprintNode<BlueprintNodeMathLerpVector2>);
			contextMenu.AddItem(new GUIContent("Math/LerpVector3"), false, AddBlueprintNode<BlueprintNodeMathLerpVector3>);
			contextMenu.AddItem(new GUIContent("Math/LerpVector4"), false, AddBlueprintNode<BlueprintNodeMathLerpVector4>);
			contextMenu.AddItem(new GUIContent("Math/MagnitudeVector3"), false, AddBlueprintNode<BlueprintNodeMathMagnitudeVector3>);
			contextMenu.AddItem(new GUIContent("Math/MagnitudeVector4"), false, AddBlueprintNode<BlueprintNodeMathMagnitudeVector4>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyFloat"), false, AddBlueprintNode<BlueprintNodeMathMultiplyFloat>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyInt"), false, AddBlueprintNode<BlueprintNodeMathMultiplyInt>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyVector2"), false, AddBlueprintNode<BlueprintNodeMathMultiplyVector2>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyVector3"), false, AddBlueprintNode<BlueprintNodeMathMultiplyVector3>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyVector4"), false, AddBlueprintNode<BlueprintNodeMathMultiplyVector4>);
			contextMenu.AddItem(new GUIContent("Math/MultiplyQuaternion"), false, AddBlueprintNode<BlueprintNodeMathMultiplyQuaternion>);
			contextMenu.AddItem(new GUIContent("Math/NormalizeVector3"), false, AddBlueprintNode<BlueprintNodeMathNormalizeVector3>);
			contextMenu.AddItem(new GUIContent("Math/NormalizeVector4"), false, AddBlueprintNode<BlueprintNodeMathNormalizeVector4>);
			contextMenu.AddItem(new GUIContent("Math/Sin"), false, AddBlueprintNode<BlueprintNodeMathSin>);
			contextMenu.AddItem(new GUIContent("Math/SlerpQuaternion"), false, AddBlueprintNode<BlueprintNodeMathSlerpQuaternion>);
			contextMenu.AddItem(new GUIContent("Math/SlerpVector3"), false, AddBlueprintNode<BlueprintNodeMathSlerpVector3>);
			contextMenu.AddItem(new GUIContent("Math/SubtractFloat"), false, AddBlueprintNode<BlueprintNodeMathSubtractFloat>);
			contextMenu.AddItem(new GUIContent("Math/SubtractInt"), false, AddBlueprintNode<BlueprintNodeMathSubtractInt>);
			contextMenu.AddItem(new GUIContent("Math/SubtractVector2"), false, AddBlueprintNode<BlueprintNodeMathSubtractVector2>);
			contextMenu.AddItem(new GUIContent("Math/SubtractVector3"), false, AddBlueprintNode<BlueprintNodeMathSubtractVector3>);
			contextMenu.AddItem(new GUIContent("Math/SubtractVector4"), false, AddBlueprintNode<BlueprintNodeMathSubtractVector4>);
			contextMenu.AddItem(new GUIContent("Math/Tan"), false, AddBlueprintNode<BlueprintNodeMathTan>);
			contextMenu.AddItem(new GUIContent("Math/Vector2xFloat"), false, AddBlueprintNode<BlueprintNodeMathVector2xFloat>);
			contextMenu.AddItem(new GUIContent("Math/Vector3xFloat"), false, AddBlueprintNode<BlueprintNodeMathVector3xFloat>);
			contextMenu.AddItem(new GUIContent("Math/Vector4xFloat"), false, AddBlueprintNode<BlueprintNodeMathVector4xFloat>);

			//Add set blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Set/Animation/SetBool"), false, AddBlueprintNode<BlueprintNodeSetSetBool_Animator>);
			contextMenu.AddItem(new GUIContent("Set/Animation/SetFloat"), false, AddBlueprintNode<BlueprintNodeSetSetFloat_Animator>);
			contextMenu.AddItem(new GUIContent("Set/Animation/SetInt"), false, AddBlueprintNode<BlueprintNodeSetSetInt_Animator>);
			contextMenu.AddItem(new GUIContent("Set/Audio/SetAudioClip"), false, AddBlueprintNode<BlueprintNodeSetSetAudioClip>);
			contextMenu.AddItem(new GUIContent("Set/Generic/SetBool"), false, AddBlueprintNode<BlueprintNodeSetSetBool>);
			contextMenu.AddItem(new GUIContent("Set/Generic/SetFloat"), false, AddBlueprintNode<BlueprintNodeSetSetFloat>);
			contextMenu.AddItem(new GUIContent("Set/Generic/SetInt"), false, AddBlueprintNode<BlueprintNodeSetSetInt>);
			contextMenu.AddItem(new GUIContent("Set/Generic/SetString"), false, AddBlueprintNode<BlueprintNodeSetSetString>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetQuaternion(Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetQuaternion_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetQuaternion(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetQuaternion_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetVector2"), false, AddBlueprintNode<BlueprintNodeSetSetVector2>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetVector3"), false, AddBlueprintNode<BlueprintNodeSetSetVector3>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetVector4"), false, AddBlueprintNode<BlueprintNodeSetSetVector4>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetX"), false, AddBlueprintNode<BlueprintNodeSetSetX_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetY"), false, AddBlueprintNode<BlueprintNodeSetSetY_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetZ"), false, AddBlueprintNode<BlueprintNodeSetSetZ_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetW"), false, AddBlueprintNode<BlueprintNodeSetSetW_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetX"), false, AddBlueprintNode<BlueprintNodeSetSetX_Vector2>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetY"), false, AddBlueprintNode<BlueprintNodeSetSetY_Vector2>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetX"), false, AddBlueprintNode<BlueprintNodeSetSetX_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetY"), false, AddBlueprintNode<BlueprintNodeSetSetY_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetZ"), false, AddBlueprintNode<BlueprintNodeSetSetZ_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetX"), false, AddBlueprintNode<BlueprintNodeSetSetX_Vector4>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetY"), false, AddBlueprintNode<BlueprintNodeSetSetY_Vector4>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetZ"), false, AddBlueprintNode<BlueprintNodeSetSetZ_Vector4>);
			contextMenu.AddItem(new GUIContent("Set/Math/SetW"), false, AddBlueprintNode<BlueprintNodeSetSetW_Vector4>);

			contextMenu.AddItem(new GUIContent("Set/Object/SetGameObject"), false, AddBlueprintNode<BlueprintNodeSetSetGameObject>);
			contextMenu.AddItem(new GUIContent("Set/Special/SetBlueprint"), false, AddBlueprintNode<BlueprintNodeSetSetBlueprint>);
			contextMenu.AddItem(new GUIContent("Set/Special/SetBlueprintVariable"), false, AddBlueprintNode<BlueprintNodeSetSetBlueprintVariable>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalPosition(Transform : transform, Vector3 : position)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalPosition_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalPosition(Vector3 : position)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalPosition_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalRotation(Transform : transform, Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalRotation_TransformQuaternion>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalRotation(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalRotation_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalRotation(Transform : transform, Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalRotation_TransformQuaternion>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalRotation(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalRotation_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalScale(Transform : transform, Vector3 : scale)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalScale_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetLocalScale(Vector3 : scale)"), false, AddBlueprintNode<BlueprintNodeSetSetLocalScale_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetPosition(Transform : transform, Vector3 : position)"), false, AddBlueprintNode<BlueprintNodeSetSetPosition_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetPosition(Vector3 : position)"), false, AddBlueprintNode<BlueprintNodeSetSetPosition_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetRotation(Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetRotation_Quaternion>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetRotation(Transform : transform, Quaternion : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetRotation_TransformQuaternion>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetRotation(Transform : transform, Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetRotation_TransformVector3>);
			contextMenu.AddItem(new GUIContent("Set/Transform/SetRotation(Vector3 : rotation)"), false, AddBlueprintNode<BlueprintNodeSetSetRotation_Vector3>);
			contextMenu.AddItem(new GUIContent("Set/UI/SetText"), false, AddBlueprintNode<BlueprintNodeSetSetText>);

			//Add time blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Time/DeltaTime"), false, AddBlueprintNode<BlueprintNodeTimeDeltaTime>);
			contextMenu.AddItem(new GUIContent("Time/RunTime"), false, AddBlueprintNode<BlueprintNodeTimeRunTime>);

			//Add variable blueprint nodes to the context menu
			contextMenu.AddItem(new GUIContent("Variable/Assets/Audio/AudioClip"), false, AddBlueprintNodeVariableAsset<AudioClip, BlueprintConnectionAttributeInputAudioClip, BlueprintConnectionAttributeOutputAudioClip>, "AudioClip");
			contextMenu.AddItem(new GUIContent("Variable/Components/Animation/Animator"), false, AddBlueprintNodeVariableComponent<Animator, BlueprintConnectionAttributeInputAnimator, BlueprintConnectionAttributeOutputAnimator>, "Animator");
			contextMenu.AddItem(new GUIContent("Variable/Components/Audio/AudioSource"), false, AddBlueprintNodeVariableComponent<AudioSource, BlueprintConnectionAttributeInputAudioSource, BlueprintConnectionAttributeOutputAudioSource>, "AudioSource");
			contextMenu.AddItem(new GUIContent("Variable/Components/UI/Button"), false, AddBlueprintNodeVariableComponent<Button, BlueprintConnectionAttributeInputButton, BlueprintConnectionAttributeOutputButton>, "Button");
			contextMenu.AddItem(new GUIContent("Variable/Components/UI/Text"), false, AddBlueprintNodeVariableComponent<Text, BlueprintConnectionAttributeInputText, BlueprintConnectionAttributeOutputText>, "Text");
			contextMenu.AddItem(new GUIContent("Variable/Bool"), false, AddBlueprintNode<BlueprintNodeVariableBool>);
			contextMenu.AddItem(new GUIContent("Variable/Float"), false, AddBlueprintNode<BlueprintNodeVariableFloat>);
			contextMenu.AddItem(new GUIContent("Variable/GameObject"), false, AddBlueprintNode<BlueprintNodeVariableGameObject>);
			contextMenu.AddItem(new GUIContent("Variable/Int"), false, AddBlueprintNode<BlueprintNodeVariableInt>);
			contextMenu.AddItem(new GUIContent("Variable/Quaternion"), false, AddBlueprintNode<BlueprintNodeVariableQuaternion>);
			contextMenu.AddItem(new GUIContent("Variable/String"), false, AddBlueprintNode<BlueprintNodeVariableString>);
			contextMenu.AddItem(new GUIContent("Variable/Vector2"), false, AddBlueprintNode<BlueprintNodeVariableVector2>);
			contextMenu.AddItem(new GUIContent("Variable/Vector3"), false, AddBlueprintNode<BlueprintNodeVariableVector3>);
			contextMenu.AddItem(new GUIContent("Variable/Vector4"), false, AddBlueprintNode<BlueprintNodeVariableVector4>);

			//Show the context menu
			contextMenu.ShowAsContext();
		}

		//Process event
		Event.current.Use();
	}

	//Handles mouse up events for the blueprint editor panel
	private static void HandleMouseUpEvents()
	{
		//If the mouse up event is being triggered by the left mouse button
		if(Event.current.button == 0)
		{
			//Cancel connection
			BlueprintEditorManager.connection = null;
		}

		//Process event
		Event.current.Use();
	}

	//Shows any application blueprint node/connection context menus
	public static void ProcessContextMenus()
	{
		//For each blueprint node
		for(int blueprintNodeIndex = 0; blueprintNodeIndex < BlueprintEditorManager.blueprint.GetBlueprintNodeCount(); blueprintNodeIndex++)
		{
			//If the blueprint nodes context menu should be shown
			if(BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).ShouldShowContextMenu())
			{
				//Show the blueprint nodes context menu
				BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).ShowContextMenu();
			}

			//If the blueprint nodes input execution connection is valid
			if(BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).inputExecutionConnection != null)
			{
				//If the blueprint nodes input execution connection context menu should be shown
				if(BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).inputExecutionConnection.ShouldShowContextMenu())
				{
					//Show the blueprint nodes input execution connection context menu
					BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).inputExecutionConnection.ShowContextMenu();
				}
			}

			//If the blueprint nodes output execution connection is valid
			if (BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).outputExecutionConnection != null)
			{
				//If the blueprint nodes output execution connection context menu should be shown
				if (BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).outputExecutionConnection.ShouldShowContextMenu())
				{
					//Show the blueprint nodes output execution connection context menu
					BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).outputExecutionConnection.ShowContextMenu();
				}
			}

			//For each of the blueprint nodes connections
			for(int blueprintConnectionIndex = 0; blueprintConnectionIndex < BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).connections.Count; blueprintConnectionIndex++)
			{
				//If the blueprint nodes connection context menu should be shown
				if (BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).connections[blueprintConnectionIndex].ShouldShowContextMenu())
				{
					//Show the blueprint nodes connection context menu
					BlueprintEditorManager.blueprint.GetBlueprintNodeAt(blueprintNodeIndex).connections[blueprintConnectionIndex].ShowContextMenu();
				}
			}
		}
	}

	//Adds a blueprint node of the specified type to the blueprint
	private static void AddBlueprintNode<T>() where T : BlueprintNode, new()
	{
		//Add a blueprint node of the specified type to the blueprint
		BlueprintEditor.GetBlueprint().AddBlueprintNode(new T(), _pan);
	}

	//Adds a blueprint node connection one to many of the specified type to the blueprint
	private static void AddBlueprintNodeConnectionOneToMany<T1, T2>(object userData) where T1 : BlueprintConnectionAttribute, new() where T2 : BlueprintConnectionAttribute, new()
	{
		//Add a blueprint node connection one to many of the specified type to the blueprint
		BlueprintEditor.GetBlueprint().AddBlueprintNodeConnectionOneToMany<T1, T2>((string)userData, _pan);
	}

	//Adds a blueprint node variable asset of the specified asset type to the blueprint
	private static void AddBlueprintNodeVariableAsset<T1, T2, T3>(object userData) where T1 : Object where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
	{
		//Add a blueprint node variable asset of the specified asset type to the blueprint
		BlueprintEditor.GetBlueprint().AddBlueprintNodeVariableAsset<T1, T2, T3>((string)userData, _pan);
	}

	//Adds a blueprint node variable component of the specified component type to the blueprint
	private static void AddBlueprintNodeVariableComponent<T1, T2, T3>(object userData) where T1 : Component where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
	{
		//Add a blueprint node variable component of the specified component type to the blueprint
		BlueprintEditor.GetBlueprint().AddBlueprintNodeVariableComponent<T1, T2, T3>((string)userData, _pan);
	}

	//Adds a blueprint node blueprint instance to the blueprint
	private static void AddBlueprintNodeBlueprint(object userData)
	{
		//Add a blueprint node blueprint instance to the blueprint
		BlueprintEditor.GetBlueprint().AddCustomBlueprintNode((string)userData, _pan);
	}
}