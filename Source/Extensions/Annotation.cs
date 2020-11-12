using System;
using System.Collections.Generic;
using System.Text;
using Mate.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Mate.Net.UT")]

namespace Mate.Extensions
{
    internal static class Annotation
    {
        //TODO: Finish implementing this class
        //TODO: Check if History or Void as return type
        internal static History AnnotateMove(this Player player, Move move)
        {
            return player.Moves;
        }
    }
}