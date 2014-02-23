namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using NegroniGame.Interfaces;
    using System.Collections.Generic;

    public static class GameManager
    {
        static GameManager()
        {
            SpriteObjList = new List<SpriteObject>();
        }

        public static List<SpriteObject> SpriteObjList { get; set; }

        public static void DoReaction<T>(IReact obj) where T : IReact
        {
            Rectangle reactRect = obj.ReactRect;
            foreach (var item in SpriteObjList)
            {
                if (item is T && item != obj)
                {
                    if ((item as IReact).ReactRect.Intersects(reactRect))
                    {
                        (item as IReact).DoAction<T>(obj);
                    }
                }
            }
        }

        public static bool DoesIntersect(Rectangle positionRect)
        {
            foreach (var item in SpriteObjList)
            {
                if (item.DrawRect.Intersects(positionRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}