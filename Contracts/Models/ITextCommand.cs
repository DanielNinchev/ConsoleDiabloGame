using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface ITextCommand
    {
        IReadOnlyList<string> ArgsList { get; }
        ICharacterService CharacterService { get; }
        string Execute(params string[] args);
    }
}
