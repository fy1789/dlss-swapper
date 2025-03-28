using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DLSS_Swapper.Data;

namespace DLSS_Swapper.UserControls;

public partial class MultipleDLLsFoundControlModel : ObservableObject
{
    public List<GameAsset> DLLsList { get; init; }

    public MultipleDLLsFoundControlModel(Game game, GameAssetType gameAssetType)
    {
        DLLsList = game.GameAssets.Where(x => x.AssetType == gameAssetType).ToList();
    }

    [RelayCommand]
    void OpenDLLPath(GameAsset gameAsset)
    {
        try
        {
            if (File.Exists(gameAsset.Path))
            {
                Process.Start("explorer.exe", $"/select,{gameAsset.Path}");
            }
            else
            {
                var dllPath = Path.GetDirectoryName(gameAsset.Path) ?? string.Empty;
                if (Directory.Exists(dllPath))
                {
                    Process.Start("explorer.exe", dllPath);
                }
                else
                {
                    throw new Exception($"Could not find file \"{gameAsset.Path}\".");
                }
            }
        }
        catch (Exception err)
        {
            Logger.Error(err);
        }
    }
}
