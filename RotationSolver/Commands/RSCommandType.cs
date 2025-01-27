﻿namespace RotationSolver.Commands
{
    internal enum SpecialCommandType : byte
    {
        EndSpecial,
        HealArea,
        HealSingle,
        DefenseArea,
        DefenseSingle,
        EsunaShieldNorth,
        RaiseShirk,
        MoveForward,
        MoveBack,
        AntiKnockback,
        Burst,
    }

    internal enum StateCommandType : byte
    {
        Cancel,
        Smart,
        Manual,
    }

    internal enum OtherCommandType : byte
    {
        Settings,
        Rotations,
        Actions,
    }
}
