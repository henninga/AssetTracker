using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class HomeViewModel : Screen
    {
        public IResult NewProgram()
        {
            return Show.Screen<AddProgramViewModel>();
        }
    }
}