using XIVComboPlus;

namespace XIVComboPlus.Combos;

internal class GunbreakerGnashingFangContinuation : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.GunbreakerGnashingFangCont;


    protected internal override uint[] ActionIDs { get; } = new uint[1] { 16146u };


    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == 16146)
        {
            if (level >= 70)
            {
                if (HasEffect(1844))
                {
                    return 16158u;
                }
                if (HasEffect(1843))
                {
                    return 16157u;
                }
                if (HasEffect(1842))
                {
                    return 16156u;
                }
            }
            return OriginalHook(16146u);
        }
        return actionID;
    }
}