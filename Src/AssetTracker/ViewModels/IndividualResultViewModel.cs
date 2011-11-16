using System.Collections.Generic;
using AssetTracker.Framework;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class IndividualResultViewModel : Screen
    {
        public IndividualResultViewModel()
        {
        }

        public IndividualResultViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
        public string Id { get; set; }

        public IEnumerable<IResult> Open()
        {
            yield return Show.Busy();
            var getProgram = new GetProgram(Id)
                .AsQuery();
            yield return getProgram;
            yield return Show.Screen<ProgramOverviewViewModel>()
                .Configured(x => x.WithData(getProgram.Response));
            yield return Show.NotBusy();
        }
    }
}