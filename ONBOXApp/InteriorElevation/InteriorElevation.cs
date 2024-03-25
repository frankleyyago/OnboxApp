using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace ONBOXAppl
{
    [Transaction(TransactionMode.Manual)]
    internal class InteriorElevation : IExternalCommand
    {
        //Variables members.
        UIApplication _uiapp;
        UIDocument _uidoc;
        Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("Title", "Body");

            return Result.Succeeded;
        }

        #region GetAllRooms()
        /// <summary>
        /// Get all rooms in linked and non linked model inside crop view in current view.
        /// </summary>
        /// <returns>List of all rooms.</returns>
        public IList<Element> GetAllRooms()
        {
            IList<Element> allRooms = new List<Element>();

            foreach (Element r in GetAllRoomsNonLink())
            {
                allRooms.Add(r);
            }

            return allRooms;
        }
        #endregion

        #region GetAllRoomsNonLink()
        /// <summary>
        /// Get all rooms inside of crop view in current document, non linked.
        /// </summary>
        /// <returns>List of rooms.</returns>
        public IList<Element> GetAllRoomsNonLink()
        {
            FilteredElementCollector roomCollector = new FilteredElementCollector(_doc, _doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType();

            IList<Element> allRoomsNonLink = roomCollector.ToElements();

            return allRoomsNonLink;
        }
        #endregion

        #region DEBUG
        public void ShowElementList(IList<Element> el)
        {
            string s = string.Empty;
            int num = 0;

            foreach (Element e in el)
            {
                s += $"{num} - {e.Name}";
                num++;
            }

            TaskDialog.Show("Elements", s);
        }
        #endregion
    }
}
