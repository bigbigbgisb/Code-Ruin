using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Avalonia.Media.Imaging;

using Code_Ruins.Views;

namespace Code_Ruins;


public partial class MainGamePage
{
    private class PlayerData
    {
        public int OffsetX;
        public int OffsetY;
        public int TileX;
        public int TileY;
        public string LastDirection = string.Empty;
        public int LeftIndex;
        public int UpIndex;
        public int RightIndex;
        public int DownIndex;
    }
    private class ChattingData
    {
        public bool IsOutingDone;
        public int ChattingIndex;
        public string RecentStage;
        
    }
    private class ProcessData
    {
        public int RecentMapIndex;
        public string RecentTask;
        public bool IsInTaskZone;
        public string BackgroundImage;
        public string DecorationImage;
        public bool IsTaskBarVisible;
        public string TaskBarText;
        public int[][] Map;

    }

    private class CodeData
    {
        public string PlayerCode;
        public string ChipCode;
        public string PreInput;
        public string StandardOutput;
    }

    private class FpsData
    {
        public long LastTime;
        public int FpsCount;
        public double FpsSum;
    }
    private class GameData
    {
        public PlayerData PlayerData;
        public ChattingData ChattingData;
        public ProcessData ProcessData;
        public FpsData FpsData;
        public CodeData CodeData;
    }
    public void SaveGame()
    {
        var option = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
        };

        var gameData = new GameData()
        {
            PlayerData = new()
            {
                DownIndex = downIndex,
                UpIndex = upIndex,
                LeftIndex = leftIndex,
                RightIndex = rightIndex,
                LastDirection = lastDirection,
                OffsetX = offsetX,
                OffsetY = offsetY,
                TileX = tileX,
                TileY = tileY,
            },
            ChattingData = new()
            {
                IsOutingDone = mwvm!.ChattingBoxViewModel.IsOutingDone,
                ChattingIndex = mwvm!.ChattingBoxViewModel.ChattingIndex,
                RecentStage = mwvm!.ChattingResource.RecentStage,
            },
            ProcessData = new()
            {
                IsInTaskZone = isInTaskZone,
                RecentMapIndex = recentMapIndex,
                RecentTask = recentTask,
                BackgroundImage = ScenePlatform?.Tag?.ToString() ?? "Assets/Pictures/Dummy.png",
                DecorationImage = ScenePlatformDecoration?.Tag?.ToString() ?? "Assets/Pictures/Dummy.png",
                TaskBarText = mwvm!.TaskTipViewModel.TipText,
                IsTaskBarVisible = mwvm!.TaskTipViewModel.IsVisible,
                Map = maps[recentMapIndex].Value,

            },
            
            FpsData = new()
            {
                FpsCount = fpsCount,
                FpsSum = fpsSum,
                LastTime = lastTime,
            },
            CodeData = new()
            {
                ChipCode = mwvm!.ChipCodeViewModel.ChipCodeDocument.Text,
                PlayerCode = mwvm!.CodeEditor.PlayerCode.Text,
                PreInput = mwvm!.ChipCodeViewModel.PreInput,
                StandardOutput = mwvm!.ChipCodeViewModel.StandardOutput,
            }
        };
        string json = JsonSerializer.Serialize<GameData>(gameData,option);
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "CodeRuinsSave"));
        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "CodeRuinsSave","Save.txt"), json);
    }


    private async void ReadGame()
    {
        var option = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
        };

        string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "CodeRuinsSave", "Save.txt"));

        GameData gameData = JsonSerializer.Deserialize<GameData>(json, option)!;

        // ========== PlayerData ==========
        offsetX = gameData.PlayerData.OffsetX;
        offsetY = gameData.PlayerData.OffsetY;
        tileX = gameData.PlayerData.TileX;
        tileY = gameData.PlayerData.TileY;
        lastDirection = gameData.PlayerData.LastDirection;
        leftIndex = gameData.PlayerData.LeftIndex;
        upIndex = gameData.PlayerData.UpIndex;
        rightIndex = gameData.PlayerData.RightIndex;
        downIndex = gameData.PlayerData.DownIndex;

        // ========== ChattingData ==========
        mwvm!.ChattingBoxViewModel.IsOutingDone = gameData.ChattingData.IsOutingDone;
        mwvm!.ChattingBoxViewModel.ChattingIndex = gameData.ChattingData.ChattingIndex;
        mwvm!.ChattingResource.RecentStage = gameData.ChattingData.RecentStage;

        // ========== ProcessData ==========
        isInTaskZone = gameData.ProcessData.IsInTaskZone;
        recentMapIndex = gameData.ProcessData.RecentMapIndex;
        recentTask = gameData.ProcessData.RecentTask;
        if (Path.Exists(gameData.ProcessData.BackgroundImage)) ScenePlatform.Source = new Bitmap(gameData.ProcessData.BackgroundImage);
        else Log.Error($"无法找到资源{gameData.ProcessData.BackgroundImage}");
        if (Path.Exists(gameData.ProcessData.DecorationImage)) ScenePlatformDecoration.Source = new Bitmap(gameData.ProcessData.DecorationImage);
        else Log.Error($"无法找到资源{gameData.ProcessData.DecorationImage}");
        Debug.WriteLine(gameData.ProcessData.IsTaskBarVisible);
        if (gameData.ProcessData.IsTaskBarVisible)
        {
            mwvm!.TaskTipViewModel.TipText = gameData.ProcessData.TaskBarText;
            await mwvm!.TaskTipViewModel.ShowTaskTipAsync();
        }
        for (int x = 0; x < gameData.ProcessData.Map.Length; x++)
        {
            for (int y = 0; y < gameData.ProcessData.Map[0].Length; y++)
            {
                maps[recentMapIndex].Value[x][y] = gameData.ProcessData.Map[x][y];
            }
        }

        // ========== FpsData ==========
        fpsCount = gameData.FpsData.FpsCount;
        fpsSum = gameData.FpsData.FpsSum;
        lastTime = gameData.FpsData.LastTime;

        // ========== CodeData ==========
        mwvm!.ChipCodeViewModel.ChipCodeDocument.Text = gameData.CodeData.ChipCode;
        mwvm!.CodeEditor.PlayerCode.Text = gameData.CodeData.PlayerCode;
        mwvm!.ChipCodeViewModel.PreInput = gameData.CodeData.PreInput;
        mwvm!.ChipCodeViewModel.StandardOutput = gameData.CodeData.StandardOutput;

        if (!gameData.ChattingData.IsOutingDone)
        {
            mwvm!.ChattingBox.WindowState = Avalonia.Controls.WindowState.Normal;
            mwvm!.ChattingBox.IsVisible = true;
        }

    }

}


