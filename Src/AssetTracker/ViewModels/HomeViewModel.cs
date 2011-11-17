using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class HomeViewModel : Screen
    {
        
        public HomeViewModel(ProgramSearchViewModel programSearchViewModel)
        {
            ProgramSearch = programSearchViewModel;
        }

        public IScreen ProgramSearch { get; set; }

        public IResult NewProgram()
        {
            return Show.Screen<AddProgramViewModel>();
        }

    }
}