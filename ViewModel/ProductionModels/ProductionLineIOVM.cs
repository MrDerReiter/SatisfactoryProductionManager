using CommunityToolkit.Mvvm.ComponentModel;
using FactoryManagementCore;

namespace SatisfactoryProductionManager;

public partial class ProductionLineIOVM : ObservableObject
{
    private readonly ProductionLine _sourceLine;

    public ProductionLine SourceLine { get => _sourceLine; }

    [ObservableProperty]
    public partial List<ResourceStreamButtonVM> InputButtons { get; private set; }
    [ObservableProperty]
    public partial List<ResourceStreamButtonVM> OutputButtons { get; private set; }

    public event Action<ResourceStream> ResourceStreamToBlock;


    public ProductionLineIOVM(ProductionLine sourceLine)
    {
        _sourceLine = sourceLine;
        _sourceLine.IOChanged += UpdateViewModel;
        UpdateViewModel();
    }


    private void UpdateViewModel()
    {
        InputButtons =
        [
            .. _sourceLine.Inputs
            .Select(input => new ResourceStreamButtonVM(input))
        ];
        OutputButtons =
        [
            .. _sourceLine.Outputs
            .Select(output => new ResourceStreamButtonVM(output))
        ];

        InputButtons.ForEach(button =>
        {
            button.ObjectSelected +=
                stream => ResourceStreamToBlock(stream);
        });
    }
}
