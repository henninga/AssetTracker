using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class EditProgramViewModel : Screen
    {
        bool _wasSaved;
        public string Name { get; private set; }
        public string Notes { get; private set; }
        public string Id { get; private set; }

        public bool CanSave
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public IEnumerable<IResult> Save()
        {
            yield return Show.Busy();
            var editProgram = new EditProgram(Id,Name, Notes)
                .AsResult();

            _wasSaved = true;
            yield return editProgram;
            yield return Show.Screen<AllProgramsViewModel>();
            yield return Show.NotBusy();
        }

        public void WithData(string id, string name, string notes)
        {
            Id = id;
            Name = name;
            Notes = notes;
        }
    }
}