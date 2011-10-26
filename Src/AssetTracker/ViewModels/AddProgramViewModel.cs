using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class AddProgramViewModel : Screen
    {
        bool _wasSaved;
        public string Name { get; set; }
        public string Notes { get; set; }

        public bool CanAddProgram
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public IEnumerable<IResult> AddProgram()
        {
            yield return Show.Busy();
            var addProgram = new AddProgram(Name, Notes)
                .AsResult();

            _wasSaved = true;
            yield return addProgram;
            yield return Show.Screen<AllProgramsViewModel>();
            yield return Show.NotBusy();

        }

        
        
    }
}