using Avalonia.Media;
using Avalonia.Media.Imaging;
using Code_Ruins.Views;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
        private Dictionary<string,Bitmap> cacheCharacterStandingImage;
        private int leftIndex;
        private int upIndex;
        private int rightIndex;
        private int downIndex;
        private List<string> characterWalkingImage;
        private List<Bitmap> cacheCharacterWalkingImage;
        private long lastTime = DateTime.Now.Ticks;
        private int fpsCount = 0;
        private double fpsSum = 0;  

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
                Character.Source = cacheCharacterWalkingImage[upIndex];
                upIndex++;
            }
            if (isDown)
            {
                if (downIndex >= 12)
                {
                    downIndex = 9;

                }
                lastDirection = "Down";
                Character.Source = cacheCharacterWalkingImage[downIndex];
                downIndex++;
            }
            if (isLeft)
            {
                if (leftIndex >= 2)
                {
                    leftIndex = 0;
                }
                lastDirection = "Left";
                Character.Source = cacheCharacterWalkingImage[leftIndex];
                leftIndex++;
            }
            if (isRight)
            {
                if (rightIndex >= 8)
                {
                    rightIndex = 6;
                }
                lastDirection = "Right";
                Character.Source = cacheCharacterWalkingImage[rightIndex];
                rightIndex++;
            }
            if (!isLeft && !isRight && !isUp && !isDown)
            {
                Character.Source = cacheCharacterStandingImage[lastDirection];
            }
        }
        void CalculateAndChangeOffsetValue()
        {
            CalculateFps();
            if (isUp)
            {
                int nextOffsetY = offsetY + mwvm.BaseSettingsViewModel.PlayerSpeed;
                int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
                try
                {
                    if (maps[recentMapIndex].Value[nextTileY][tileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetY += mwvm.BaseSettingsViewModel.PlayerSpeed;
                    }
                }
                catch
                {
                    //Out of map index
                }



            }
            if (isDown)
            {
                int nextOffsetY = offsetY - mwvm.BaseSettingsViewModel.PlayerSpeed;
                int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
                try
                {
                    if (maps[recentMapIndex].Value[nextTileY][tileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetY -= mwvm.BaseSettingsViewModel.PlayerSpeed;
                    }
                }

                catch
                {
                    //Out of map index
                }


            }
            if (isLeft)
            {
                int nextOffsetX = offsetX + mwvm.BaseSettingsViewModel.PlayerSpeed;
                int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
                try
                {
                    if (maps[recentMapIndex].Value[tileY][nextTileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetX += mwvm.BaseSettingsViewModel.PlayerSpeed;
                    }
                }
                catch
                {
                    //Out of map index
                }

            }
            if (isRight)
            {
                int nextOffsetX = offsetX - mwvm.BaseSettingsViewModel.PlayerSpeed;
                int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
                try
                {
                    if (maps[recentMapIndex].Value[tileY][nextTileX] == 1)
                    {
                        //pass
                    }
                    else
                    {
                        offsetX -= mwvm.BaseSettingsViewModel.PlayerSpeed;
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
            ScenePlatform.RenderTransform = new TranslateTransform(offsetX, offsetY);
            ScenePlatformDecoration.RenderTransform = new TranslateTransform(offsetX, offsetY);
        }

        void CalculateFps()
        {
            long thisTime = DateTime.Now.Ticks;
            double deltaTime = (thisTime - lastTime) / 10000.0;
            if (fpsCount == 5)
            {
                FpsShower.Text = Math.Round(1000.0 / (fpsSum / 5.0)).ToString();
                fpsCount = 1;
                fpsSum = 0;
                fpsSum += deltaTime;
            }
            else
            {
                fpsSum += deltaTime;
                fpsCount++;
            }
            
            lastTime = thisTime;
        }
        void MovementInit()
        {
            offsetX = 0; offsetY = 0;
            tileX = 0; tileY = 0;
            cacheCharacterWalkingImage = new();
            cacheCharacterStandingImage = new();
            characterStandingImage = new()
            {
                ["Left"] = "Assets/ManAction/LMan2.png",
                ["Up"] = "Assets/ManAction/UMan2.png",
                ["Right"] = "Assets/ManAction/RMan2.png",
                ["Down"] = "Assets/ManAction/DMan2.png"
            };
            leftIndex = 0;  upIndex = 3; rightIndex = 6; downIndex = 9;
            characterWalkingImage = new() { "Assets/ManAction/LMan1.png", "Assets/ManAction/LMan2.png", "Assets/ManAction/LMan3.png", "Assets/ManAction/UMan1.png", "Assets/ManAction/UMan2.png", "Assets/ManAction/UMan3.png", "Assets/ManAction/RMan1.png", "Assets/ManAction/RMan2.png", "Assets/ManAction/RMan3.png", "Assets/ManAction/DMan1.png", "Assets/ManAction/Dman2.png", "Assets/ManAction/DMan3.png" };
            foreach(string path in characterWalkingImage)
            {
                cacheCharacterWalkingImage.Add(new Bitmap(path));
            }
            foreach (var keyAndValue in characterStandingImage)
            {
                cacheCharacterStandingImage[keyAndValue.Key] = new Bitmap(keyAndValue.Value);
            }

            lastDirection = "Right";
        }
    }
}
