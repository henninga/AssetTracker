using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class NoProgramsViewModel : PropertyChangedBase
    {
        public IResult AddProgram()
        {
            return Show.Screen<AddProgramViewModel>();
        }
    }
}