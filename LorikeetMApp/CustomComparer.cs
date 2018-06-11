using System;
using System.Collections.Generic;
using Syncfusion.DataSource;

namespace LorikeetMApp
{
	public class CustomComparer : IComparer<object>, ISortDirection
    {
        public int Compare(object x, object y)
        {
            int nameX;
            int nameY;

            //For Contacts Type data
			if (x.GetType() == typeof(ModelsLinq.MemberSQLite))
            {
                //Calculating the length of ContactName if the object type is Contacts
				nameX = ((ModelsLinq.MemberSQLite)x).FirstName.Length;
				nameY = ((ModelsLinq.MemberSQLite)y).FirstName.Length;
            }
            else
            {
                nameX = x.ToString().Length;
                nameY = y.ToString().Length;
            }

            // Objects are compared and return the SortDirection
            if (nameX.CompareTo(nameY) > 0)
                return SortDirection == ListSortDirection.Ascending ? 1 : -1;
            else if (nameX.CompareTo(nameY) == -1)
                return SortDirection == ListSortDirection.Ascending ? -1 : 1;
            else
                return 0;
        }

        //Get or Set the SortDirection value
        private ListSortDirection _SortDirection;
        public ListSortDirection SortDirection
        {
            get { return _SortDirection; }
            set { _SortDirection = value; }
        }
    }
}
