using System;
using System.Collections.Generic;
using AssetTracker.Extensions;
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
        public IndividualResultViewModel(string id,string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Id { get; set; }

        public IEnumerable<IResult> Open()
        {
            yield return Show.Busy();
            var getProgram = new GetProgram(Id)
                .AsResult();
            yield return getProgram;
            yield return Show.Screen<ProgramVersionsViewModel>()
                            .Configured(x => x.WithData(getProgram.Response));
            yield return Show.NotBusy();
        }

        
    }
}