using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BillOfMaterials 
{
	private Dictionary<string,Part> PartList = new Dictionary<string, Part> 
	{
		{"BASE18", 			new Part("BASE18",			"1.5\" W x 8' L Base (Black)",	3.66f)},
		{"BASE38", 			new Part("BASE38",			"3.5\" W x 8' L Base (Black)",	5.98f)},
		{"WCAP18", 			new Part("WCAP18",			"1.5\" W x 8' L Cap (White)",	3.66f)},
		{"WCAP38", 			new Part("WCAP38",			"3.5\" W x 8' L Cap (White)",	5.98f)},
		{"BCAP18", 			new Part("BCAP18",			"1.5\" W x 8' L Cap  (Beige)",	3.66f)},
		{"BCAP38", 			new Part("BCAP38",			"3.5\" W x 8' L Cap  (Beige)",	5.98f)},
		{"GCAP18", 			new Part("GCAP18",			"1.5\" W x 8' L Cap  (Gray)",	3.75f)},
		{"GCAP38", 			new Part("GCAP38",			"3.5\" W x 8' L Cap (Gray)",	5.98f)},
		{"BRCAP18", 		new Part("BRCAP18",			"1.5\" W x 8' L Cap (Brown)",	3.66f)},
		{"BRCAP38", 		new Part("BRCAP38",			"3.5\" W x 8' L Cap (Brown)",	5.98f)},
		{"FBS36", 			new Part("FBS36",			"Fiberglass Screen (36\" width)  UNIT = 1 linear foot",	0.45f)},
		{"FBS48", 			new Part("FBS48",			"Fiberglass Screen (48\" width) UNIT = 1 linear foot",	0.55f)},
		{"FBS60", 			new Part("FBS60",			"Fiberglass Screen (60\" width) UNIT = 1 linear foot",	0.93f)},
		{"BULKSPLINE", 		new Part("BULKSPLINE",		"0.175\" Spline UNIT = 1 linear foot",	0.04f)},
		{"WS1", 			new Part("WS1",				"1\" Wood Screws.  UNIT = 1 screw",	0.19f)},
		{"MT8W", 			new Part("MT8W",			"3/4\" x 3/4\" channel, 8' L Base (white)",	9.87f)},
		{"MT8B", 			new Part("MT8B",			"3/4\" x 3/4\" channel, 8' L Base (bronze)",	9.87f)},
		{"MTSPLINE", 		new Part("MTSPLINE",		"0.175\" Spline UNIT = 1 linear foot",	0.06f)},
		{"MTCLIPSCREW100", 	new Part("MTCLIPSCREW100",	"Screw Type Clip UNIT = 1",	0.28f)},
		{"FT128W", 			new Part("FT128W",			"1\" x 2\" channel, 8' L Base (white)",	19.16f)},
		{"FT128B", 			new Part("FT128B",			"1\" x 2\" channel, 8' L Base (bronze)",	19.16f)},
		{"FLATSPLINE300", 	new Part("FLATSPLINE300",	"0.175\" Spline UNIT = 1 linear foot",	0.06f)},
		{"FTCLIP", 			new Part("FTCLIP",			"Fast Track Clips UNIT = 1",	0.56f)},
		{"FTSCREW", 		new Part("FTSCREW",			"#10 by 1 and 1/2 inch stainless steel screws UNIT =1",	0.11f)},
		{"WAC30HD", 		new Part("WAC30HD",			"Waccamaw 30\"",	59.00f)},
		{"WAC32HD", 		new Part("WAC32HD",			"Waccamaw 32\"",	59.00f)},
		{"WAC36HD", 		new Part("WAC36HD",			"Waccamaw 36\"",	59.00f)},
		{"5BAR32HD", 		new Part("5BAR32HD",		"5Bar 32\"",	74.87f)},
		{"5BAR36HD", 		new Part("5BAR36HD",		"5Bar 36\"",	74.87f)},
		{"CAR32HD", 		new Part("CAR32HD",			"Carolina 32\"",	67.00f)},
		{"CAR36HD", 		new Part("CAR36HD",			"Carolina 36\"",	67.00f)},
		{"BH32HD", 			new Part("BH32HD",			"Bell Harbour 32\"",	99.00f)},
		{"BH36HD", 			new Part("BH36HD",			"Bell Harbour 36\"",	99.00f)},
		{"LAF32", 			new Part("LAF32",			"Lafayette 32\"",	64.00f)},
		{"LAF36", 			new Part("LAF36",			"Lafayette 36\"", 	64.00f)},
		{"WED32", 			new Part("WED32",			"Wedgefield 32\"",	98.00f)},
		{"WED36", 			new Part("WED36",			"Wedgefield 36\"",	98.00f)},
		{"SAN32", 			new Part("SAN32",			"Santee 32\"",	79.00f)},
		{"SAN36", 			new Part("SAN36",			"Santee 36\"",	79.00f)},
		{"TBAR32", 			new Part("TBAR32",			"T-BAR 32\"",	38.00f)},
		{"TBAR36", 			new Part("TBAR36",			"T-BAR 36\"",	38.00f)},
		{"SPR30", 			new Part("SPR30",			"Springview 30\"",	99.00f)},
		{"SPR32", 			new Part("SPR32",			"Springview 32\"",	99.00f)},
		{"SPR36", 			new Part("SPR36",			"Springview 36\"",	99.00f)},
		{"5BAR32", 			new Part("5BAR32",			"5 Bar 32\"",	74.00f)},
		{"5BAR36", 			new Part("5BAR36",			"5 Bar 36\"",	74.00f)},
		{"WTBAR30", 		new Part("WTBAR30",			"T-Bar Wood 30\"",	19.98f)},
		{"WTBAR32", 		new Part("WTBAR32",			"T-Bar Wood 32\"",	19.98f)},
		{"WTBAR36", 		new Part("WTBAR36",	"T-Bar Wood 36\"",	19.98f)},
		{"WCEN32", 			new Part("WCEN32",	"Century Wood 32\"",	49.88f)},
		{"WCEN36", 			new Part("WCEN36",	"Century Wood 36\"",	49.88f)},
		{"WCOL32", 			new Part("WCOL32",	"Colonial Wood 32\"",	64.00f)},
		{"WCOL36", 			new Part("WCOL36",	"Colonial Wood 36\"",	64.00f)},
		{"WTIM32PT", 		new Part("WTIM32PT",	"Timberline Pressure Treated 32\"",	129.00f)},
		{"WTIM36PT", 		new Part("WTIM36PT",	"Timberline Pressure Treated 36\"",	129.00f)},
		{"WSUM32PT", 		new Part("WSUM32PT",	"Summit Pressure Treated 32\"",	99.00f)},
		{"WSUM36PT", 		new Part("WSUM36PT",	"Summit Pressure Treated 36\"",	99.00f)},
		{"LIB32", 			new Part("LIB32",	"Liberty 32\"",	54.00f)},
		{"LIB36", 			new Part("LIB36",	"Liberty 36\"",	54.00f)},
		{"WKHLIB36PT", 		new Part("WKHLIB36PT",	"Liberty Pressure Treated 36\"",	119.00f)},
		{"WKHLIB32PT", 		new Part("WKHLIB32PT",	"Liberty Pressure Treated 32\"",	119.00f)},
		{"WFRD30", 			new Part("WFRD30",	"Frederick Wood 30\"",	39.97f)},
		{"WFRD32", 			new Part("WFRD32",	"Frederick Wood 32\"",	39.97f)},
		{"WFRD36", 			new Part("WFRD36",	"Frederick Wood 36\"",	39.97f)},
		{"SDHB", 			new Part("SDHB",	"Brass Lock Kit UNIT =1",	14.95f)},
		{"SDHW", 			new Part("SDHW",	"White Lock Kit UNIT =1",	14.95f)},
		{"SDHN", 			new Part("SDHN",	"Nickel Lock Kit UNIT =1",	14.95f)}
	};
	
	private List<BillOfMaterialsItem> STItems;
	private List<BillOfMaterialsItem> NonSTItems;
	private int VerticalStudCount    = 0;
	private int FrontWallHorizontalRowCount   = 0;
	private int LeftWallHorizontalRowCount   = 0;
	private int RightWallHorizontalRowCount   = 0;
	private int FrontWallRowLen = 0;
	private int LeftWallRowLen = 0;
	private int RightWallRowLen = 0;
	private float VerticalStudSum   = 0;
	private float HorizontalRowSum  = 0;
	private float TotalLinearLength = 0;
	private ProductType product;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void CreateBOM()
	{
		STItems = new List<BillOfMaterialsItem>();
		NonSTItems = new List<BillOfMaterialsItem>();
		
		FrontWallHorizontalRowCount = AppRoot.Scene.Porch.GetWall(PorchWallId.Front).HRowsCount;
		FrontWallRowLen = Mathf.RoundToInt(AppRoot.Scene.Porch.GetWall(PorchWallId.Front).Length * Constants.M2In);
		
		
		if (AppRoot.Scene.Porch.Layout == PorchWallLayoutId.TwoWallLeft || AppRoot.Scene.Porch.Layout == PorchWallLayoutId.ThreeWall)
		{
			LeftWallHorizontalRowCount = AppRoot.Scene.Porch.GetWall(PorchWallId.Left).HRowsCount;
			LeftWallRowLen = Mathf.RoundToInt(AppRoot.Scene.Porch.GetWall(PorchWallId.Left).Length * Constants.M2In);
		}
		else
		{
			LeftWallHorizontalRowCount = 0;
			LeftWallRowLen = 0;
		}
		
		if (AppRoot.Scene.Porch.Layout == PorchWallLayoutId.TwoWallRight || AppRoot.Scene.Porch.Layout == PorchWallLayoutId.ThreeWall)
		{
			RightWallHorizontalRowCount = AppRoot.Scene.Porch.GetWall(PorchWallId.Right).HRowsCount;
			RightWallRowLen = Mathf.RoundToInt(AppRoot.Scene.Porch.GetWall(PorchWallId.Right).Length * Constants.M2In);
		}
		else
		{
			RightWallHorizontalRowCount = 0;
			RightWallRowLen = 0;
		}
		
		product = ProductType.ScreenTight;
		SumAllStudsAndRows(out VerticalStudCount, out FrontWallHorizontalRowCount, out LeftWallHorizontalRowCount, out RightWallHorizontalRowCount, out VerticalStudSum, out HorizontalRowSum);
		
		//Get active state of single stud to determine product type
		
		DetermineSTMaterials(product);
		DetermineNonSTMaterials(product);
		
		Debug.Log("Vertical Studs: " + VerticalStudCount.ToString() + ", Vertical Sum: " + VerticalStudSum.ToString());
		Debug.Log("HorizontalRows: " + (FrontWallHorizontalRowCount + LeftWallHorizontalRowCount + RightWallHorizontalRowCount).ToString() + ", Horizontal Sum: " + HorizontalRowSum.ToString());
		
		Debug.Log("SCREEN TIGHT PRODUCTS");
		foreach(BillOfMaterialsItem b in STItems)
		{
			Debug.Log("\"" + b.PartNumber + "\" -- " + b.Description + " -- " + b.Count.ToString() + " -- " + PartList[b.PartNumber].Price.ToString("C") + " ---- " + (PartList[b.PartNumber].Price * b.Count).ToString("C"));
		}
		Debug.Log("NON SCREEN TIGHT PRODUCTS");
		foreach(BillOfMaterialsItem b in NonSTItems)
		{
			Debug.Log("\"" + b.PartNumber + "\", -- " + b.Description + " -- " + b.Count.ToString() + " -- " + PartList[b.PartNumber].Price.ToString("C") + " ---- " + (PartList[b.PartNumber].Price * b.Count).ToString("C"));
		}
	}
	
	void DetermineSTMaterials(ProductType pt)
	{
		int qty;
		
		qty = CalcQuantity(Mathf.RoundToInt(VerticalStudSum + HorizontalRowSum), 96);
		Part p = PartList[(pt == ProductType.FastTrack) ? "FT128W" : (pt == ProductType.MiniTrack) ? "MT8W" : "BASE18"];
		STItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty));
		
		if (pt == ProductType.ScreenTight)
		{
			p = PartList["WCAP18"];
			STItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty));
		}
		
		
	}
	
	void DetermineNonSTMaterials(ProductType pt)
	{
		int qty = 0;

		int studLen = Mathf.RoundToInt(AppRoot.Scene.Porch.GetWall(PorchWallId.Front).Height * Constants.M2In);
		
		Part p;
		
		//Spline
		qty = CalcQuantity(Mathf.RoundToInt(VerticalStudSum + HorizontalRowSum), 3600);
		p = PartList[(pt == ProductType.FastTrack) ? "FLATSPLINE300" : (pt == ProductType.MiniTrack) ? "MTSPLINE" : "BULKSPLINE"];
		NonSTItems.Add (new BillOfMaterialsItem(p.PartNumber, p.Description, qty * 300)); //Spline comes in buckets of 300 ft
		
		//Screws and clips
		switch (pt)
		{
		case ProductType.FastTrack:
			Debug.Log("DETERMINEPARTQUANTITY FTCLIP, studLen = " + studLen.ToString() + ",  frontWallRowLen = " + FrontWallRowLen.ToString() + ", leftWallRowLen = " + LeftWallRowLen.ToString() + ", rightWallRowLen = " + RightWallRowLen.ToString());
			qty = DeterminePartQuantity(studLen, FrontWallRowLen, LeftWallRowLen, RightWallRowLen, 4, 14);
			
			p = PartList["FTCLIP"];
			Debug.Log("Determined Quantity: " + qty.ToString());
			NonSTItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty));
			p = PartList["FTSCREW"];
			NonSTItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty * 4));
			break;
		case ProductType.MiniTrack:
			qty = DeterminePartQuantity(studLen, FrontWallRowLen, LeftWallRowLen, RightWallRowLen, 2, 8);
				
			p = PartList["MTCLIPSCREW100"];
			NonSTItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty));
			break;
		case ProductType.ScreenTight:
			qty = DeterminePartQuantity(studLen, FrontWallRowLen, LeftWallRowLen, RightWallRowLen, 2, 8);
			
			p = PartList["WS1"];
			NonSTItems.Add(new BillOfMaterialsItem(p.PartNumber, p.Description, qty));
			break;
		default:
			break;
		}
	}
	
	int DeterminePartQuantity(int vLen, int fHLen, int lHLen, int rHLen,  int start, int interval)
	{
		int vQty = 0;
		int fHQty = 0;
		int lHQty = 0;
		int rHQty = 0;
		int qty = 0;
		
		//Vertical Studs
		vQty = Mathf.FloorToInt((vLen - start) / interval) + 1;
		vQty *= VerticalStudCount;
		
		//Horizontal Rows
		if (FrontWallHorizontalRowCount > 0)
		{
			fHQty += Mathf.FloorToInt((fHLen - start) / interval) + 1;
			fHQty *= FrontWallHorizontalRowCount;
		}
		if (LeftWallHorizontalRowCount > 0)
		{
			lHQty += Mathf.FloorToInt((lHLen - start) / interval) + 1;
			lHQty *= LeftWallHorizontalRowCount;	
		}
		if (RightWallHorizontalRowCount > 0)
		{
			rHQty += Mathf.FloorToInt((rHLen - start) / interval) + 1;
			rHQty *= RightWallHorizontalRowCount;	
		}
		
		Debug.Log(vQty.ToString() + " " + fHQty.ToString() + " " + lHQty.ToString() + " " + rHQty.ToString());
		qty = vQty + fHQty + lHQty + rHQty;
		
		return qty;
	}
	
	void SumAllStudsAndRows(out int vCount, out int fwHCount, out int lwHCount, out int rwHCount, out float vSum, out float hSum)
	{
		vCount = 0;
		fwHCount = 0;
		lwHCount = 0;
		rwHCount = 0;
		vSum = 0;
		hSum = 0;
		
		
		foreach (Stud15 st in AppRoot.Scene.Porch.GetWall(PorchWallId.Front).VStuds)
		{
			if (st.Go.active)
			{
				vSum += st.Scale.y;
				vCount++;
			}
		}
		
		foreach (StudHoriz st in AppRoot.Scene.Porch.GetWall(PorchWallId.Front).HRows)
		{
			if (st.Go.active)
			{
				hSum += st.Scale.x;
				fwHCount++;
			}
		}
		
		//Add two more verticals for corners
		vSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Front).Height * 2;
		vCount += 2;
		
		//Include the top and bottom
		hSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Front).Length * 2;
		fwHCount += 2;
		
		//Skip the one wall option, front wall is always calculated
		switch(AppRoot.Scene.Porch.Layout)
		{
		case PorchWallLayoutId.TwoWallLeft:
			foreach (Stud15 st in AppRoot.Scene.Porch.GetWall(PorchWallId.Left).VStuds)
			{
				if (st.Go.active)
				{
					vSum += st.Scale.y;
					vCount++;
				}
			}

			foreach (StudHoriz st in AppRoot.Scene.Porch.GetWall(PorchWallId.Left).HRows)
			{
				if (st.Go.active)
				{
					hSum += st.Scale.x;
					lwHCount++;
				}
			}
			
			//Two Wall Layout, add two more verticals for corners
			vSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Left).Height * 2;
			vCount += 2;
			
			//Two Wall Layout, add two more horizontals for top/bottom of side
			hSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Left).Length * 2;
			lwHCount += 2;
			
			break;
		case PorchWallLayoutId.TwoWallRight:
			foreach (Stud15 st in AppRoot.Scene.Porch.GetWall(PorchWallId.Right).VStuds)
			{
				if (st.Go.active)
				{
					vSum += st.Scale.y;
					vCount++;
				}
			}

			foreach (StudHoriz st in AppRoot.Scene.Porch.GetWall(PorchWallId.Right).HRows)
			{
				if (st.Go.active)
				{
					hSum += st.Scale.x;
					rwHCount++;
				}
			}
			
			//Two Wall Layout, add two more verticals for corners
			vSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Right).Height * 2;
			vCount += 2;
			
			//Two Wall Layout, add two more horizontals for top/bottom of side
			hSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Right).Length * 2;
			lwHCount += 2;
			
			break;
		case PorchWallLayoutId.ThreeWall:
			foreach (Stud15 st in AppRoot.Scene.Porch.GetWall(PorchWallId.Left).VStuds)
			{
				if (st.Go.active)
				{
					vSum += st.Scale.y;
					vCount++;
				}
			}

			foreach (Stud15 st in AppRoot.Scene.Porch.GetWall(PorchWallId.Right).VStuds)
			{
				if (st.Go.active)
				{
					vSum += st.Scale.y;
					vCount++;
				}
			}

			foreach (StudHoriz st in AppRoot.Scene.Porch.GetWall(PorchWallId.Left).HRows)
			{
				if (st.Go.active)
				{
					hSum += st.Scale.x;
					lwHCount++;
				}
			}

			foreach (StudHoriz st in AppRoot.Scene.Porch.GetWall(PorchWallId.Right).HRows)
			{
				if (st.Go.active)
				{
					hSum += st.Scale.x;
					rwHCount++;
				}
			}
			
			//Three Wall Layout, add four more verticals for corners
			vSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Left).Height * 2;
			vCount += 2;
			
			vSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Right).Height * 2;
			vCount += 2;
			
			//Three Wall Layout, add four more horizontals for top/bottom of each side
			hSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Left).Length * 2;
			lwHCount += 2;
			
			hSum += AppRoot.Scene.Porch.GetWall(PorchWallId.Right).Length * 2;
			lwHCount += 2;
			
			break;
		default: //One Wall
			break;
		}
		
		//If fast track or mini track, double current vertical value (two sided)
		if (product == ProductType.FastTrack || product == ProductType.MiniTrack)
			vSum *= 2;
		
		//Add corner posts to vertical
		
		
		vSum *= Constants.M2In;
		vSum = Mathf.RoundToInt(vSum);
		hSum *= Constants.M2In;
		hSum = Mathf.RoundToInt(hSum);
	}
	
	int CalcQuantity(int len, int modVal)
	{
		int qty;
		
		if ((len % modVal) == 0)
		{
			qty = (int)(len / modVal);	
		}
		else
		{
			qty = (int)Mathf.Floor(len / modVal) + 1;	
		}
		
		return qty;
	}
	
}

class BillOfMaterialsItem
{
	public string PartNumber{get;set;}
	public string Description{get;set;}
	public int Count{get;set;}
			
	public BillOfMaterialsItem(string part, string desc, int c)
	{
		PartNumber = part;
		Description = desc;
		Count = c;
	}
}

class Part
{
	public string PartNumber{get;set;}
	public string Description{get;set;}
	public float Price{get;set;}
	
	public Part(string part, string desc, float price)
	{
		PartNumber = part;
		Description = desc;
		Price = price;
	}
}

public enum ProductType{ScreenTight, FastTrack, MiniTrack};