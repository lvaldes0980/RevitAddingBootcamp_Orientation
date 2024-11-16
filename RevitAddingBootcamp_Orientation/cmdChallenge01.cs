using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace RevitAddingBootcamp_Orientation
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 01 Challenge code goes here
            int myFirstNumber = 250;
            double startingElevation = 0;
            double floorHeight = 15;

            startingElevation += floorHeight;

            // create a transation to lock the model
            Transaction t = new Transaction(doc);
            t.Start("I'm solving the challenge question");

            for (int i = 1; i < myFirstNumber; i++)

            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    // get a tittle block type
                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                    collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
                    collector2.WhereElementIsElementType();

                    // create a sheet
                    ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
                    newSheet.Name = "FIZZBUZZ_#" + i;

                    //create a floor level
                    Level newLevel = Level.Create(doc, floorHeight);

                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }
                    // create a floor plan view
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);

                    // create a viewport
                    // first create a point
                    XYZ placePoint = new XYZ();
                    Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, placePoint);
                }
                else if (i % 3 == 0)
                {
                    //create a floor level
                    Level newLevel = Level.Create(doc, floorHeight);

                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }
                    // create a floor plan view
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPlan.Name = "FIZZ_" + i;

                   
                }
                else if (i % 5 == 0)
                {
                    //create a floor level
                    Level newLevel = Level.Create(doc, floorHeight);

                    // create a filtered element collector to get view family type
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                        }
                    }
                    // create a ceiling plan view
                    ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    newCeilingPlan.Name = "BUZZ_" + i;
                }

               


            }
            t.Commit();
            t.Dispose();




            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");

            return myButtonData.Data;
        }
    }

}
