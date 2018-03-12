using SFMF;
using UnityEngine;

namespace VisibleCursor
{
    public class VisibleCursor : IMod
    {
        public void Start()
        {
            Cursor.visible = true;
        }
    }
}
