using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private int offsetX;
        private int offsetY;
        private int tileX;
        private int tileY;
        private bool isUp;
        private bool isDown;
        private bool isLeft;
        private bool isRight;
        private Dictionary<string, string> characterStandingImage;
        private int leftIndex;
        private int upIndex;
        private int rightIndex;
        private int downIndex;
        private List<string> characterWalkingImage;

        private string lastDirection;
        void PlayerAnimation()
        {
            if (isUp)
            {
                if (upIndex >= 5)
                {
                    upIndex = 3;
                }
                lastDirection = "Up";
                Character.Source = new Bitmap(characterWalkingImage[upIndex]);
                upIndex++;
            }
            if (isDown)
            {
                if (downIndex >= 12)
                {
                    downIndex = 9;

                }
                lastDirection = "Down";
                Character.Source = new Bitmap(characterWalkingImage[downIndex]);
                downIndex++;
            }
            if (isLeft)
            {
                if (leftIndex >= 2)
                {
                    leftIndex = 0;
                }
                lastDirection = "Left";
                Character.Source = new Bitmap(characterWalkingImage[leftIndex]);
                leftIndex++;
            }
            if (isRight)
            {
                if (rightIndex >= 8)
                {
                    rightIndex = 6;
                }
                lastDirection = "Right";
                Character.Source = new Bitmap(characterWalkingImage[rightIndex]);
                rightIndex++;
            }
            if (!isLeft && !isRight && !isUp && !isDown)
            {
                Character.Source = new Bitmap(characterStandingImage[lastDirection]);
            }
        }
        void CalculateAndChangeOffsetValue()
        {
            if (isUp)
            {
                int nextOffsetY = offsetY + 3;
                int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
                try
                {
                    if (maps[recentMap][nextTileY][tileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetY += 3;
                    }
                }
                catch
                {
                    //Out of map index
                }



            }
            if (isDown)
            {
                int nextOffsetY = offsetY - 3;
                int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
                try
                {
                    if (maps[recentMap][nextTileY][tileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetY -= 3;
                    }
                }

                catch
                {
                    //Out of map index
                }


            }
            if (isLeft)
            {
                int nextOffsetX = offsetX + 3;
                int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
                try
                {
                    if (maps[recentMap][tileY][nextTileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetX += 3;
                    }
                }
                catch
                {
                    //Out of map index
                }

            }
            if (isRight)
            {
                int nextOffsetX = offsetX - 3;
                int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
                try
                {
                    if (maps[recentMap][tileY][nextTileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetX -= 3;
                    }
                }
                catch
                {
                    //Out of map index
                }

            }
            
        }
        void UpdateMapTranslation()
        {
            SceneOnePlatform.RenderTransform = new TranslateTransform(offsetX, offsetY);
        }

        void MovementInit()
        {
            offsetX = 0; offsetY = 0;
            tileX = 0; tileY = 0;
            characterStandingImage = new()
            {
                ["Left"] = "Assets/ManAction/LMan2.png",
                ["Up"] = "Assets/ManAction/UMan2.png",
                ["Right"] = "Assets/ManAction/RMan2.png",
                ["Down"] = "Assets/ManAction/DMan2.png"
            };
            leftIndex = 0;  upIndex = 3; rightIndex = 6; downIndex = 9;
            characterWalkingImage = new() { "Assets/ManAction/LMan1.png", "Assets/ManAction/LMan2.png", "Assets/ManAction/LMan3.png", "Assets/ManAction/UMan1.png", "Assets/ManAction/UMan2.png", "Assets/ManAction/UMan3.png", "Assets/ManAction/RMan1.png", "Assets/ManAction/RMan2.png", "Assets/ManAction/RMan3.png", "Assets/ManAction/DMan1.png", "Assets/ManAction/Dman2.png", "Assets/ManAction/DMan3.png" };
            lastDirection = "Right";
        }
    }
}
