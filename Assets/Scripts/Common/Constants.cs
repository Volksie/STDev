using System;
using System.Collections.Generic;

public static class Constants
{
	public const float Ft2M = 0.3048f;
	public const float M2Ft = 3.28083f;
	public const float In2M = 0.0254f;
	public const float M2In = 39.37f;

	public static string MsgModelClicked = "OnModelClicked";

    // State group index
    public static int cChoosePorchWallLayout = 0;
    public static int cAdjustPorchWallDimensionsUsingSliders = 1;
    public static int cWallSections = 2;
	public static int cSideWallLength = 3;
	public static int cEditingHorizontalRow = 4;
	public static int cMoveHorizontalRow = 5;
	public static int cEditingVericalStud = 6;
	public static int cMoveVerticalStud = 7;
    public static int cEditingDoor = 8;
    public static int cWallWarningDialog = 9;
    public static int cDoorsPropertiesPanel = 10;
    public static int cOpenTheDoorDialog = 11;

	// Model tags
	public static string ModelPartTag_Affordance = "affordance";
	public static string ModelPartTag_AffordanceWidth = "width";
	public static string ModelPartTag_AffordanceDepth = "depth";
	public static string ModelPartTag_AffordanceMove = "trans";
	public static string ModelPartTag_AffordanceAdd = "connect";
	public static string ModelPartTag_ScreenTight = "ScreenTight";
	public static string ModelPartTag_FastTrack = "FastTrack";
	public static string ModelPartTag_MiniTrack = "MiniTrack";
	public static string ModelTagName = "Model";

	public static string PorchMainIconName = "wallproperties/WallProperties-Main";
	public static string PorchSide1IconName = "wallproperties/WallProperties-SideL";
	public static string PorchSide2IconName = "wallproperties/WallProperties-SideR";

	public const float PorchSizeMin = 1.829f;
	public const float PorchSizeMax = 9.144f;
	public const float PorchWidthMin = PorchSizeMin;
	public const float PorchWidthMax = PorchSizeMax;
	public const float PorchDepthMin = PorchSizeMin;
	public const float PorchDepthMax = PorchSizeMax;
	public const float PorchHeightMin = PorchSizeMin;
	public const float PorchHeightMax = 3.658f;

	public const float PorchRoofHeight = 0.1f;
	public const float PorchFloorHeight = 0.1f;
	public const float PorchCornerOffset = 0.038f;
	public const float PorchVCornerSize = 0.089f;
	public const float PorchHCornerWidth = PorchVCornerSize;
	public const float PorchHCornerHeight = 0.038f;
	public const float PorchArrowOffset = 0.2f;
	public const float PorchWallLengthOffset = 2 * (Constants.PorchCornerOffset + Constants.PorchVCornerSize);
	public const float PorchWallHeightOffset = 2 * Constants.PorchHCornerHeight + Constants.PorchFloorHeight + Constants.PorchRoofHeight;
	public const float PorchWallStudOffset = 0;

    public const float cDoorSize36 = 36.0f;
    public const float cDoorSize32 = 32.0f;
    public const float cDoorSize30 = 30.0f;

	public const int MinVerticalStuds = 0;
    public const int MaxVerticalStuds = 15;
    public const int MinHorizontalRows = 0;
    public const int MaxHorizontalRows = 3;

    public const float MinSideWallLength = 5f;
    public const float MaxSideWallLength = 10f;

	public const float PorchWallIconOffset = 0.05f;

    // Button names
    public const string cWallDimensionWidthSlider = "WallDimension_Width_Text";
    public const string cWallDimensionDepthSlider = "WallDimension_Depth_Text";
    public const string cWallDimensionHeightSlider = "WallDimension_Height_Text";
    public const string cMenuPreConfig = "MenuPreConfig";
    public const string cWallDimensionDone = "WallDimension_PropertiesDone";
    public const string cMenuButton = "MenuButton";
    public const string сBackButton = "ibtn_Back";

    public const string cHorizontalFramesControl = "HorizontalFrames";
    public const string cVerticalFramesControl = "VerticalFrames";
    public const string cMainWallPropertiesDoneControl = "Frames_PropertiesDone";

    public const string cSideWallLengthContol = "SideWallLength";
    public const string cSideWallLengthDoneContol = "SideWallLength_PropertiesDone";
    
	public const string cMainWall = "WallProperties-Main";
	public const string cSidelLeftWall = "WallProperties-SideL";
	public const string cSidelRightWall = "WallProperties-SideR";
	
	public const string cActionControl = "ibtn_action";
	public const string cEditControl = "ibtn_edit";
	public const string cCloneControl = "ibtn_clone";
	public const string cDeleteControl = "ibtn_delete";
	public const string cMoveHorizontalRowDoneControl = "MoveHorizontalRowDone";
	public const string cRowHeightControl = "MoveHorizontalRow";
	
	public const string cMoveVerticalStudDoneControl = "MoveVerticalStudDone";
	public const string cStudPositionControl = "MoveVerticalStud";

    public const string cScreenTightControl = "ibtn_ST";
    public const string cFastTrackControl = "ibtn_FT";
    public const string cMiniTrackControl = "ibtn_MT";
    public const string сBare = "ibtn_Bare";

    public const string cDoorSizeControl = "DoorSize";
    public const string cDoorSizeDoneControl = "DoorSizeDone";

    public const string cWarningDialogContinue = "ibtn_warning_continue";
    public const string cWarningDialogCancel = "ibtn_warning_cancel";

    public const string cOpenTheDoorDoneControl = "OpenTheDoorDone";
	
    // Animation name

}

