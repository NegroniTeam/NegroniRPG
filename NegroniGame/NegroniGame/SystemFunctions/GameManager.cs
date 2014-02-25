namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using NegroniGame.Interfaces;
    using System.Collections.Generic;

    public static class GameManager
    {
        private static bool isLocked;
        private static List<SpriteObject> spriteObjList;
        private static List<SpriteObject> spriteToAddList;
        private static List<SpriteObject> spriteToDeleteList;

        static GameManager()
        {
            spriteObjList = new List<SpriteObject>();
            spriteToAddList = new List<SpriteObject>();
            spriteToDeleteList = new List<SpriteObject>();
            isLocked = false;
        }

        public static void DoReaction<T>(IReact obj) where T : IReact
        {
            Rectangle reactRect = obj.ReactRect;
            isLocked = true;
            foreach (var item in spriteObjList)
            {
                if (item is T && item != obj && !item.IsDeleted)
                {
                    if ((item as IReact).ReactRect.Intersects(reactRect))
                    {
                        (item as IReact).DoAction<T>(obj);
                    }
                }
            }

            isLocked = false;
        }

        public static bool DoesIntersect(Rectangle positionRect, SpriteObject obj)
        {
            isLocked = true;
            foreach (var item in spriteObjList)
            {
                if (item != obj && item.DrawRect.Intersects(positionRect))
                {
                    isLocked = false;
                    return true;
                }
            }

            isLocked = false;
            return false;
        }

        // Call Update method for all not deleted game objects
        public static void Update(GameTime gameTime)
        {
            foreach (var obj in GameManager.spriteObjList)
            {
                if (obj.IsDeleted)
                {
                    spriteToDeleteList.Add(obj);
                    continue;
                }

                obj.Update(gameTime);
            }
        }

        // Call Draw method for all visible game objects
        public static void Draw(SpriteBatch spriteBatch)
        {
            // Add objects
            if (!isLocked)
            {
                foreach (var obj in spriteToAddList)
                {
                    spriteObjList.Add(obj);
                }

                spriteToAddList = new List<SpriteObject>();
            }

            isLocked = true;
            foreach (var obj in spriteObjList)
            {
                obj.Draw();
            }

            // Delete Objects
            isLocked = false;
            if (!isLocked)
            {
                foreach (var obj in spriteToDeleteList)
                {
                    spriteObjList.RemoveAll(x => x == obj);
                }

                spriteToDeleteList = new List<SpriteObject>();
            }
        }

        public static void AddSprite(SpriteObject obj)
        {
            spriteToAddList.Add(obj);
        }

        public static void RemoveSprite(SpriteObject obj)
        {
            spriteToDeleteList.Add(obj);
        }
    }
}