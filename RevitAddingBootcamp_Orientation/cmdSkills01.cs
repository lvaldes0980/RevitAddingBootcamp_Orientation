namespace RevitAddingBootcamp_Orientation
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // create a transation to lock the model
            Transaction t = new Transaction(doc);
            t.Start("I'm doing something new in Revit");


            // get a tittle block type
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();

            // create a sheet
            ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newSheet.Name = "My new sheet";
            newSheet.SheetNumber = "A101";

            //create a floor level
            Level newLevel = Level.Create(doc, 10);

            // create a filtered element collector to get view family type
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanVFT = null;// why was this set to null?
            foreach (ViewFamilyType curVFT in collector1) // what is curVFT?
            {
                if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorPlanVFT = curVFT;
                }
            }
            // create a floor plan view
            ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
            newFloorPlan.Name = "My new floor plan";

            ViewFamilyType ceilingPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if(curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT = curVFT;
                }
            }

            // create a ceiling plan view
            ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
            newCeilingPlan.Name = "My new ceiling plan";


            // create a viewport
            // first create a point
            XYZ insPoint = new XYZ();
            XYZ insPoint2 = new XYZ(1, 0.5, 0);

            Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, insPoint2);

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
    }

}
