using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private List<Avalonia.Controls.Image> SceneOneImages;
        void BalanceImageSize()
        {
            //TODO:这里以后应该改成通用的ScenePlatfORm和ScenePlatformDecoration
            ScenePlatform.Width = Bounds.Width * 2.0;
            ScenePlatform.Height = ScenePlatform.Width * (353.0 / 800.0);
            ScenePlatformDecoration.Width = Bounds.Width * 2.0;
            ScenePlatformDecoration.Height = ScenePlatform.Width * (353.0 / 800.0);
        }
        void CalculateTilePosition()
        {

            //X，Y from (0,0)，1=32px
            //图片1像素放大后等于屏幕2像素,所以除以2
            tileX = (int)Math.Floor((double)(-offsetX / 32 / 2));
            tileY = (int)Math.Floor((double)(5 + -offsetY / 32 / 2));
            
        }
        void CalculateAndClampViewport()
        {
            double maxX = Math.Abs(ScenePlatform.Bounds.Width - Bounds.Width);
            double maxY = Math.Abs((ScenePlatform.Bounds.Height - Bounds.Height) / 2);
            double minX = 0;
            offsetX = (int)Math.Clamp(offsetX, -maxX, minX);
            offsetY = (int)Math.Clamp(offsetY, -maxY, maxY + 100);
        }

        void CalculateAndMoveBackground(PointerEventArgs e)
        {
            var point = e.GetPosition(SceneOne);
            double centerX = Bounds.Width / 2;
            double centerY = Bounds.Height / 2;
            double offsetX = (point.X - centerX);
            double offsetY = (point.Y - centerY);

            int i = SceneOneImages.Count;
            foreach (Image image in SceneOneImages)
            {
                image.RenderTransform = new TranslateTransform(offsetX / (i * 10), offsetY / (i * 10));
                i--;
            }
        }

        void ViewportControllerInit()
        {
            
            //设置行走图片
            SceneOneImages = new() { SceneOne1, SceneOne2, SceneOne3, SceneOne4, SceneOne5, SceneOne6 };
            //设置原始偏移点与偏移量
            ScenePlatform.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
            ScenePlatform.RenderTransform = new TranslateTransform(0, 0);
            foreach (Image image in SceneOneImages)
            {
                image.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
            }
            
            
        }

        
    }
}
