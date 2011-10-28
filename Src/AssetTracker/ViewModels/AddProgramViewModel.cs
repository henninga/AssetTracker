using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Queries;
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
                .AsCommand();

            _wasSaved = true;
            yield return addProgram;
            var getProgram = new GetProgram(addProgram.Command.GeneratedId)
                                .AsQuery();
            yield return getProgram;
            yield return Show.Screen<ProgramOverviewViewModel>().Configured(x => x.WithData(getProgram.Response));
            yield return Show.NotBusy();

        }

        
        
    }
}